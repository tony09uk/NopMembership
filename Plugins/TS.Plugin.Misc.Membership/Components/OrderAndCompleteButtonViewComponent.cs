using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;
using System.Linq;
using Ts.Plugin.Misc.Membership;
using Ts.Plugin.Misc.Membership.Factories;
using Ts.Plugin.Misc.Membership.Models.Order;
using Ts.Plugin.Misc.Membership.Services;

namespace Nop.Plugin.Misc.MySmartCards.Components
{
    [ViewComponent(Name = MembershipPluginConstants.MEMBERSHIP_ORDER_AND_COMPLETE_BUTTON_COMPONENT_NAME)]
    public class OrderAndCompleteButtonViewComponent : NopViewComponent
    {
        private readonly IOrderAndCompleteButtonViewFactory _orderAndCompleteButtonViewFactory;

        public OrderAndCompleteButtonViewComponent(IOrderAndCompleteButtonViewFactory orderAndCompleteButtonViewFactory)
        {
            _orderAndCompleteButtonViewFactory = orderAndCompleteButtonViewFactory;
        }

        public IViewComponentResult Invoke(RouteValueDictionary additionalData)
        {
            var productDetailsModel = additionalData.Values.LastOrDefault() as ProductDetailsModel;
            var orderAndCompleteButtonModel = _orderAndCompleteButtonViewFactory.PrepareOrderAndCompleteButtonModel(productDetailsModel.AddToCart.ProductId);

            return View("~/Plugins/Misc.Membership/Views/Shared/Components/OrderAndCompleteButton/Default.cshtml", orderAndCompleteButtonModel);
        }
    }
}
