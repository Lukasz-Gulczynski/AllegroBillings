namespace AllegroBillings.BusinessLogic.Dtos
{
    public class BillingDto
    {
        public string Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public BillingTypeDto Type { get; set; }
        public OfferDto Offer { get; set; }
        public BillingValueDto Value { get; set; }
        public TaxDto Tax { get; set; }
        public BalanceDto Balance { get; set; }
        public OrderDto Order { get; set; }
    }
}
