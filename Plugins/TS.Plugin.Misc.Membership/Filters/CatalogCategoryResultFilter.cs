using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using Nop.Web.Models.Catalog;
using System.Collections.Generic;
using Ts.Plugin.Misc.Membership.Factories;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership.Filters
{
    public class CatalogCategoryResultFilter : MembershipResultFilterBase
    {
        public CatalogCategoryResultFilter(
            IMembershipService membershipService,
            IMembershipListFactory membershipListFactory,
            IWorkContext workContext) :
                base(
                    membershipService,
                    membershipListFactory,
                    workContext)
        { }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (IsTargetRoute(context, "Catalog", "Category"))
            {
                var result = context.Result as ViewResult;
                var model = result.Model as CategoryModel;

                Dictionary<int, bool> allowedPurchaseList = GetAllowedPurchasedList();

                foreach (var product in model.Products)
                {
                    if(shouldDisableBuyButton(allowedPurchaseList, product.Id))
                    {
                        product.ProductPrice.DisableBuyButton = true;
                    }
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
