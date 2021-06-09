using Nop.Web.Framework.Models;

namespace Ts.Plugin.Misc.Membership.Models.Order
{
    public class OrderAndCompleteButtonModel : BaseNopModel
    {
        public int ProductId { get; set; }
        public bool ShowAddOrderButton { get; set; }
        public bool ShowUpgradeMembershipButton { get; set; }
        public bool ShowAlterExistingOrdersButton { get; set; }
        public bool IsAllOrderFulfilled { get; set; }
    }
}
