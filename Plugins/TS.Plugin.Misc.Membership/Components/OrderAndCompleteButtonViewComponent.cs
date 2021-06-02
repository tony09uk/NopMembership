using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;
using System.Linq;
using Ts.Plugin.Misc.Membership;
using Ts.Plugin.Misc.Membership.Models.Order;

namespace Nop.Plugin.Misc.MySmartCards.Components
{
    [ViewComponent(Name = MembershipPluginConstants.MEMBERSHIP_ORDER_AND_COMPLETE_BUTTON_COMPONENT_NAME)]
    public class OrderAndCompleteButtonViewComponent : NopViewComponent
    {
        public OrderAndCompleteButtonViewComponent()
        {
        }

        public IViewComponentResult Invoke(RouteValueDictionary additionalData)
        {
            var productDetailsModel = additionalData.Values.LastOrDefault() as ProductDetailsModel;

            return View("~/Plugins/Misc.Membership/Views/Shared/Components/OrderAndCompleteButton/Default.cshtml", new OrderButtonModel { ProductId = productDetailsModel.AddToCart.ProductId });
        }
    }
}
