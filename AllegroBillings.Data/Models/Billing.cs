namespace AllegroBillings.Data.Models
{
    public class Billing
    {
        public string Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public BillingType Type { get; set; }
        public Offer Offer { get; set; }
        public BillingValue Value { get; set; }
        public Tax Tax { get; set; }
        public Balance Balance { get; set; }
        public Order Order { get; set; }
    }
}
