using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Ts.Plugin.Misc.Membership.Services
{
    public class WidgetSelectorService : IWidgetSelectorService
    {
        private Dictionary<string, string> _map = new Dictionary<string, string> {
            { PublicWidgetZones.AccountNavigationBefore, MembershipPluginConstants.MEMBERSHIP_NAVIGATION_VIEW_COMPONENT_NAME },
            { PublicWidgetZones.ProductDetailsInsideOverviewButtonsBefore, MembershipPluginConstants.MEMBERSHIP_ORDER_AND_COMPLETE_BUTTON_COMPONENT_NAME }
        };

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return _map[widgetZone];
        }

        public IList<string> GetUsedPublicWidgetZones()
        {
            return _map.Keys.ToList();
        }
    }
}
