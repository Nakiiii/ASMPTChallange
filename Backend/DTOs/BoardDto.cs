namespace Backend.DTOs
{
    public class BoardDto
    {
        public Guid? Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Length { get; set; }
        public int Width { get; set; }

        public List<Guid> ComponentIds { get; set; } = [];
        public List<Guid> OrderIds { get; set; } = [];
    }
}
