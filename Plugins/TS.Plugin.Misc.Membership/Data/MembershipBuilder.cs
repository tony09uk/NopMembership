using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Core.Domain.Customers;
using Nop.Data.Mapping.Builders;
using System.Data;
using Nop.Core.Domain.Catalog;

namespace Ts.Plugin.Misc.Membership.Data
{
    public class MembershipBuilder : NopEntityBuilder<Domain.Membership>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Domain.Membership.MaxProducts))
                    .AsInt32()
                .WithColumn(nameof(Domain.Membership.OrderRequestCount))
                    .AsInt32()
                .WithColumn(nameof(Domain.Membership.OrderFulfilledCount))
                    .AsInt32()
                .WithColumn(nameof(Domain.Membership.CustomerId))
                    .AsInt32()
                    .ForeignKey<Customer>()
                    .OnDelete(Rule.None)
                .WithColumn(nameof(Domain.Membership.ProductId))
                    .AsInt32()
                    .ForeignKey<Product>()
                    .OnDelete(Rule.None);
        }
    }
}
