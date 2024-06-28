using AllegroBillings.BusinessLogic.Dtos;
using AllegroBillings.Data.Models;

namespace AllegroBillings.Data.Helpers
{
    public static class BillingMapper
    {
        public static Billing MapToBilling(BillingDto dto)
        {
            return new Billing
            {
                Id = dto.Id,
                OccurredAt = dto.OccurredAt,
                Type = new BillingType
                {
                    Id = dto.Type.Id,
                    Name = dto.Type.Name
                },
                Offer = new Offer
                {
                    Id = dto.Offer.Id,
                    Name = dto.Offer.Name
                },
                Value = new BillingValue
                {
                    Amount = ConvertStringIntoDecimal(dto.Value.Amount),
                    Currency = dto.Value.Currency
                },
                Tax = new Tax
                {
                    Percentage = ConvertStringIntoDecimal(dto.Tax.Percentage),
                    Annotation = dto.Tax.Annotation
                },
                Balance = new Balance
                {
                    Amount = ConvertStringIntoDecimal(dto.Balance.Amount),
                    Currency = dto.Balance.Currency
                },
                Order = new Order
                {
                    Id = dto.Order.Id
                }
            };
        }

        private static decimal ConvertStringIntoDecimal(string valueToConvert)
        {
            return decimal.TryParse(valueToConvert, out var convertedValue) ? convertedValue : 0;
        }
    }
}
