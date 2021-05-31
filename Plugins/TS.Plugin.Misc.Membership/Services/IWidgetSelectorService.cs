using System.Collections.Generic;

namespace Ts.Plugin.Misc.Membership.Services
{
    public interface IWidgetSelectorService
    {
        string GetWidgetViewComponentName(string widgetZone);
        public IList<string> GetUsedPublicWidgetZones();

    }
}