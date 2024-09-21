namespace ColengoChallenge.Domain.Entities
{
    public class OriginalPrice
    {
        public int Id { get; set; }
        public string? Currency { get; set; }
        public double? Amount { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}
