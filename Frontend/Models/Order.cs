namespace Frontend.Models
{
    public class Order
    {
        public Guid? Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public List<Guid> BoardIds { get; set; } = [];
    }
}
