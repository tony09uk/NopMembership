using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Web.Factories;
using Nop.Web.Models.Catalog;
using System.Collections.Generic;
using System.Linq;
using Ts.Plugin.Misc.Membership.Models.Customers;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership.Factories
{
    public class MembershipListFactory : IMembershipListFactory
    {
        private readonly ILocalizationService _localizationService;
        private readonly ICatalogModelFactory _catalogModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IMembershipService _membershipService;

        public MembershipListFactory(
            ILocalizationService localizationService,
            ICatalogModelFactory catalogModelFactory,
            ICategoryService categoryService,
            IMembershipService membershipService)
        {
            _localizationService = localizationService;
            _catalogModelFactory = catalogModelFactory;
            _categoryService = categoryService;
            _membershipService = membershipService;
        }

        public MembershipList PrepareMembershipList(int customerId)
        {
            CategorySimpleModel membershipCategory = _catalogModelFactory.PrepareCategorySimpleModels()
                            .FirstOrDefault(x => x.Name.ToLower() == MembershipPluginConstants.MEMBERSHIP_CATEGORY_NAME.ToLower());

            if (membershipCategory == null)
            {
                return null;
            }

            Category category = _categoryService.GetCategoryById(membershipCategory.Id);

            CategoryModel categoryModel = 
                _catalogModelFactory.PrepareCategoryModel(category, new CatalogPagingFilteringModel());

            Domain.Membership selectedMembership = _membershipService.GetMembershipByCustomerId(customerId);

            var allowedPriceList = CreateAllowedPurchaseList(selectedMembership);

            return new MembershipList
            {
                CategoryModel = categoryModel,
                SelectedMembershipId = selectedMembership?.ProductId,
                UpgradableMembershipList = allowedPriceList
            };
        }

        public Dictionary<int, bool> CreateAllowedPurchaseList(Domain.Membership selectedMembership)
        {
            IList<ProductOverviewModel> memberships = _membershipService.GetAllMemberships();
            var selectedMembershipPrice = 0.0M;
            
            if(selectedMembership != null)
            {
                selectedMembershipPrice = memberships.First(x => x.Id == selectedMembership.ProductId).ProductPrice.PriceValue;
            }

            var list = new Dictionary<int, bool>();
            foreach(var membership in memberships)
            {
                list.Add(membership.Id, membership.ProductPrice.PriceValue > selectedMembershipPrice);
            }
            return list;
        }
    }
}
