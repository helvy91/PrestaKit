using PrestaKit.Entities;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using PrestaKit.Exceptions;
using PrestaKit.Serialization;
using Shouldly;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PrestaKit.Tests.Serialization
{
    [XmlRoot("dummy_class")]
    [ApiResource("dummies")]
    public class DummyClassToSerialize : PrestaShopEntity
    {
        [XmlElement("dummy_name")] public string? Name { get; set; }
        [XmlElement("dummy_surname")] public string? Surname { get; set; }
    }

    [Trait("Category", "Unit")]
    public class PrestaShopSerializerTests
    {
        private readonly PrestaShopXmlSerializer _sut = new PrestaShopXmlSerializer();

        [Fact]
        public void SerializeEntity_ShouldProperlySerializeEntity()
        {
            // Arrange
            const int id = 1;
            const string name = "TestName";
            const string surname = "TestSurname";

            var dummy = new DummyClassToSerialize()
            {
                Id = id,
                Name = name,
                Surname = surname
            };

            // Act
            var serialized = _sut.SerializeEntity(dummy);

            // Assert
            var doc = XDocument.Parse(serialized);
            var entity = doc.Root!.Elements().First();

            doc.Root.Name.LocalName.ShouldBe("prestashop");
            entity.Name.LocalName.ShouldBe("dummy_class");
            entity.Element("dummy_name")!.Value.ShouldBe(name);
            entity.Element("dummy_surname")!.Value.ShouldBe(surname);
        }

        [Fact]
        public void DeserializeSingle_ShouldProperlyDeserializeEntity()
        {
            // Arrange
            const string singleProductXml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
                <product>
                <id>5</id>
                <price>19.900000</price>
                <active>1</active>
                <reference>CHAIR-01</reference>
                <name>
                    <language id="1"><![CDATA[Chair]]></language>
                    <language id="2"><![CDATA[Krzeslo]]></language>
                </name>
                <associations>
                    <categories nodeType="category" api="categories">
                    <category><id>2</id></category>
                    <category><id>9</id></category>
                    </categories>
                    <stock_availables nodeType="stock_available" api="stock_availables">
                    <stock_available><id>77</id><id_product_attribute>0</id_product_attribute></stock_available>
                    </stock_availables>
                </associations>
                </product>
            </prestashop>
            """;

            // Act
            var deserialized = _sut.DeserializeSingle<Product>(singleProductXml);

            // Assert
            deserialized.Id.ShouldBe(5);
            deserialized.Price.ShouldBe(19.9M);
            deserialized.Active.ShouldBe(true);
            deserialized.Reference.ShouldBe("CHAIR-01");
            deserialized.Name!.Values[0].Value.ShouldBe("Chair");
            deserialized.Name!.Values[0].LanguageId.ShouldBe(1);
            deserialized.Name!.Values[1].Value.ShouldBe("Krzeslo");
            deserialized.Name!.Values[1].LanguageId.ShouldBe(2);
            deserialized.Associations!.Categories[0].Id.ShouldBe(2);
            deserialized.Associations!.Categories[1].Id.ShouldBe(9);
            deserialized.Associations!.StockAvailables[0].Id.ShouldBe(77);
            deserialized.Associations!.StockAvailables[0].IdProductAttribute.ShouldBe(0);
        }

        [Fact]
        public void DeserializeSingle_ShouldThrow_WhenThereIsNoEntityNode()
        {
            // Arrange
            const string invalidXml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
            </prestashop>
            """;

            // Act & Assert
            var deserialized = Should.Throw(
                () => _sut.DeserializeSingle<Product>(invalidXml),
                typeof(PrestaShopSerializationException));
        }

        [Fact]
        public void DeserializeList_ShouldProperlyDeserializeListOfEntities()
        {
            // Arrange
            const string productListXml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
              <products>
                <product>
                  <id>1</id>
                  <price>10.000000</price>
                  <name><language id="1"><![CDATA[Table]]></language></name>
                </product>
                <product>
                  <id>2</id>
                  <price>25.500000</price>
                  <name><language id="1"><![CDATA[Lamp]]></language></name>
                </product>
              </products>
            </prestashop>
            """;

            // Act
            var deserializedCollection = _sut.DeserializeList<Product>(productListXml);

            // Assert
            deserializedCollection.Count.ShouldBe(2);

            var product1 = deserializedCollection[0];
            var product2 = deserializedCollection[1];

            product1.Id.ShouldBe(1);
            product1.Price.ShouldBe(10);
            product1.Name!.Values[0].Value.ShouldBe("Table");

            product2.Id.ShouldBe(2);
            product2.Price.ShouldBe(25.5M);
            product2.Name!.Values[0].Value.ShouldBe("Lamp");
        }

        [Fact]
        public void DeserializeList_ShouldThrow_WhenThereIsNoResourceAggregateNode()
        {
            // Arrange
            const string invalidXml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
            </prestashop>
            """;

            // Act & Assert
            var deserialized = Should.Throw(
                () => _sut.DeserializeList<Product>(invalidXml),
                typeof(PrestaShopSerializationException));
        }

        [Fact]
        public void DeserializeIds_ShouldProperlyDeserializeListOfIds()
        {
            // Arrange
            const string idsListXml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
              <products>
                <product id="1" xlink:href="https://shop.example.com/api/products/1"/>
                <product id="2" xlink:href="https://shop.example.com/api/products/2"/>
                <product id="3" xlink:href="https://shop.example.com/api/products/3"/>
              </products>
            </prestashop>
            """;

            // Act
            var deserializedIds = _sut.DeserializeIds(idsListXml);

            // Assert
            deserializedIds.Count.ShouldBe(3);

            deserializedIds[0].ShouldBe(1);
            deserializedIds[1].ShouldBe(2);
            deserializedIds[2].ShouldBe(3);
        }

        [Fact]
        public void DeserializeIds_ShouldThrow_WhenThereIsNoAggregateNode()
        {
            // Arrange
            const string invalidXml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
            </prestashop>
            """;

            // Act & Assert
            var deserialized = Should.Throw(
                () => _sut.DeserializeIds(invalidXml),
                typeof(PrestaShopSerializationException));
        }

        [Fact]
        public void DeserializeIds_ShouldThrow_WhenThereIsMalformedXml()
        {
            // Arrange
            const string invalidXml = """
            <?xns:xlink="http://www.w3.org/1999/xlink">
            /adas<s>554bd
            </prestashop
            """;

            // Act & Assert
            var deserialized = Should.Throw(
                () => _sut.DeserializeIds(invalidXml),
                typeof(PrestaShopSerializationException));
        }

        [Fact]
        public void DeserializeErrors_ShouldProperlyDeserializeListOfErrors()
        {
            // Arrange
            const string errorsListXml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
                <errors>
                <error>
                    <code><![CDATA[89]]></code>
                    <message><![CDATA[The email is invalid.]]></message>
                </error>
                </errors>
            </prestashop>
            """;

            // Act
            var deserializedErrors = _sut.DeserializeErrors(errorsListXml);

            // Assert
            deserializedErrors.Count.ShouldBe(1);

            var error = deserializedErrors[0];
            error.Code.ShouldBe(89);
            error.Message.ShouldBe("The email is invalid.");
        }

        [Fact]
        public void DeserializeErrors_ShouldReturnEmptyList_WhenThereIsNoErrorsNode()
        {
            // Arrange
            const string invalidXml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
            </prestashop>
            """;

            // Act
            var deserializedErrors = _sut.DeserializeErrors(invalidXml);

            // Assert
            deserializedErrors.ShouldBeEmpty();
        }

        [Fact]
        public void DeserializeErrors_ShouldReturnEmptyList_WhenThereIsMalformedXml()
        {
            // Arrange
            const string invalidXml = "<html><b</body>502 </html>";

            // Act
            var deserializedErrors = _sut.DeserializeErrors(invalidXml);

            // Assert
            deserializedErrors.ShouldBeEmpty();
        }
    }
}
