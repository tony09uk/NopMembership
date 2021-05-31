using Ts.Plugin.Misc.Membership.Models.Customers;

namespace Ts.Plugin.Misc.Membership.Factories
{
    public interface IMembershipNavigationFactory
    {
        MembershipNavigationModel PrepareMembershipNavigationModel(int selectedTabId);
    }
}