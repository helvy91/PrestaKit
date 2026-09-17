using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;

namespace PrestaKit.IntegrationTests.Fixtures
{
    [CollectionDefinition("PrestaShop")]
    public class PrestaShopCollectionDefinition : ICollectionFixture<PrestaShopContainerFixture> { }

    public class PrestaShopContainerFixture : IAsyncLifetime
    {
        public string BaseUrl { get; private set; } = null!;
        public const string ApiKey = "TESTKEY000000000000000000000000A";

        private INetwork _network;
        private IContainer _prestashop;
        private IContainer _mysql;

        public PrestaShopContainerFixture()
        {
            _network = new NetworkBuilder().Build();

            _mysql = new ContainerBuilder("mariadb:10.11")
                .WithNetwork(_network)
                .WithNetworkAliases("mysql")
                .WithEnvironment("MYSQL_ROOT_PASSWORD", "admin")
                .WithEnvironment("MYSQL_DATABASE", "prestashop")
                .Build();

            _prestashop = new ContainerBuilder("prestashop/prestashop:8")
                .WithNetwork(_network)
                .WithEnvironment("DB_SERVER", "mysql")
                .WithEnvironment("DB_NAME", "prestashop")
                .WithEnvironment("DB_USER", "root")
                .WithEnvironment("DB_PASSWD", "admin")
                .WithEnvironment("PS_INSTALL_AUTO", "1")
                .WithEnvironment("PS_ENABLE_SSL", "0")
                .WithEnvironment("PS_DOMAIN", "localhost")          
                .WithEnvironment("PS_HANDLE_DYNAMIC_DOMAIN", "1")
                .WithEnvironment("PS_DEV_MODE", "1")
                .WithPortBinding(80, true)
                .WithWaitStrategy(Wait.ForUnixContainer()
                    .UntilHttpRequestIsSucceeded(r => r.ForPort(80).ForPath("/robots.txt")))
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _mysql.StartAsync();
            await _prestashop.StartAsync();

            await SeedPrestaShopAsync();

            BaseUrl = $"http://localhost:{_prestashop.GetMappedPublicPort(80)}/api/";
        }

        public async Task DisposeAsync()
        {
            await _prestashop.DisposeAsync();
            await _mysql.DisposeAsync();
            await _network.DisposeAsync();
        }

        private async Task SeedPrestaShopAsync()
        {
            var sql =
                "INSERT INTO ps_configuration (name, value, date_add, date_upd) " +
                "VALUES ('PS_WEBSERVICE','1',NOW(),NOW()); " +

                $"INSERT INTO ps_webservice_account (`key`, description, active) " +
                $"VALUES ('{ApiKey}', 'test', 1); " +

                "INSERT INTO ps_webservice_permission (id_webservice_account, resource, method) " +
                "SELECT a.id_webservice_account, r.resource, m.method " +
                "FROM ps_webservice_account a " +
                "CROSS JOIN (" +
                    "SELECT 'products' AS resource " +
                    "UNION SELECT 'categories' " +
                    "UNION SELECT 'images' " +
                    "UNION SELECT 'attachments' " +
                    "UNION SELECT 'stock_availables' " +
                    "UNION SELECT 'customers' " +
                    "UNION SELECT 'manufacturers' " +
                    "UNION SELECT 'suppliers' " +
                    "UNION SELECT 'combinations' " +
                    "UNION SELECT 'addresses' " +
                    "UNION SELECT 'orders' " +
                    "UNION SELECT 'order_details' " +
                    "UNION SELECT 'order_histories' " +
                    "UNION SELECT 'order_states' " +
                    "UNION SELECT 'order_carriers' " +
                    "UNION SELECT 'carts' " +
                    "UNION SELECT 'carriers' " +
                    "UNION SELECT 'currencies' " +
                    "UNION SELECT 'countries' " +
                    "UNION SELECT 'states' " +
                    "UNION SELECT 'languages' " +
                    "UNION SELECT 'zones' " +
                    "UNION SELECT 'groups' " +
                    "UNION SELECT 'price_ranges' " +
                    "UNION SELECT 'tax_rules' " +
                    "UNION SELECT 'tax_rule_groups' " +
                    "UNION SELECT 'taxes' " +
                    "UNION SELECT 'product_features' " +
                    "UNION SELECT 'product_feature_values' " +
                    "UNION SELECT 'product_options' " +
                    "UNION SELECT 'product_option_values' " +
                    "UNION SELECT 'tags' " +
                    "UNION SELECT 'stores' " +
                    "UNION SELECT 'shops' " +
                    "UNION SELECT 'specific_prices'" +
                ") r " +
                "CROSS JOIN (SELECT 'GET' AS method UNION SELECT 'POST' UNION SELECT 'PUT' " +
                            "UNION SELECT 'DELETE' UNION SELECT 'HEAD') m " +
                $"WHERE a.`key` = '{ApiKey}'; " +

                "INSERT INTO ps_webservice_account_shop (id_webservice_account, id_shop) " +
                $"SELECT id_webservice_account, 1 FROM ps_webservice_account WHERE `key` = '{ApiKey}';";

            var result = await _mysql.ExecAsync(["mariadb", "-uroot", "-padmin", "prestashop", "-e", sql]);

            if (result.ExitCode != 0)
            {
                throw new InvalidOperationException($"Seeding failed: {result.Stderr}");
            }
                
            await _prestashop.ExecAsync(["sh", "-c", "rm -rf /var/www/html/var/cache/*"]);
        }
    }
}
