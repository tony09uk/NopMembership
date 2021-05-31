using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Ts.Plugin.Misc.Membership;
using Ts.Plugin.Misc.Membership.Factories;

namespace Nop.Plugin.Misc.MySmartCards.Components
{
    [ViewComponent(Name = MembershipPluginConstants.MEMBERSHIP_NAVIGATION_VIEW_COMPONENT_NAME)]
    public class MembershipNavigationViewComponent : NopViewComponent
    {
        private readonly IMembershipNavigationFactory _membershipNavigationFactory;

        public MembershipNavigationViewComponent(IMembershipNavigationFactory membershipNavigationFactory)
        {
            _membershipNavigationFactory = membershipNavigationFactory;
        }

        public IViewComponentResult Invoke(int selectedTabId)
        {
            var model = _membershipNavigationFactory.PrepareMembershipNavigationModel(selectedTabId);
            return View("~/Plugins/Misc.Membership/Views/Shared/Components/MembershipNavigation/Default.cshtml", model);
        }
    }
}
