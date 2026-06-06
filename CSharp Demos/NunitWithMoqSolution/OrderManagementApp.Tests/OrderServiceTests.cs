using Moq;
using OrderManagementApp.Models;
using OrderManagementApp.Interfaces;
using OrderManagementApp.Services;

namespace OrderManagementApp.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PlaceOrder_WhenStockIsAvailable_ShouldReturnSuccessMessage()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Laptop",
                AvailableQuantity = 10,
                Price = 55000
            };

            var productRepositoryMock = new Mock<IProductRespository>();
            var emailServiceMaock = new Mock<IEmailService>();

            productRepositoryMock.Setup(repo => repo.GetProductById(1))
                .Returns(product);

            var orderService = new OrderService(
                productRepositoryMock.Object, emailServiceMaock.Object);

            //Act 

            string result = orderService.PlaceOrder(1, 2, "custoemr1@mail.com");

            //Assert
            Assert.That(result, Is.EqualTo("Order placed successfully"));

            productRepositoryMock.Verify(repo => repo.UpdateStock(1, 2), Times.Once);
            emailServiceMaock.Verify(email => email.SendOrderConfirmation("custoemr1@mail.com", "Laptop"), Times.Once);
        }

        [Test]
        public void PlaceOrder_WhenProductDoesNotExist_ShouldReturnProductNotFound()
        {
            //Arrange
             var productRepositoryMock = new Mock<IProductRespository>();
            var emailServiceMaock = new Mock<IEmailService>();

            productRepositoryMock.Setup(repo => repo.GetProductById(99))
                .Returns((Product?)null);

            var orderService = new OrderService(
                productRepositoryMock.Object, emailServiceMaock.Object);

            //Act 

            string result = orderService.PlaceOrder(99, 1, "custoemr1@mail.com");

            //Assert
            Assert.That(result, Is.EqualTo("Product Not Found"));

            productRepositoryMock.Verify(repo => repo.UpdateStock(It.IsAny<int>(), 
                It.IsAny<int>()), Times.Never);
            emailServiceMaock.Verify(email => 
            email.SendOrderConfirmation(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void PlaceOrder_WhenStockIsInsufficient_ShouldReturnInsufficientStock()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Laptop",
                AvailableQuantity = 1,
                Price = 55000
            };

            var productRepositoryMock = new Mock<IProductRespository>();
            var emailServiceMaock = new Mock<IEmailService>();

            productRepositoryMock.Setup(repo => repo.GetProductById(1))
                .Returns(product);

            var orderService = new OrderService(
                productRepositoryMock.Object, emailServiceMaock.Object);

            //Act 

            string result = orderService.PlaceOrder(1, 5, "custoemr1@mail.com");

            //Assert
            Assert.That(result, Is.EqualTo("Insufficient Stock"));

            productRepositoryMock.Verify(repo => repo.UpdateStock(1, 2), Times.Never);
            emailServiceMaock.Verify(email => email.SendOrderConfirmation(
                It.IsAny<string>(),It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void PlaceOrder_WhenQuantityIsZero_ShouldReturnValidationMessage()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Laptop",
                AvailableQuantity = 10,
                Price = 55000
            };

            var productRepositoryMock = new Mock<IProductRespository>();
            var emailServiceMaock = new Mock<IEmailService>();

            productRepositoryMock.Setup(repo => repo.GetProductById(1))
                .Returns(product);

            var orderService = new OrderService(
                productRepositoryMock.Object, emailServiceMaock.Object);

            //Act 

            string result = orderService.PlaceOrder(1, 0, "custoemr1@mail.com");

            //Assert
            Assert.That(result, Is.EqualTo("Quantity must be greater than zero"));

            productRepositoryMock.Verify(repo => repo.UpdateStock(1, 2), Times.Never);
            emailServiceMaock.Verify(email => email.SendOrderConfirmation(
                It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}