using System.Security.Claims;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
	{
        public async Task SignInWithRole()
        {
            await SignInImplAsync(new Claim(ClaimTypes.Role, "Role"));
        }

        public async Task SignIn()
        {
            await SignInImplAsync();
        }

        private async Task SignInImplAsync(params Claim[] claims)
        {
            var identity = new ClaimsIdentity(claims, "ApplicationCookie");

            await Context.GetAuthentication().SignInAsync(Startup.AuthenticationScheme, new ClaimsPrincipal(identity));
            Context.RedirectToRoute(Context.Route.RouteName);
        }

        public async Task SignOut()
        {
            await Context.GetAuthentication().SignOutAsync(Startup.AuthenticationScheme);
            Context.RedirectToRoute(Context.Route.RouteName);
        }
    }
}

