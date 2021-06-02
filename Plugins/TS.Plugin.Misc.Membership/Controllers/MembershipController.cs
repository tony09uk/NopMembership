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
            _orderAndCompleteService.Run(productId);//DOES THIS WORK----->NO------>WHY?
            return View();
        }
        //STEPS TO GET IT WORKING
        //add order
        //mark as paid
        //redirect to complete page(replicate shopping cart order complete functionaility)


        //ADD ORDER
        //create ProcessPaymentRequest(example: checkoutController ln 967)
        //call OrderProcessingService.PlaceOrder


        //MARK AS PAID
        //call  _orderService.GetOrderById(id);
        //_orderProcessingService.MarkOrderAsPaid(order);
    }
}
