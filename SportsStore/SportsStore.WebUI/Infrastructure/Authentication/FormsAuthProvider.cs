using System.Web.Security;

namespace SportsStore.WebUI.Infrastructure.Authentication
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
        #pragma warning disable CS0618 // Type or member is obsolete
            var authResult = FormsAuthentication.Authenticate(username, password);
        #pragma warning restore CS0618 // Type or member is obsolete

            if (authResult)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return authResult;
        }
    }
}