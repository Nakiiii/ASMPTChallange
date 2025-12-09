namespace Backend.Models
{
    public class Board
    {
        // Primary Key
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = string.Empty;
        public int Length { get; set; }
        public int Width { get; set; }

        public List<Order> Orders { get; set; } = [];
        public List<Component> Components { get; set; } = [];
    }
}
