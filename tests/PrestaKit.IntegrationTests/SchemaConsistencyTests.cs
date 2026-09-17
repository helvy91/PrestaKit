using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using PrestaKit.IntegrationTests.Fixtures;
using Shouldly;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PrestaKit.IntegrationTests
{
   
    public class SchemaConsistencyTests : IDisposable
    {
        private readonly HttpClient _http;

        public SchemaConsistencyTests()
        {
            _http = new HttpClient { BaseAddress = new Uri("http://localhost:8080/api/") };
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{PrestaShopContainerFixture.ApiKey}:")));
        }

        public static IEnumerable<object[]> Entities()
        {
            var entityTypes = typeof(PrestaShopEntity).Assembly
                .GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && t.IsSubclassOf(typeof(PrestaShopEntity))
                            && t.GetCustomAttribute<ApiResourceAttribute>() is not null);

            foreach (var type in entityTypes)
            {
                var resource = type.GetCustomAttribute<ApiResourceAttribute>()!.Name;
                yield return new object[] { type, resource };
            }
        }

        [Theory(Skip = "Manual schema audit — grant all webservice permissions and run explicitly on pre-created local instance (image in tools)")]
        [MemberData(nameof(Entities))]
        public async Task EntityFieldsMatchPrestaShopSchema(Type entityType, string resource)
        {
            var xml = await _http.GetStringAsync($"{resource}?schema=synopsis");
            var doc = XDocument.Parse(xml);

            var entityNode = doc.Root?.Elements().FirstOrDefault()
                ?? throw new InvalidOperationException($"No schema for '{resource}'. Body: {xml}");

            var schemaFields = entityNode.Elements()
                .Where(e => e.Name.LocalName != "associations")
                .Select(e => e.Name.LocalName)
                .ToHashSet();

            var entityFields = entityType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.GetCustomAttribute<XmlElementAttribute>()?.ElementName)
                .Where(name => name is not null && name != "associations" && name != "id") 
                .Select(name => name!)
                .ToHashSet();

            var missing = schemaFields.Except(entityFields).ToList();
            var extra = entityFields.Except(schemaFields).ToList();

            (missing.Count == 0 && extra.Count == 0).ShouldBeTrue(
                $"{entityType.Name} vs '{resource}':\n" +
                (missing.Count > 0 ? $"  MISSING (schema has, entity lacks): {string.Join(", ", missing)}\n" : "") +
                (extra.Count > 0 ? $"  EXTRA (entity has, schema lacks): {string.Join(", ", extra)}" : ""));
        }

        public void Dispose()
        {
            _http.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}