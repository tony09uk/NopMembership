using System.Collections.Generic;
using Ts.Plugin.Misc.Membership.Models.Customers;

namespace Ts.Plugin.Misc.Membership.Factories
{
    public interface IMembershipListFactory
    {
        MembershipList PrepareMembershipList(int customerId);
        Dictionary<int, bool> CreateAllowedPurchaseList(Domain.Membership selectedMembership);
    }
}