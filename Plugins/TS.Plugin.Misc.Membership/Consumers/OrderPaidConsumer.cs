using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Orders;
using Nop.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership.Consumers
{
    public class OrderPaidConsumer : BasePlugin, IConsumer<OrderPaidEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMembershipService _membershipService;
        private readonly ISpecificationAttributeService _specificationAttributeService;

        public OrderPaidConsumer(
            IOrderService orderService,
            ICustomerService customerService,
            IProductService productService,
            ICategoryService categoryService,
            IMembershipService membershipService,
            ISpecificationAttributeService specificationAttributeService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
            _categoryService = categoryService;
            _membershipService = membershipService;
            _specificationAttributeService = specificationAttributeService;
        }

        public void HandleEvent(OrderPaidEvent eventMessage)
        {
            IList<OrderItem> orderItems = _orderService.GetOrderItems(eventMessage.Order.Id);
            OrderItem orderItem = orderItems[0];
            ProductCategory productCategory = _categoryService.GetProductCategoriesByProductId(orderItem.ProductId).FirstOrDefault();
            Category category = _categoryService.GetCategoryById(productCategory.CategoryId);

            if (category.Name.ToLower() != MembershipPluginConstants.MEMBERSHIP_CATEGORY_NAME.ToLower())
            {
                return;
            }

            IList<ProductSpecificationAttribute> productSpecificationAttributes = _specificationAttributeService.GetProductSpecificationAttributes(orderItem.ProductId);
            SpecificationAttributeOption specificationAttributeOption = _specificationAttributeService.GetSpecificationAttributeOptionById(productSpecificationAttributes[0].SpecificationAttributeOptionId);
            SpecificationAttribute specificationAttribute = _specificationAttributeService.GetSpecificationAttributeById(specificationAttributeOption.SpecificationAttributeId);

            if (MembershipPluginConstants.MEMBERSHIP_PRODUCT_LIMIT_SPECIFICATION_ATTRIBUTE != specificationAttribute.Name)
            {
                return;
            }

            ThrowWhenRequiredItemNullOrZero(eventMessage, orderItem);

            if (!int.TryParse(specificationAttributeOption.Name, out var maxProducts))
            {
                throw new ArgumentNullException($"{MembershipPluginConstants.MEMBERSHIP_PRODUCT_LIMIT_SPECIFICATION_ATTRIBUTE} specificationAttribute Name could not be parsed as an interger");
            }

            var selectedMembership = _membershipService.GetMembershipByCustomerId(eventMessage.Order.CustomerId);
            var isUpgrading = selectedMembership != null;

            if(isUpgrading)
            {
                selectedMembership.MaxProducts = maxProducts;
                _membershipService.Update(selectedMembership);
            } else
            {
                _membershipService.Insert(CreateMembership(maxProducts, eventMessage.Order.CustomerId, orderItem.ProductId));
            }
        }

        private void ThrowWhenRequiredItemNullOrZero(OrderPaidEvent eventMessage, OrderItem orderItem)
        {
            if (eventMessage.Order.CustomerId == 0)
            {
                throw new ArgumentNullException("Event order message had a customerId of 0");
            }

            if (orderItem.ProductId == 0)
            {
                throw new ArgumentNullException("Order item productId was 0");
            }
        }

        private Domain.Membership CreateMembership(int maxProducts, int customerId, int productId)
        {
            return new Domain.Membership
            {
                MaxProducts = maxProducts,
                OrderRequestCount = 0,
                OrderFulfilledCount = 0,
                CustomerId = customerId,
                ProductId = productId
            };
        }
    }
}
