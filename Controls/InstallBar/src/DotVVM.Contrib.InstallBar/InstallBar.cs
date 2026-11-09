using DotVVM.Contrib.InstallBar.Resources;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.InstallBar
{
    /// <summary>
    /// Renders a bar sticking to the bottom of the screen which suggests the user to install the web app (PWA).
    /// The bar is never displayed when the app already runs in the standalone mode, and it can be dismissed
    /// by the user for the rest of the browser session.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false)]
    public class InstallBar : DotvvmMarkupControl
    {

        /// <summary>
        /// Gets or sets the title displayed in the bar.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DotvvmProperty TitleProperty
            = DotvvmProperty.Register<string, InstallBar>(c => c.Title, null);

        /// <summary>
        /// Gets or sets the description displayed when the browser supports the native install prompt.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DotvvmProperty DescriptionProperty
            = DotvvmProperty.Register<string, InstallBar>(c => c.Description, null);

        /// <summary>
        /// Gets or sets the instructions displayed on iOS / iPadOS where the app must be added to the home screen manually.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string IosInstructions
        {
            get { return (string)GetValue(IosInstructionsProperty); }
            set { SetValue(IosInstructionsProperty, value); }
        }
        public static readonly DotvvmProperty IosInstructionsProperty
            = DotvvmProperty.Register<string, InstallBar>(c => c.IosInstructions, null);

        /// <summary>
        /// Gets or sets the instructions displayed when the bar is shown explicitly (using the JavaScript API)
        /// in a browser which doesn't offer the native install prompt.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string ManualInstructions
        {
            get { return (string)GetValue(ManualInstructionsProperty); }
            set { SetValue(ManualInstructionsProperty, value); }
        }
        public static readonly DotvvmProperty ManualInstructionsProperty
            = DotvvmProperty.Register<string, InstallBar>(c => c.ManualInstructions, null);

        /// <summary>
        /// Gets or sets the text of the install button.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string InstallButtonText
        {
            get { return (string)GetValue(InstallButtonTextProperty); }
            set { SetValue(InstallButtonTextProperty, value); }
        }
        public static readonly DotvvmProperty InstallButtonTextProperty
            = DotvvmProperty.Register<string, InstallBar>(c => c.InstallButtonText, null);

        /// <summary>
        /// Gets or sets the tooltip and accessible label of the close button.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string CloseButtonText
        {
            get { return (string)GetValue(CloseButtonTextProperty); }
            set { SetValue(CloseButtonTextProperty, value); }
        }
        public static readonly DotvvmProperty CloseButtonTextProperty
            = DotvvmProperty.Register<string, InstallBar>(c => c.CloseButtonText, null);

        /// <summary>
        /// Gets or sets the URL of an icon displayed in the bar (typically the app icon from the web manifest).
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string IconUrl
        {
            get { return (string)GetValue(IconUrlProperty); }
            set { SetValue(IconUrlProperty, value); }
        }
        public static readonly DotvvmProperty IconUrlProperty
            = DotvvmProperty.Register<string, InstallBar>(c => c.IconUrl, null);

        /// <summary>
        /// Gets or sets the session storage key used to remember that the user has closed the bar.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string StorageKey
        {
            get { return (string)GetValue(StorageKeyProperty); }
            set { SetValue(StorageKeyProperty, value); }
        }
        public static readonly DotvvmProperty StorageKeyProperty
            = DotvvmProperty.Register<string, InstallBar>(c => c.StorageKey, "dotvvm-contrib-install-bar-dismissed");


        public InstallBar()
        {
            Title = InstallBarTexts.Title;
            Description = InstallBarTexts.Description;
            IosInstructions = InstallBarTexts.IosInstructions;
            ManualInstructions = InstallBarTexts.ManualInstructions;
            InstallButtonText = InstallBarTexts.Btn_Install;
            CloseButtonText = InstallBarTexts.Btn_Close;
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            // the beforeinstallprompt event can fire before the view module is initialized - capture it as early as possible
            context.ResourceManager.AddRequiredResource(InstallBarConfigurationExtensions.EarlyCaptureResourceName);

            base.OnPreRender(context);
        }
    }
}
