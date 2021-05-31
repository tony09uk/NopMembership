using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Plugins;
using System.Collections.Generic;
using Nop.Web.Framework.Infrastructure;
using Nop.Core;
using Nop.Services.Localization;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership
{
    public class MembershipPlugin : BasePlugin, IMiscPlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly IWidgetSelectorService _widgetSelectorService;
        private readonly ILocalizationService _localizationService;

        public MembershipPlugin(
            IWebHelper webHelper,
            IWidgetSelectorService widgetSelectorService,
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _widgetSelectorService = widgetSelectorService;
            _localizationService = localizationService;
        }

        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return _widgetSelectorService.GetWidgetViewComponentName(widgetZone);
        }

        public IList<string> GetWidgetZones()
        {
            return _widgetSelectorService.GetUsedPublicWidgetZones();
        }

        public override void Install()
        {
            _localizationService.AddPluginLocaleResource(PluginResources());

            base.Install();
        }        
        
        public override void Uninstall()
        {
            foreach (var resource in PluginResources())
            {
                _localizationService.DeletePluginLocaleResources(resource.Key);
            }

            base.Uninstall();
        }

        private Dictionary<string, string> PluginResources()
        {
            return MemberShipPluginResources.Get();
        }
    }
}
