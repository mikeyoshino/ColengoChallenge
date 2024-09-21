namespace ColengoChallenge.Api.Dto
{
    public class ImageDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Alt { get; set; }
        public string? Original { get; set; }
        public string? Large { get; set; }
        public string? MediumLarge { get; set; }
        public string? Medium { get; set; }
        public string? MediumSmall { get; set; }
        public string? Small { get; set; }
        public string? Thumbnail { get; set; }
        public string? SmallThumbnail { get; set; }
        public object? Preset { get; set; }
    }
}
