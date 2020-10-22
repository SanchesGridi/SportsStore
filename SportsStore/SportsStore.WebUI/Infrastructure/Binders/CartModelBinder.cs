using System.Web.Mvc;
using SportsStore.Domain.Models;

namespace SportsStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cart = null as Cart;

            if (controllerContext.HttpContext.Session != null)
            {
                cart = controllerContext.HttpContext.Session["Cart"] as Cart;

                if (cart == null)
                {
                    cart = new Cart();

                    controllerContext.HttpContext.Session["Cart"] = cart;
                }
            }

            return cart;
        }
    }
}