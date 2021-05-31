using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using System.Collections.Generic;
using System.Linq;
using Ts.Plugin.Misc.Membership.Factories;
using Ts.Plugin.Misc.Membership.Models;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership.Filters
{
    public class MembershipResultFilterBase : ActionFilterAttribute
    {
        protected readonly IMembershipService _membershipService;
        protected readonly IMembershipListFactory _membershipListFactory;
        protected readonly IWorkContext _workContext;

        public MembershipResultFilterBase(
            IMembershipService membershipService,
            IMembershipListFactory membershipListFactory,
            IWorkContext workContext)
        {
            _membershipService = membershipService;
            _membershipListFactory = membershipListFactory;
            _workContext = workContext;
        }

        protected bool shouldDisableBuyButton(Dictionary<int, bool> allowedPurchaseList, int productId)
        {
            KeyValuePair<int, bool> allowedPurchaseItem = allowedPurchaseList.SingleOrDefault(x => x.Key == productId);

            if ((allowedPurchaseItem.Key, allowedPurchaseItem.Value) == default || allowedPurchaseItem.Value)
            {
                return false;
            }

            return true;
        }

        protected bool IsTargetRoute(FilterContext context, string targetControllerName, string targetActionName)
        {
            var route = Route(context);

            if (route == null)
            {
                return false;
            }

            var isTargetController = route.ControllerName == targetControllerName;
            var isTargetAction = route.ActionName == targetActionName;

            return (isTargetController && isTargetAction) ? true : false;
        }

        protected Dictionary<int, bool> GetAllowedPurchasedList()
        {
            Domain.Membership selectedMembership = _membershipService.GetMembershipByCustomerId(_workContext.CurrentCustomer.Id);

            //todo: handle non membership
            return _membershipListFactory.CreateAllowedPurchaseList(selectedMembership);
        }

        protected MembershipActionDescriptorRouteResult Route(FilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (actionDescriptor == null)
            {
                return null;
            }
            return new MembershipActionDescriptorRouteResult(actionDescriptor.ControllerName, actionDescriptor.ActionName);
        }
    }
}
