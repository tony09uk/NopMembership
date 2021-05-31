using Nop.Web.Models.Catalog;
using System.Collections.Generic;

namespace Ts.Plugin.Misc.Membership.Services
{
    public interface IMembershipService
    {
        IList<ProductOverviewModel> GetAllMemberships();
        Domain.Membership GetMembershipByCustomerId(int customerId);
        Domain.Membership GetMembershipById(int id);
        Domain.Membership GetMembershipByProductId(int productId);
        void Insert(Domain.Membership membership);
        void Update(Domain.Membership membership);
    }
}