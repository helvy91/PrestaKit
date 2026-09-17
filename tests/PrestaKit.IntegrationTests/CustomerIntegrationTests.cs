using PrestaKit.Entities;
using PrestaKit.IntegrationTests.Fixtures;
using PrestaKit.Querying;
using Shouldly;

namespace PrestaKit.IntegrationTests
{
    [Collection("PrestaShop")]
    public class CustomerIntegrationTests : IntegrationTestBase
    {
        public CustomerIntegrationTests(PrestaShopContainerFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CanCreateCustomer()
        {
            var created = await Client.Customers.AddAsync(NewCustomer());

            created.Id.ShouldNotBeNull();
            created.Id!.Value.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CanGetCustomer()
        {
            var created = await Client.Customers.AddAsync(NewCustomer());
            var fetched = await Client.Customers.GetAsync(created.Id!.Value);

            fetched.FirstName.ShouldBe(created.FirstName);
            fetched.LastName.ShouldBe(created.LastName);
            fetched.Email.ShouldBe(created.Email);
        }

        [Fact]
        public async Task CanUpdateCustomer()
        {
            var created = await Client.Customers.AddAsync(NewCustomer());

            created.LastName = $"Updated";
            await Client.Customers.UpdateAsync(created);

            var fetched = await Client.Customers.GetAsync(created.Id!.Value);
            fetched.LastName.ShouldBe($"Updated");
        }

        [Fact]
        public async Task CanDeleteCustomer()
        {
            var created = await Client.Customers.AddAsync(NewCustomer());
            await Client.Customers.DeleteAsync(created.Id!.Value);

            await Should.ThrowAsync<Exceptions.PrestaShopNotFoundException>(
                () => Client.Customers.GetAsync(created.Id.Value));
        }

        [Fact]
        public async Task CanListCustomers()
        {
            var prefix = nameof(CanListCustomers);

            for (int i = 0; i < 10; i++)
            {
                await Client.Customers.AddAsync(NewCustomer(prefix));
            }

            var results = await Client.Customers.ListAsync(
                new Query<Customer>().WhereBeginsWith(c => c.Email, prefix));

            results.Count.ShouldBe(10);
            results.ShouldAllBe(x => x.Email!.StartsWith(prefix));
        }

        [Fact]
        public async Task CanFindCustomer()
        {
            var created = await Client.Customers.AddAsync(NewCustomer());
            var found = await Client.Customers.FindAsync(created.Id!.Value);

            found.ShouldNotBeNull();
            found!.Email.ShouldBe(created.Email);
        }

        [Fact]
        public async Task FindReturnsNullWhenCustomerDoesNotExist()
        {
            var found = await Client.Customers.FindAsync(999999);
            found.ShouldBeNull();
        }

        [Fact]
        public async Task GetThrowsWhenCustomerDoesNotExist()
        {
            await Should.ThrowAsync<Exceptions.PrestaShopNotFoundException>(
                () => Client.Customers.GetAsync(999999));
        }

        private static Customer NewCustomer(string? prefix = null) => new()
        {
            FirstName = $"First",
            LastName = $"Last",
            Email = $"{prefix ?? ""}{Guid.NewGuid():N}@test.local",
            Password = "TestPass123",
        };
    }
}