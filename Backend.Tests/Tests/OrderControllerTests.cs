using AutoMapper;
using Backend.Controllers;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Backend.Tests.Tests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<OrderController>> _loggerMock;
        private readonly OrderController _controller;

        public OrdersControllerTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<OrderController>>();

            _controller = new OrderController(_orderServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var dto = new OrderDto { Id = Guid.NewGuid(), Description = "Test Order" };
            var wrongId = Guid.NewGuid();

            // Act
            var result = await _controller.Update(wrongId, dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID mismatch", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var dto = new OrderDto { Id = orderId, Description = "Test Order" };

            _orderServiceMock.Setup(s => s.GetOrderByIdAsync(orderId))
                .ReturnsAsync((Order?)null);

            // Act
            var result = await _controller.Update(orderId, dto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsNoContent_WhenUpdateSucceeds()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var dto = new OrderDto { Id = orderId, Description = "Test Order" };
            var existingOrder = new Order { Id = orderId, Description = "Old Order" };

            _orderServiceMock.Setup(s => s.GetOrderByIdAsync(orderId))
                .ReturnsAsync(existingOrder);
            _orderServiceMock.Setup(s => s.UpdateOrderAsync(orderId, existingOrder))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(orderId, dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsServerError_WhenUpdateFails()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var dto = new OrderDto { Id = orderId, Description = "Test Order" };
            var existingOrder = new Order { Id = orderId, Description = "Old Order" };

            _orderServiceMock.Setup(s => s.GetOrderByIdAsync(orderId))
                .ReturnsAsync(existingOrder);
            _orderServiceMock.Setup(s => s.UpdateOrderAsync(orderId, existingOrder))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Update(orderId, dto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
