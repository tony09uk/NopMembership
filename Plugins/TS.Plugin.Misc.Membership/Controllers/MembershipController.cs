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
using Ts.Plugin.Misc.Membership.Services;

namespace Nop.Plugin.Misc.MySmartCards.Controllers
{
    public partial class MembershipController : BasePublicController
    {
        private readonly ICatalogModelFactory _catalogModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IMembershipListFactory _membershipListFactory;
        private readonly IWorkContext _workContext;
        private readonly IOrderAndCompleteService _orderAndCompleteService;

        public MembershipController(
            ICatalogModelFactory catalogModelFactory,
            ICategoryService categoryService,
            IMembershipListFactory membershipListFactory,
            IWorkContext workContext,
            IOrderAndCompleteService orderAndCompleteService)
        {
            _catalogModelFactory = catalogModelFactory;
            _categoryService = categoryService;
            _membershipListFactory = membershipListFactory;
            _workContext = workContext;
            _orderAndCompleteService = orderAndCompleteService;
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

        [HttpsRequirement]
        public IActionResult OrderAndComplete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderAndComplete(int productId)
        {
            var orderId = _orderAndCompleteService.Run(productId);

            //todo: how should I implement the order complete message/screen?

            //todo: decrement available remaining orders if the above is successful
            //todo: redirect to success screen
            //todo: display the number of remainng available orders to user e.g. "You have created 3 of an available 5 orders"
            var protocol = Request.IsHttps ? "https://" : "http://";
            var host = Request.Host.Value;

            var location = $"{protocol}{host}/checkout/Completed?orderId={orderId}";

            //todo: message should be populate from _localizationService.GetResource("WHATEVER MESSAGE I NEED TO RETURN")
            return Json(new
            {
                success = true,
                message = "The order has been created",
                redirect = location
            });
        }
    }
}
