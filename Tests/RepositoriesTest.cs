using AllegroBillings.BusinessLogic.Dtos;
using AllegroBillings.Data;
using AllegroBillings.Data.Implementations;
using AllegroBillings.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AllegroBillings.Tests
{
    [TestFixture]
    public class BillingRepositoryTests
    {
        private BillingRepository _billingRepository;
        private BillingContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<BillingContext>()
                .UseInMemoryDatabase(databaseName: "BillingTest")
                .Options;
            _context = new BillingContext(options);
            _billingRepository = new BillingRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllOffersAsync_ReturnsOffers()
        {
            // Arrange
            var offers = new List<Offer>
            {
                new Offer { Id = "1", Name = "Offer1" },
                new Offer { Id = "2", Name = "Offer2" }
            };
            await _context.Offers.AddRangeAsync(offers);
            await _context.SaveChangesAsync();

            // Act
            var result = await _billingRepository.GetAllOffersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task SaveBillingEntriesAsync_SavesEntries()
        {
            // Arrange
            var billingDtos = new List<BillingDto>
            {
                new BillingDto
                {
                    Id = "1",
                    OccurredAt = DateTime.Now,
                    Type = new BillingTypeDto { Id = "Type1", Name = "TypeName1" },
                    Offer = new OfferDto { Id = "1", Name = "Offer1" },
                    Value = new BillingValueDto { Amount = "100", Currency = "PLN" },
                    Tax = new TaxDto { Percentage = "20", Annotation = "Annotation" },
                    Balance = new BalanceDto { Amount = "1000", Currency = "PLN" },
                    Order = new OrderDto { Id = "OrderId1" }
                }
            };

            // Act
            await _billingRepository.SaveBillingEntriesAsync(billingDtos);

            // Assert
            var savedEntries = await _context.Billings.ToListAsync();
            Assert.That(savedEntries, Is.Not.Null);
            Assert.That(savedEntries.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetOrdersFromOrderTableAsync_ReturnsOrders()
        {
            // Arrange
            var orders = new List<OrderTable>
            {
                new OrderTable { OrderId = "1" },
                new OrderTable { OrderId = "2" }
            };
            await _context.OrderTables.AddRangeAsync(orders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _billingRepository.GetOrdersFromOrderTableAsync();

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
