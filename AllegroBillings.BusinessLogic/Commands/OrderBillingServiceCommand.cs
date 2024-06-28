using AllegroBillings.Contracts.Interfaces.Repositories;
using AllegroBillings.Contracts.Interfaces.Services;

namespace AllegroBillingEntries.BussinesLogic.Commands
{
    public class OrderBillingServiceCommand : BillingServiceCommandBase
    {
        public OrderBillingServiceCommand(IBillingRepository billingRepository, IBillingService billingService)
            : base(billingRepository, billingService)
        {
        }

        public override async Task ExecuteAsync()
        {
            var ordersIds = await _billingRepository.GetOrdersFromOrderTableAsync();

            foreach (var orderId in ordersIds)
            {
                var filters = new Dictionary<string, string>
                {
                    { "marketplaceId", "allegro-pl" },
                    { "order.id", "39dfb9b0-1667-1667-11ee-a9ed-5cb2e49c7660" },
                    { "limit", "100" },
                    { "offset", "0" }
                };
                await ProcessBillingsAsync(filters);
            }
        }
    }
}
