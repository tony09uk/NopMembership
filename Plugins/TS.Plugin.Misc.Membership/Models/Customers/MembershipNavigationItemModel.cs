using Nop.Web.Framework.Models;

namespace Ts.Plugin.Misc.Membership.Models.Customers
{
    public class MembershipNavigationItemModel : BaseNopModel
    {
        public string RouteName { get; set; }
        public string Title { get; set; }
        public MembershipNavigationEnum Tab { get; set; }
        public string ItemClass { get; set; }
    }
}
