using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Web.Controllers;
using Nop.Web.Factories;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Models.Catalog;
using System.Linq;
using Ts.Plugin.Misc.Membership.Factories;
using Ts.Plugin.Misc.Membership.Models.Customers;

namespace Nop.Plugin.Misc.MySmartCards.Controllers
{
    public partial class MembershipController : BasePublicController
    {
        private readonly ICatalogModelFactory _catalogModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IMembershipListFactory _membershipListFactory;
        private readonly IWorkContext _workContext;

        public MembershipController(
            ICatalogModelFactory catalogModelFactory,
            ICategoryService categoryService,
            IMembershipListFactory membershipListFactory,
            IWorkContext workContext)
        {
            _catalogModelFactory = catalogModelFactory;
            _categoryService = categoryService;
            _membershipListFactory = membershipListFactory;
            _workContext = workContext;
        }

        [HttpsRequirement]
        public IActionResult List()
        {
            CategorySimpleModel membershipCategory = _catalogModelFactory.PrepareCategorySimpleModels()
                                        .FirstOrDefault(x => x.Name.ToLower() == "memberships");

            if(membershipCategory == null)
            {
                return View("~/Plugins/Misc.Membership/Views/Membership/List.cshtml");
            }

            Category category = _categoryService.GetCategoryById(membershipCategory.Id);

            CategoryModel categoryModel = _catalogModelFactory.PrepareCategoryModel(category, new CatalogPagingFilteringModel());

            MembershipList model = _membershipListFactory.PrepareMembershipList(_workContext.CurrentCustomer.Id);

            //USE ABOVE MODEL TO:
            //ENABLE/DISABLE SELECTEABLE MEMBERSHIPS

            return View("~/Plugins/Misc.Membership/Views/Membership/List.cshtml", model);
        }

        [HttpPost]
        public IActionResult List(object model)
        {
            var redirectActionName = "Membership";

            return RedirectToAction(redirectActionName);
        }
    }
}
