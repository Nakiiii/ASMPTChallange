namespace Frontend.Models
{
    public class Component
    {
        public Guid? Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
