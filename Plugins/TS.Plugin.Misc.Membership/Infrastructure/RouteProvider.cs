using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
    
namespace Ts.Plugin.Misc.Membership.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public int Priority => 0;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(
                MembershipPluginConstants.MEMBERSHIP_ROUTE_NAME,
                "Plugins/Membership/List",
                new { controller = "Membership", action = "List" });

            endpointRouteBuilder.MapControllerRoute(
                MembershipPluginConstants.MEMBERSHIP_ORDER_AND_COMPLETE_ROUTE_NAME,
                "Plugins/Membership/OrderAndComplete",
                new { controller = "Membership", action = "OrderAndComplete" });
        }
    }
}
