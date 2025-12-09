namespace Frontend.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Length { get; set; }
        public double Width { get; set; }
        public List<Guid> ComponentIds { get; set; } = [];
        public List<Guid> OrderIds { get; set; } = [];
    }
}
