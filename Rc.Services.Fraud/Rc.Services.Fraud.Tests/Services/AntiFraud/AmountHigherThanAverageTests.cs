using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Rc.Services.Fraud.Application.DTO;
using Rc.Services.Fraud.Application.Services;
using Rc.Services.Fraud.Infrastructure.Services.AntiFraud;

namespace Rc.Services.Fraud.Tests.Services.AntiFraud
{
    public class AmountHigherThanAverageTests
    {
        private Mock<IOrdersApi> _ordersApiMock;
        private AmountHigherThanAverage _amountHigherThanAverageTests;

        [SetUp]
        public void SetUp()
        {
            _ordersApiMock = new Mock<IOrdersApi>();
            _ordersApiMock.Setup(o => o.FetchAll())
                .ReturnsAsync(new List<OrderDto>
                {
                    new()
                    {
                        Amount = 5,
                        Address = new AddressDto()
                        {
                            City = "Test",
                            Country = "Test",
                            Street = "test",
                            ZipCode = 1234
                        }
                    },
                    new()
                    {
                        Amount = 5,
                        Address = new AddressDto()
                        {
                            City = "Test",
                            Country = "Test",
                            Street = "test",
                            ZipCode = 1234
                        }
                    },
                    new()
                    {
                        Amount = 6,
                        Address = new AddressDto()
                        {
                            City = "Test",
                            Country = "Test",
                            Street = "test",
                            ZipCode = 1234
                        }
                    },
                    new()
                    {
                        Amount = 6,
                        Address = new AddressDto()
                        {
                            City = "Test",
                            Country = "Test",
                            Street = "test",
                            ZipCode = 1234
                        }
                    }
                });
            
            _amountHigherThanAverageTests = new AmountHigherThanAverage(_ordersApiMock.Object);
        }

        [Test]
        public async Task Test_IsValid_AmountLowerThanAverage_ReturnsTrue()
        {
            var result = await _amountHigherThanAverageTests.IsValid(new OrderDto
            {
                Amount = 1
            });
            
            Assert.That(result);
        }
        
        [Test]
        public async Task Test_IsValid_AmountHigherThanAverage_ReturnsFalse()
        {
            var result = await _amountHigherThanAverageTests.IsValid(new OrderDto
            {
                Amount = 122
            });
            
            Assert.That(result == false);
        }
    }
}