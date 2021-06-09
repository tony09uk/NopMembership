using System.Collections.Generic;

namespace Ts.Plugin.Misc.Membership
{
    public class MemberShipPluginResources
    {
        public static Dictionary<string, string> Get()
        {
            //todo: check this works - install process not tested with new value (manually entered into DB for testing other plugin elements)
            return new Dictionary<string, string>
            {
                ["ts.customer-navigation-membership"] = "Membership",
                ["Misc.Membership.Account.Subscriptions"] = "Membership",
                ["Misc.Membership.OrderAndComplete.AddOrder"] = "Create",
                ["Misc.Membership.OrderAndComplete.AlterExistingOrders"] = "Edit Orders",
                ["Misc.Membership.OrderAndComplete.UpgradeMembership"] = "Upgrade Membership",
            };
        }
    }
}
