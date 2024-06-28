using AllegroBillings.BusinessLogic.Dtos;
using AllegroBillings.Contracts.Interfaces.Repositories;
using AllegroBillings.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AllegroBillings.Data.Implementations
{
    public class BillingRepository : IBillingRepository
    {
        private readonly BillingContext _context;

        public BillingRepository(BillingContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllOffersAsync()
        {
            return await _context.Offers.Select(x => x.Id).ToListAsync();
        }

        public async Task SaveBillingEntriesAsync(IEnumerable<BillingDto> billingEntries)
        {
            var billingsToSave = billingEntries.Select(BillingMapper.MapToBilling);
            await _context.Billings.AddRangeAsync(billingsToSave);
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetOrdersFromOrderTableAsync()
        {
            //W tabeli brakuje informacji o tym kiedy zamówienie zostało złożone, a na tej podstawie mógłbym pobierać zamówienia np. z ostatnich 5 minut
            return await _context.OrderTables.Select(x => x.OrderId).ToListAsync();
        }
    }
}
