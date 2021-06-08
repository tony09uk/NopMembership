using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Payments;
using System;
using System.Linq;

namespace Ts.Plugin.Misc.Membership.Services
{
    public class OrderAndCompleteService : IOrderAndCompleteService
    {
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILogger _logger;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IPaymentService _paymentService;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly IOrderService _orderService;
        private readonly OrderSettings _orderSettings;

        public OrderAndCompleteService(
            IGenericAttributeService genericAttributeService,
            IOrderProcessingService orderProcessingService,
            IPaymentService paymentService,
            IStoreContext storeContext,
            ILocalizationService localizationService,
            IWebHelper webHelper,
            IWorkContext workContext,
            IOrderService orderService,
            OrderSettings orderSettings)
        {
            _genericAttributeService = genericAttributeService;
            //_logger = logger;
            _orderProcessingService = orderProcessingService;
            _paymentService = paymentService;
            _storeContext = storeContext;
            _localizationService = localizationService;
            _webHelper = webHelper;
            _workContext = workContext;
            _orderService = orderService;
            _orderSettings = orderSettings;
        }

        public int Run(int productId)
        {
            if (!IsMinimumOrderPlacementIntervalValid(_workContext.CurrentCustomer))
                throw new Exception(_localizationService.GetResource("Checkout.MinOrderPlacementInterval"));

            //place order
            var processPaymentRequest = new ProcessPaymentRequest();
            _paymentService.GenerateOrderGuid(processPaymentRequest);
            processPaymentRequest.StoreId = _storeContext.CurrentStore.Id;
            processPaymentRequest.CustomerId = _workContext.CurrentCustomer.Id;
            processPaymentRequest.PaymentMethodSystemName = _genericAttributeService.GetAttribute<string>(_workContext.CurrentCustomer,
                NopCustomerDefaults.SelectedPaymentMethodAttribute, _storeContext.CurrentStore.Id);

            var placeOrderResult = _orderProcessingService.PlaceOrder(processPaymentRequest);
            //todo: work out what failure states exist in the above and return a boolean from here
            if (placeOrderResult.Success)
            {
                return placeOrderResult.PlacedOrder.Id;
            } 
            else
            {
                return -1;
            }
        }


        protected virtual bool IsMinimumOrderPlacementIntervalValid(Customer customer)
        {
            //prevent 2 orders being placed within an X seconds time frame
            if (_orderSettings.MinimumOrderPlacementInterval == 0)
                return true;

            var lastOrder = _orderService.SearchOrders(storeId: _storeContext.CurrentStore.Id,
                customerId: _workContext.CurrentCustomer.Id, pageSize: 1)
                .FirstOrDefault();
            if (lastOrder == null)
                return true;

            var interval = DateTime.UtcNow - lastOrder.CreatedOnUtc;
            return interval.TotalSeconds > _orderSettings.MinimumOrderPlacementInterval;
        }
    }
}
