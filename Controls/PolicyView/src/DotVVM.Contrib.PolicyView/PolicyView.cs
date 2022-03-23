using System.Security.Claims;
using System.Threading.Tasks;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace DotVVM.Contrib.PolicyView
{
    /// <summary>
    /// Renders different content to the users who met requirements specified by an authorization Policy definition and to users who don't met it.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(RequirementsMetTemplate))]
    public class PolicyView : ConfigurableHtmlControl
    {
        private readonly IAuthorizationService _authorizationService;

        public PolicyView(IAuthorizationService authorizationService) : base("div")
        {
            RenderWrapperTag = false;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Gets or sets a comma-separated list of authorization policy names. The user must meet at least one of these authorization policies.
        /// </summary>
        [MarkupOptions(AllowBinding = false, Required = true)]
        public string[] Policies
        {
            get { return (string[])GetValue(PoliciesProperty); }
            set { SetValue(PoliciesProperty, value); }
        }

        public static readonly DotvvmProperty PoliciesProperty
            = DotvvmProperty.Register<string[], PolicyView>(c => c.Policies, null);

        /// <summary>
        /// Gets or sets the content displayed to the users who met requirements specified by an authorizaiton Policy definition.
        /// </summary>
        [MarkupOptions(MappingMode = MappingMode.InnerElement, AllowBinding = false)]
        public ITemplate RequirementsMetTemplate
        {
            get { return (ITemplate)GetValue(RequirementsMetTemplateProperty); }
            set { SetValue(RequirementsMetTemplateProperty, value); }
        }

        public static readonly DotvvmProperty RequirementsMetTemplateProperty
            = DotvvmProperty.Register<ITemplate, PolicyView>(c => c.RequirementsMetTemplate);

        /// <summary>
        /// Gets or sets the content displayed to the users who don't met requirements specified by an authorization Policy definition.
        /// </summary>
        [MarkupOptions(MappingMode = MappingMode.InnerElement, AllowBinding = false)]
        public ITemplate RequirementsNotMetTemplate
        {
            get { return (ITemplate)GetValue(RequirementsNotMetTemplateProperty); }
            set { SetValue(RequirementsNotMetTemplateProperty, value); }
        }

        public static readonly DotvvmProperty RequirementsNotMetTemplateProperty
            = DotvvmProperty.Register<ITemplate, PolicyView>(c => c.RequirementsNotMetTemplate);

        /// <summary>
        /// Gets or sets whether the control will be hidden completely to anonymous users. If set to <c>false</c>,
        /// the <see cref="RequirementsNotMetTemplate" /> will be rendered to anonymous users.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public bool HideForAnonymousUsers
        {
            get { return (bool)GetValue(HideForAnonymousUsersProperty); }
            set { SetValue(HideForAnonymousUsersProperty, value); }
        }

        public static readonly DotvvmProperty HideForAnonymousUsersProperty
            = DotvvmProperty.Register<bool, PolicyView>(c => c.HideForAnonymousUsers, true);


        protected override void OnInit(IDotvvmRequestContext context)
        {
            var user = context.HttpContext.User;
            var isAuthenticated = user?.Identity?.IsAuthenticated == true;

            if (!HideForAnonymousUsers || isAuthenticated)
            {
                if (isAuthenticated && UserRequirementMetAsync(user).GetAwaiter().GetResult())
                {
                    RequirementsMetTemplate?.BuildContent(context, this);
                }
                else
                {
                    RequirementsNotMetTemplate?.BuildContent(context, this);
                }
            }
        }

        protected virtual async Task<bool> UserRequirementMetAsync(ClaimsPrincipal user)
        {
            if (user != null && Policies != null)
            {
                foreach (var policy in Policies)
                {
                    var authorizationSucceed = await HasUserMeetAuthorizationPolicy(user, policy).ConfigureAwait(false);

                    if (authorizationSucceed)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected virtual async Task<bool> HasUserMeetAuthorizationPolicy(ClaimsPrincipal user, string policy)
        {
            return (await _authorizationService.AuthorizeAsync(user, policy)).Succeeded;
        }
    }
}
