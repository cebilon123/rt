using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Rc.Services.Fraud.Application.DTO;
using Rc.Services.Fraud.Application.Services;
using Rc.Services.Fraud.Infrastructure.Services.AntiFraud;

namespace Rc.Services.Fraud.Tests.Services.AntiFraud
{
    public class NewClientNigeriaRuleTests
    {
        private Mock<IOrdersApi> _ordersApiMock;
        private NewClientNigeriaRule _nigeriaRule;

        [SetUp]
        public void SetUp()
        {
            _ordersApiMock = new Mock<IOrdersApi>();
            _ordersApiMock.Setup(o => o.GetOrdersForEmail("client@test.com"))
                .ReturnsAsync(new List<OrderDto>
                {
                    new()
                    {
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
                        Address = new AddressDto()
                        {
                            City = "Test",
                            Country = "Test",
                            Street = "test",
                            ZipCode = 1234
                        }
                    }
                });
            _nigeriaRule = new NewClientNigeriaRule(_ordersApiMock.Object);
        }

        [Test]
        public async Task Test_IsValid_NotNigeria_ReturnsTrue()
        {
            var result = await _nigeriaRule.IsValid(new OrderDto()
            {
                Amount = 9999,
                Address = new AddressDto()
                {
                    Country = "Poland"
                }
            });

            Assert.That(result);
        }

        [Test]
        public async Task Test_IsValid_NigeriaAmountLessThan1000_ReturnsTrue()
        {
            var result = await _nigeriaRule.IsValid(new OrderDto()
            {
                Amount = 99,
                Address = new AddressDto()
                {
                    Country = "Nigeria"
                }
            });

            Assert.That(result);
        }

        [Test]
        public async Task Test_IsValid_NigeriaAmount1000OrMoreNewOrder_ReturnsFalse()
        {
            var result = await _nigeriaRule.IsValid(new OrderDto()
            {
                Email = "cl@test.com",
                Amount = 9999,
                Address = new AddressDto()
                {
                    Country = "Nigeria"
                }
            });

            Assert.That(result == false);

            result = await _nigeriaRule.IsValid(new OrderDto()
            {
                Email = "clt@test.com",
                Amount = 1001,
                Address = new AddressDto()
                {
                    Country = "Nigeria"
                }
            });

            Assert.That(result == false);
        }
        
        [Test]
        public async Task Test_IsValid_NigeriaAmount1000OrMoreNotNewOrder_ReturnsTrue()
        {
            var result = await _nigeriaRule.IsValid(new OrderDto()
            {
                Email = "client@test.com",
                Amount = 9999,
                Address = new AddressDto()
                {
                    Country = "Nigeria"
                }
            });

            Assert.That(result);

            result = await _nigeriaRule.IsValid(new OrderDto()
            {
                Email = "client@test.com",
                Amount = 1001,
                Address = new AddressDto()
                {
                    Country = "Nigeria"
                }
            });

            Assert.That(result);
        }
    }
}