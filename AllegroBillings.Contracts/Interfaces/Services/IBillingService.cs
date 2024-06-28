using AllegroBillings.BusinessLogic.Dtos;

namespace AllegroBillings.Contracts.Interfaces.Services
{
    public interface IBillingService
    {
        Task<IEnumerable<BillingDto>> FetchBillingEntriesAsync(Dictionary<string, string> queryParams);
    }
}
