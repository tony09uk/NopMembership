using System.Collections.Generic;

namespace Ts.Plugin.Misc.Membership.Models.Customers
{
    public class MembershipNavigationModel
    {
        public MembershipNavigationModel()
        {
            MembershipNavigationItems = new List<MembershipNavigationItemModel>();
        }

        public IList<MembershipNavigationItemModel> MembershipNavigationItems { get; set; }

        public MembershipNavigationEnum SelectedTab { get; set; }
    }
}
