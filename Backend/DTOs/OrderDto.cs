namespace Backend.DTOs
{
    public class OrderDto
    {
        public Guid? Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }

        public List<BoardDto> Boards { get; set; } = new();
    }
}
