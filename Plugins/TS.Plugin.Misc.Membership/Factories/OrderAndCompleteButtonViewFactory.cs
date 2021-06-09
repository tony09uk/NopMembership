using Nop.Core;
using Ts.Plugin.Misc.Membership.Models.Order;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership.Factories
{
    public class OrderAndCompleteButtonViewFactory : IOrderAndCompleteButtonViewFactory
    {
        private readonly IMembershipService _membershipService;
        private readonly IWorkContext _workContext;

        public OrderAndCompleteButtonViewFactory(
            IMembershipService membershipService,
            IWorkContext workContext)
        {
            _membershipService = membershipService;
            _workContext = workContext;
        }

        public OrderAndCompleteButtonModel PrepareOrderAndCompleteButtonModel(int productId)
        {
            var orderAndCompleteButtonModel = new OrderAndCompleteButtonModel { ProductId = productId };

            var membership = _membershipService.GetMembershipByCustomerId(_workContext.CurrentCustomer.Id);

            if(membership == null)
            {
                return orderAndCompleteButtonModel;
            }

            if(membership.OrderRequestCount >= membership.MaxProducts)
            {
                orderAndCompleteButtonModel.ShowAlterExistingOrdersButton = true;
                orderAndCompleteButtonModel.ShowUpgradeMembershipButton = true;
            }
            else
            {
                orderAndCompleteButtonModel.ShowAddOrderButton = true;
            }

            if(membership.OrderFulfilledCount >= membership.MaxProducts)
            {
                orderAndCompleteButtonModel.IsAllOrderFulfilled = true;
            }

            return orderAndCompleteButtonModel;
        }
    }
}
