using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership.Filters
{
    public class OrderAndCompleteFilter : ActionFilterAttribute
    {
        private readonly IMembershipService _membershipService;
        private readonly IWorkContext _workContext;

        public OrderAndCompleteFilter(
            IMembershipService membershipService,
            IWorkContext workContext)
        {
            _membershipService = membershipService;
            _workContext = workContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (actionDescriptor == null)
            {
                return;
            }

            if (actionDescriptor.ControllerName == "ShoppingCart" && actionDescriptor.ActionName == "AddProductToCart_Details")
            {
                var membership = _membershipService.GetMembershipByCustomerId(_workContext.CurrentCustomer.Id);
                if(membership.OrderRequestCount >= membership.MaxProducts)
                {
                    context.Result = new BadRequestObjectResult($"Order request count is {membership.OrderRequestCount} and maximum items allowed are {membership.MaxProducts}");
                    //todo: add logging
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
