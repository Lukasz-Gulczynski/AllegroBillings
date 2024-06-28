using AllegroBillings.Contracts.Interfaces.Repositories;
using AllegroBillings.Contracts.Interfaces.Services;

namespace AllegroBillingEntries.BussinesLogic.Commands
{
    public class OfferBillingServiceCommand : BillingServiceCommandBase
    {
        public OfferBillingServiceCommand(IBillingRepository billingRepository, IBillingService billingService)
            : base(billingRepository, billingService)
        {
        }

        public override async Task ExecuteAsync()
        {
            var offersIds = await _billingRepository.GetAllOffersAsync();

            foreach (var offerId in offersIds)
            {
                var filters = new Dictionary<string, string>
                {
                    { "marketplaceId", "allegro-pl" },
                    { "offer.id", offerId },
                    { "limit", "100" },
                    { "offset", "0" }
                };
                await ProcessBillingsAsync(filters);
            }
        }
    }
}
