namespace ColengoChallenge.Api.Dto
{
    public class TagDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ParentName { get; set; }
        public string? UserIdentifier { get; set; }
        public string? CollectionName { get; set; }
        public int CollectionId { get; set; }
        public string? ThumbnailImage { get; set; }
    }
}
