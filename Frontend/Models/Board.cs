namespace Frontend.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Length { get; set; }
        public double Width { get; set; }
        public Guid OrderId { get; set; }
        public List<Component> Components { get; set; } = new();
    }
}
