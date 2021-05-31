using System.Collections.Generic;

namespace Ts.Plugin.Misc.Membership.Models.Customers
{
    public class MembershipList
    {
        public Nop.Web.Models.Catalog.CategoryModel CategoryModel { get; set; }
        public int? SelectedMembershipId { get; set; }
        public Dictionary<int, bool> UpgradableMembershipList { get; set; }
    }
}
