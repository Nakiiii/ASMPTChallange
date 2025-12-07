using Backend.Data;
using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Backend.Tests.Tests
{
    public class OrderRepositoryTests
    {
        private readonly DbContextOptions<ChallangeDbContext> _dbOptions;

        public OrderRepositoryTests()
        {
            _dbOptions = new DbContextOptionsBuilder<ChallangeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenOrderExists()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<OrderRepository>>();

            using var context = new ChallangeDbContext(_dbOptions);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Description = "Test Order",
                OrderDate = DateTime.Now
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var repo = new OrderRepository(context, loggerMock.Object);

            // Act
            order.Description = "Updated Description";
            var result = await repo.UpdateOrderAsync(order.Id, order);

            // Assert
            Assert.True(result);

            // Verify that the order was updated in the database
            var updatedOrder = await context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
            Assert.Equal("Updated Description", updatedOrder!.Description);
        }
    }
}
