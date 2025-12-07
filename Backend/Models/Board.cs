namespace Backend.Models
{
    public class Board
    {
        // Primary Key
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = string.Empty;
        public int Length { get; set; }
        public int Width { get; set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public ICollection<Component> Components { get; set; } = [];
    }
}
