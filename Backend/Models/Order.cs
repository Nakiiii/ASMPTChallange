namespace Backend.Models
{
    public class Order
    {
        // Primary Key
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }

        public List<Board> Boards { get; set; } = [];
    }
}
