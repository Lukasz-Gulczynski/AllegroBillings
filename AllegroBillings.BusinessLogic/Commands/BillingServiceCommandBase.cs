using AllegroBillings.Contracts.Interfaces.Repositories;
using AllegroBillings.Contracts.Interfaces.Services;

namespace AllegroBillingEntries.BussinesLogic.Commands
{
    public abstract class BillingServiceCommandBase
    {
        protected readonly IBillingRepository _billingRepository;
        protected readonly IBillingService _billingService;

        protected BillingServiceCommandBase(IBillingRepository billingRepository, IBillingService billingService)
        {
            _billingRepository = billingRepository;
            _billingService = billingService;
        }

        public abstract Task ExecuteAsync();

        protected async Task ProcessBillingsAsync(Dictionary<string, string> filters)
        {
            var billingEntries = await _billingService.FetchBillingEntriesAsync(filters);
            await _billingRepository.SaveBillingEntriesAsync(billingEntries);
        }
    }
}
