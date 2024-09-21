namespace ColengoChallenge.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ParentName { get; set; }
        public string? UserIdentifier { get; set; }
        public string? CollectionName { get; set; }
        public int CollectionId { get; set; }
        public string? ThumbnailImage { get; set; }

        public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}
