using System.Collections.Generic;

namespace Ts.Plugin.Misc.Membership
{
    public class MemberShipPluginResources
    {
        public static Dictionary<string, string> Get()
        {
            return new Dictionary<string, string>
            {
                ["ts.customer-navigation-membership"] = "Membership",
                ["Misc.Membership.Account.Subscriptions"] = "Membership",
            };
        }
    }
}
