namespace ColengoChallenge.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserIdentifier { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}
