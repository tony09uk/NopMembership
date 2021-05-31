using Nop.Core;

namespace Ts.Plugin.Misc.Membership.Domain
{
    public class Membership : BaseEntity
    {
        public int MaxProducts { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int OrderRequestCount { get; set; }
        public int OrderFulfilledCount { get; set; }
    }
}
