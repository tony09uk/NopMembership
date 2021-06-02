using Microsoft.Extensions.Logging;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Orders;
using Nop.Services.Payments;

namespace Ts.Plugin.Misc.Membership.Services
{
    public class OrderAndCompleteService : IOrderAndCompleteService
    {
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILogger _logger;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IPaymentService _paymentService;
        private readonly IStoreContext _storeContext;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly IOrderService _orderService;

        public OrderAndCompleteService(
            IGenericAttributeService genericAttributeService,
            ILogger logger,
            IOrderProcessingService orderProcessingService,
            IPaymentService paymentService,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IWorkContext workContext,
            IOrderService orderService)
        {
            _genericAttributeService = genericAttributeService;
            _logger = logger;
            _orderProcessingService = orderProcessingService;
            _paymentService = paymentService;
            _storeContext = storeContext;
            _webHelper = webHelper;
            _workContext = workContext;
            _orderService = orderService;
        }

        public void Run(int productId)
        {
            //create order

            //mark order as paid

            var processPaymentRequest = new ProcessPaymentRequest();

            _paymentService.GenerateOrderGuid(processPaymentRequest);
            processPaymentRequest.StoreId = _storeContext.CurrentStore.Id;
            processPaymentRequest.CustomerId = _workContext.CurrentCustomer.Id;
            processPaymentRequest.PaymentMethodSystemName =
                _genericAttributeService.GetAttribute<string>(
                    _workContext.CurrentCustomer,
                    NopCustomerDefaults.SelectedPaymentMethodAttribute,
                    _storeContext.CurrentStore.Id);
            //HttpContext.Session.Set<ProcessPaymentRequest>("OrderPaymentInfo", processPaymentRequest);
            PlaceOrderResult placeOrderResult = _orderProcessingService.PlaceOrder(processPaymentRequest);
            if (placeOrderResult.Success)
            {
                //HttpContext.Session.Set<ProcessPaymentRequest>("OrderPaymentInfo", null);
                //var postProcessPaymentRequest = new PostProcessPaymentRequest
                //{
                //    Order = placeOrderResult.PlacedOrder
                //};
                //_paymentService.PostProcessPayment(postProcessPaymentRequest);
                _orderProcessingService.MarkOrderAsPaid(placeOrderResult.PlacedOrder);

            }

            //foreach (var error in placeOrderResult.Errors)
            //    model.Warnings.Add(error);
        }
    }
}
