using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Web.Models.Catalog;
using System.Collections.Generic;
using System.Linq;
using Ts.Plugin.Misc.Membership.Factories;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership.Filters
{
    public class ProductDetailsResultFilter : MembershipResultFilterBase
    {
        private readonly ICategoryService _categoryService;

        public ProductDetailsResultFilter(
            IMembershipService membershipService,
            IMembershipListFactory membershipListFactory,
            ICategoryService categoryService,
            IWorkContext workContext) : 
                base(
                    membershipService, 
                    membershipListFactory, 
                    workContext)
        {
            _categoryService = categoryService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (IsTargetRoute(context, "Product", "ProductDetails"))
            {
                var result = context.Result as ViewResult;
                var productDetailsModel = result.Model as ProductDetailsModel;

                IList<ProductCategory> productCategories = _categoryService.GetProductCategoriesByProductId(productDetailsModel.Id);

                var productCategory = productCategories.ToList().FirstOrDefault();
                if (productCategory != null)
                {
                    Category category = _categoryService.GetCategoryById(productCategory.CategoryId);
                    if (category.Name.ToLower() == MembershipPluginConstants.MEMBERSHIP_CATEGORY_NAME.ToLower())
                    {
                        Dictionary<int, bool> allowedPurchaseList = GetAllowedPurchasedList();

                        if (shouldDisableBuyButton(allowedPurchaseList, productDetailsModel.Id))
                        {
                            productDetailsModel.AddToCart.DisableBuyButton = true;
                        }
                    } 
                    else
                    {
                        var membership = _membershipService.GetMembershipByCustomerId(_workContext.CurrentCustomer.Id);

                        if(membership != null)
                        {
                            //IS ACTIVE??
                            var remainingProducts = membership.MaxProducts - membership.OrderRequestCount;
                            if (remainingProducts == 0)
                            {
                                productDetailsModel.AddToCart.DisableBuyButton = true;
                            }
                        }
                    }
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
