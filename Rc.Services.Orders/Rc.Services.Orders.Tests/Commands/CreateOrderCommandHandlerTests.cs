using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Rc.Services.Orders.Application.Events;
using Rc.Services.Orders.Application.Handlers.Commands;
using Rc.Services.Orders.Application.Handlers.Commands.Handlers;
using Rc.Services.Orders.Application.Services;
using Rc.Services.Orders.Core.Domain;
using Rc.Services.Orders.Core.Exceptions;
using Rc.Services.Orders.Core.Repositories;

namespace Rc.Services.Orders.Tests.Commands
{
    public class CreateOrderCommandHandlerTests
    {
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IEventProcessor> _eventProcessorMock;
        private Mock<IMessageBroker> _messageBrokerMock;
        private CreateOrderCommandHandler _commandHandler;

        [SetUp]
        public void SetUp()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _eventProcessorMock = new Mock<IEventProcessor>();
            _messageBrokerMock = new Mock<IMessageBroker>();

            _commandHandler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _eventProcessorMock.Object,
                _messageBrokerMock.Object);
        }


        [TestCase("test@gmail.com", 3, "test", "test", "test", 1234, "test", 4, true, false)]
        // I don't know if creating order for amount of 0 is good idea
        [TestCase("test@gmail.com", 0, "test", "test", "test", 1234, "test", 4, true, false)]
        [TestCase("test@gmail.com", -1, "test", "test", "test", 1234, "test", 4, false, true)]
        [TestCase("test@gmail.com", 1, "test", "test", "test", 0, "test", 4, false, true)]
        [TestCase("test@gmail.com", 4, "test", "test", "test", 1234, "test", 0, false, true)]
        [TestCase("", 2, "test", "test", "test", 1234, "test", 4, false, true)]
        public async Task Test_HandleAsync(string email, decimal amount, string street, string country, string city,
            int zipCode, string productName, int productQuantity, bool createsOrder, bool throwsException)
        {
            var orderCreated = false;
            _orderRepositoryMock.Setup(m => m.Insert(It.IsAny<Order>()))
                .Callback(() => { orderCreated = true; });

            if (throwsException)
                Assert.ThrowsAsync<InvalidOrderException>(async () =>
                {
                    await _commandHandler.HandleAsync(new CreateOrder
                    {
                        Address = new CreateOrder.CustomerAddress
                        {
                            City = city,
                            Country = country,
                            Street = street,
                            ZipCode = zipCode
                        },
                        Amount = amount,
                        Email = email,
                        Products = new List<CreateOrder.Product>
                        {
                            new()
                            {
                                Name = productName,
                                Quantity = productQuantity
                            }
                        }
                    });
                });
            else
                await _commandHandler.HandleAsync(new CreateOrder
                {
                    Address = new CreateOrder.CustomerAddress
                    {
                        City = city,
                        Country = country,
                        Street = street,
                        ZipCode = zipCode
                    },
                    Amount = amount,
                    Email = email,
                    Products = new List<CreateOrder.Product>
                    {
                        new()
                        {
                            Name = productName,
                            Quantity = productQuantity
                        }
                    }
                });

            Assert.That(orderCreated == createsOrder);
        }
    }
}