using PrestaKit.Entities;
using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Common;
using PrestaKit.Serialization;

Console.WriteLine("Hello, World!");

var product = new Product()
{
    Name = new TranslatedField() { Values = new() { new() { LanguageId = 1, Value = "Chair" } } },
    Price = 19.5M,
    Associations = new ProductAssociations() { Categories = new() { new() { Id = 1 } } }
};

var serializer = new PrestaShopXmlSerializer();
var serialized = serializer.SerializeEntity(product);
Console.ReadLine();
