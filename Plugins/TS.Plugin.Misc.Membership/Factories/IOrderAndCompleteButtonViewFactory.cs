using Ts.Plugin.Misc.Membership.Models.Order;

namespace Ts.Plugin.Misc.Membership.Factories
{
    public interface IOrderAndCompleteButtonViewFactory
    {
        OrderAndCompleteButtonModel PrepareOrderAndCompleteButtonModel(int productId);
    }
}