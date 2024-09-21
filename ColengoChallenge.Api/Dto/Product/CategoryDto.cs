namespace ColengoChallenge.Api.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
    }
}
