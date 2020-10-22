using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Authentication;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public ActionResult Login()
        {
            return base.View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.UserName, model.Password))
                {
                    TempData["ActionMessage"] = "Welcome!";

                    return base.Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
            }

            return this.AddModelErrorToLoginView();
        }

        private ViewResult AddModelErrorToLoginView()
        {
            ModelState.AddModelError("AdminAuthenticationError", "Incorrect username or password");
            return base.View();
        }
    }
}