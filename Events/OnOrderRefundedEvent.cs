using Nop.Core;
using Nop.Core.Domain.Logging;
using Nop.Core.Domain.Orders;
using Nop.Services.Catalog;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = Nop.Services.Logging.ILogger;

namespace Nop.Plugin.Misc.Events.Events
{
    public class OnOrderRefundedEvent : IConsumer<OrderRefundedEvent>
    {
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IWidgetPluginManager _widgetPluginManager;
        private readonly ILogger _logger;    
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerService _customerService;

        public OnOrderRefundedEvent(

            ISettingService settingService,
            IStoreContext storeContext,
            ILogger logger,
            IWidgetPluginManager widgetPluginManager,
            
            IOrderService orderService,
            IProductService productService,
            ICategoryService categoryService,
            IGenericAttributeService genericAttributeService,
            ICustomerService customerService
            )
        {
            _logger = logger;
            _settingService = settingService;
            _storeContext = storeContext;
            _widgetPluginManager = widgetPluginManager;
          
            _orderService = orderService;
            _productService = productService;
            _categoryService = categoryService;
            _genericAttributeService = genericAttributeService;
            _customerService = customerService;
        }
        private async Task<bool> IsPluginActive()
        {
            return await _widgetPluginManager.IsPluginActiveAsync(EventsDefaults.PluginSystemName);
        }

        public async Task HandleEventAsync(OrderRefundedEvent eventMessage)
        {
            var order = eventMessage.Order;
            await ProcessCustomFunctionAsync(order);
        }
        private async Task ProcessCustomFunctionAsync(Order order)
        {
            try
            {
                //load settings for a chosen store scope
                //var store = await _storeService.GetStoreByIdAsync(order.StoreId) ?? await _storeContext.GetCurrentStoreAsync();        
                var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
                          

                var itemList = await _orderService.GetOrderItemsAsync(order.Id);

                foreach (var orderItem in itemList)
                {
                    var product = await _productService.GetProductByIdAsync(orderItem.ProductId);

                    if ((product.Sku != null)) 
                    {
                        // call any function you need to excecute the action after OrderRefunded
                        // await CustomFunction(order, orderItem, product);                       
                    }

                }

            }
            catch (Exception ex)
            {
                await _logger.InsertLogAsync(LogLevel.Error, " Error in processing  Event ", ex.ToString());
            }
        }

    }
}
