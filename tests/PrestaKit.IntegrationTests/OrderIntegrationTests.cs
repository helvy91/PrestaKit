using PrestaKit.Entities;
using PrestaKit.Entities.Associations;
using PrestaKit.Exceptions;
using PrestaKit.IntegrationTests.Fixtures;
using PrestaKit.Querying;
using Shouldly;

namespace PrestaKit.IntegrationTests
{
    public class OrderIntegrationTests : IntegrationTestBase
    {
        private const int CountryId = 1;

        public OrderIntegrationTests(PrestaShopContainerFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CanGetOrder()
        {
            var order = await CreateOrderAsync();

            var fetched = await Client.Orders.GetAsync(order.Id!.Value);

            fetched.Id.ShouldBe(order.Id);
        }

        [Fact]
        public async Task CanListOrdersWithQuery()
        {
            var prefix = nameof(CanListOrdersWithQuery);

            var orders = new List<Order>();
            for (int i = 0; i < 10; i++)
            {
                orders.Add(await CreateOrderAsync(prefix));
            }

            var query = new Query<Order>();
            query.WhereBeginsWith(x => x.Payment, prefix);
            var results = await Client.Orders.ListAsync(query);

            results.Count.ShouldBe(10);
            results.ShouldAllBe(x => x.Payment!.StartsWith(prefix));
        }

        [Fact]
        public async Task CanUpdateOrderState()
        {
            var order = await CreateOrderAsync();

            order.CurrentState = 3;
            await Client.Orders.UpdateAsync(order);

            var fetched = await Client.Orders.GetAsync(order.Id!.Value);
            fetched.CurrentState.ShouldBe(3);
        }

        [Fact]
        public async Task CanCreateOrder()
        {
            var order = await CreateOrderAsync();

            order.Id.ShouldNotBeNull();
            order.Id!.Value.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetThrowsWhenOrderDoesNotExist()
        {
            await Should.ThrowAsync<PrestaShopNotFoundException>(
                () => Client.Orders.GetAsync(999999));
        }

        private async Task<Order> CreateOrderAsync(string? prefix = null)
        {
            await EnsureCountryActiveAsync(CountryId);
            var customer = await CreateCustomerAsync();
            var address = await CreateAddressAsync(customer.Id!.Value);
            var product = await CreateProductAsync();
            var cart = await CreateCartAsync(customer.Id.Value, address.Id!.Value, product.Id!.Value);

            var order = new Order
            {
                IdCustomer = customer.Id.Value,
                IdAddressDelivery = address.Id.Value,
                IdAddressInvoice = address.Id.Value,
                IdCart = cart.Id!.Value,
                IdCurrency = 1,               
                IdLang = 1,
                IdCarrier = 1,                
                CurrentState = 1,             
                Payment = $"{prefix ?? ""}TestPayment",
                Module = "ps_checkpayment",   
                TotalPaid = 9.99m,
                TotalPaidReal = 0m,
                TotalProducts = 9.99m,
                TotalProductsWt = 9.99m,
                ConversionRate = 1m,
            };

            return await Client.Orders.AddAsync(order);
        }

        private async Task<Customer> CreateCustomerAsync() =>
            await Client.Customers.AddAsync(new Customer
            {
                FirstName = "Test",
                LastName = "Customer",
                Email = $"order_{Guid.NewGuid():N}@test.local",
                Password = "TestPass123",
            });

        private async Task<Address> CreateAddressAsync(long customerId) =>
            await Client.Resource<Address>().AddAsync(new Address
            {
                IdCustomer = customerId,
                IdCountry = CountryId,              
                Alias = "Test Address",
                FirstName = "Test",
                LastName = "Customer",
                Address1 = "1 Test Street",
                City = "Test City",
                PostCode = "12345",
            });

        private async Task<Product> CreateProductAsync()
        {
            var product = new Product
            {
                Name = "Order Test Product",
                Price = 9.99m,
                State = 1,
                ProductType = "standard",
                IdCategoryDefault = 2,
                Active = true,
                Associations = new ProductAssociations { Categories = [2] }
            };
            return await Client.Products.AddAsync(product);
        }

        private async Task<Cart> CreateCartAsync(long customerId, long addressId, long productId)
        {
            var cart = new Cart
            {
                IdCustomer = customerId,
                IdAddressDelivery = addressId,
                IdAddressInvoice = addressId,
                IdCurrency = 1,
                IdLang = 1,
                Associations = new CartAssociations
                {
                    CartRows = { new CartRow { IdProduct = productId, Quantity = 1 } }
                }
            };
            return await Client.Resource<Cart>().AddAsync(cart);
        }

        private async Task EnsureCountryActiveAsync(long countryId)
        {
            var country = await Client.Resource<Country>().GetAsync(countryId);
            if (country.Active != true)
            {
                country.Active = true;
                await Client.Resource<Country>().UpdateAsync(country);
            }
        }
    }
}