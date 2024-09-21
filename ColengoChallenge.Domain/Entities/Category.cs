namespace ColengoChallenge.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }


        public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}
