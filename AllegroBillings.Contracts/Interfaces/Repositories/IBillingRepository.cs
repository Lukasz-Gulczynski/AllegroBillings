using AllegroBillings.BusinessLogic.Dtos;

namespace AllegroBillings.Contracts.Interfaces.Repositories
{
    public interface IBillingRepository
    {
        Task<List<string>> GetAllOffersAsync();
        Task SaveBillingEntriesAsync(IEnumerable<BillingDto> billingEntries);
        Task<List<string>> GetOrdersFromOrderTableAsync();
    }
}
