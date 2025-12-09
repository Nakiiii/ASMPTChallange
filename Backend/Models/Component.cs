namespace Backend.Models
{
    public class Component
    {
        // Primary Key
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public List<Board> Boards { get; set; } = [];
    }
}
