using Nop.Services.Localization;
using Ts.Plugin.Misc.Membership.Models.Customers;

namespace Ts.Plugin.Misc.Membership.Factories
{
    public class MembershipNavigationFactory : IMembershipNavigationFactory
    {
        private readonly ILocalizationService _localizationService;

        public MembershipNavigationFactory(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public MembershipNavigationModel PrepareMembershipNavigationModel(int selectedTabId)
        {
            var model = new MembershipNavigationModel();

            model.MembershipNavigationItems.Add(new MembershipNavigationItemModel
            {
                RouteName = MembershipPluginConstants.MEMBERSHIP_ROUTE_NAME,
                Title = "Membership",//_localizationService.GetResource("membership-navigation"),
                Tab = MembershipNavigationEnum.Membership
                //ItemClass = "update-handwriting"
            });

            model.SelectedTab = (MembershipNavigationEnum)selectedTabId;

            return model;
        }
    }
}
