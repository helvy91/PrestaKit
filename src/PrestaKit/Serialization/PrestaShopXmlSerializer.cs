using PrestaKit.Clients.Resources;
using PrestaKit.Entities;
using PrestaKit.Entities.Common;
using PrestaKit.Errors;
using PrestaKit.Exceptions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PrestaKit.Serialization
{
    public partial class PrestaShopXmlSerializer : IPrestaShopSerializer
    {
        public string ContentType => "text/xml";
   
        public string SerializeEntity<T>(T entity) where T : PrestaShopEntity
        {
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };

            var stringBuilder = new StringBuilder();
            using var xmlWriter = XmlWriter.Create(stringBuilder, settings);
            ResourceMeta<T>.Serializer.Serialize(xmlWriter, entity, EmptyNamespaces);

            var entityElement = XElement.Parse(stringBuilder.ToString());
            NormalizeXmlValuesForSerialization<T>(entityElement);
            var envelope = new XElement("prestashop", entityElement);
            

            return envelope.ToString();
        }

        public T DeserializeSingle<T>(string xml) where T : PrestaShopEntity
        {
            var document = ParseOrThrow(xml);
            var entityNode = document.Root?.Elements().FirstOrDefault()
                ?? throw new PrestaShopSerializationException(
                    "Expected an entity element inside <prestashop>, but none was found.", xml);

            NormalizeXmlValuesForDeserialization<T>(entityNode);
            var entity = DeserializeEntity<T>(entityNode!);
            return entity;
        }

        public List<T> DeserializeList<T>(string xml) where T : PrestaShopEntity
        { 
            var entityNodes = GetListNodes(xml);

            var result = new List<T>();
            foreach (var entityNode in entityNodes.Elements())
            {
                NormalizeXmlValuesForDeserialization<T>(entityNode);
                var entity = DeserializeEntity<T>(entityNode);
                result.Add(entity);
            }

            return result;
        }

        public List<long> DeserializeIds(string xml)
        {
            var entityNodes = GetListNodes(xml);

            return entityNodes.Elements()
                .Select(x => (long?)x.Attribute("id"))
                .Where(x => x.HasValue)
                .Select(x => x!.Value)
                .ToList();
        }

        public List<PrestaShopError> DeserializeErrors(string xml)
        {
            XDocument document;
            try
            {
                document = XDocument.Parse(xml);
            }
            catch
            {
                return []; 
            }

            var errorNodes = document.Root?.Elements()?
                .FirstOrDefault(x => x.Name.LocalName == "errors");
            if (errorNodes == null)
            {
                return [];
            }

            var result = new List<PrestaShopError>();
            foreach (var errorNode in errorNodes.Elements())
            {
                var codeElement = errorNode.Element("code");
                var messageElement = errorNode.Element("message");

                var error = new PrestaShopError
                {
                    Code = codeElement != null &&
                        int.TryParse(codeElement.Value, out var code) ? code : null,

                    Message = messageElement?.Value
                };
                result.Add(error);
            }

            return result;
        }

        private static XElement GetListNodes(string xml)
        {
            var document = ParseOrThrow(xml);
            return document.Root?.Elements().FirstOrDefault()
                ?? throw new PrestaShopSerializationException(
                    "Expected resource list node inside <prestashop>, but none was found.", xml);
        }

        private static T DeserializeEntity<T>(XNode entityNode) where T : PrestaShopEntity
        {
            try
            {
                using var reader = entityNode.CreateReader();
                return (T)ResourceMeta<T>.Serializer.Deserialize(reader)!;
            }
            catch (Exception ex)
            {
                var detail = ex.InnerException?.Message ?? ex.Message;
                throw new PrestaShopSerializationException(
                    $"Failed to deserialize as {typeof(T).Name}: {detail}", entityNode.ToString(), ex);
            }
        }

        private static void NormalizeXmlValuesForDeserialization<T>(XElement entityNode) where T : PrestaShopEntity
        {
            NormalizeEmptyValueFields(entityNode);
            NormalizeDates<T>(entityNode, DateFormat.Deserialization);
        }

        private static void NormalizeXmlValuesForSerialization<T>(XElement entityNode) where T : PrestaShopEntity
        {
            NormalizeDates<T>(entityNode, DateFormat.Serialization);
            NormalizeBooleansForSerialization<T>(entityNode);
        }

        private static void NormalizeEmptyValueFields(XElement entityNode)
        {
            var emptyNodes = entityNode.Elements()
                .Where(x => x.Name.LocalName != "associations"
                         && !x.HasElements
                         && string.IsNullOrWhiteSpace(x.Value))
                .ToList();

            foreach (var node in emptyNodes)
            {
                node.Remove();
            }
        }

        private static void NormalizeDates<T>(XElement entityNode, DateFormat target) where T : PrestaShopEntity
        {
            var separator = target == DateFormat.Deserialization ? 'T' : ' ';

            var dateFields = GetDateFieldNames(typeof(T));
            var dateNodes = entityNode.Elements()
                .Where(x => dateFields.Contains(x.Name.LocalName))
                .ToList();

            foreach (var node in dateNodes)
            {
                var value = node.Value;

                if (string.IsNullOrWhiteSpace(value) || value.StartsWith("0000-00-00", StringComparison.InvariantCulture))
                {
                    node.Remove();
                    continue;
                }

                var match = PrestaShopDateTime().Match(value);
                if (match.Success)
                {
                    node.Value = $"{match.Groups[1].Value}{separator}{match.Groups[2].Value}";
                }
            }
        }

        private static void NormalizeBooleansForSerialization<T>(XElement entityNode)
        {
            var boolFields = GetBoolFieldNames(typeof(T));   
            foreach (var node in entityNode.Elements()
                .Where(x => boolFields.Contains(x.Name.LocalName))
                .ToList())
            {
                node.Value = node.Value switch
                {
                    "true" => "1",
                    "false" => "0",
                    _ => node.Value
                };
            }
        }

        private static XDocument ParseOrThrow(string xml)
        {
            try
            {
                return XDocument.Parse(xml);
            }
            catch (XmlException ex)
            {
                throw new PrestaShopSerializationException("Response was not well-formed XML.", xml, ex);
            }
        }

        #region Prestashop quirk handling data

        // Empty namespaces while serializing
        private static readonly XmlSerializerNamespaces EmptyNamespaces = CreateEmptyNamespaces();
        private static XmlSerializerNamespaces CreateEmptyNamespaces()
        {
            var nameSpaces = new XmlSerializerNamespaces();
            nameSpaces.Add(string.Empty, string.Empty);
            return nameSpaces;
        }

        // DateTime format normalization
        [GeneratedRegex(@"^(\d{4}-\d{2}-\d{2})[T ](\d{2}:\d{2}:\d{2})", RegexOptions.Compiled)]
        private static partial Regex PrestaShopDateTime();

        /* Enum for separating serialization and deserialization format */
        private enum DateFormat { Serialization, Deserialization }

        /* Property cache */
        private static readonly Dictionary<Type, HashSet<string>> DateFieldsByType = new();

        private static HashSet<string> GetDateFieldNames(Type type)
        {
            if (DateFieldsByType.TryGetValue(type, out var cached))
            {
                return cached;
            }

            var dateFields = type.GetProperties()
                .Where(x => x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?))
                .Select(x => x.GetCustomAttribute<XmlElementAttribute>()?.ElementName ?? x.Name)
                .ToHashSet();

            DateFieldsByType[type] = dateFields;
            return dateFields;
        }

        // Bool fields normalization

        /* Property cache */
        private static readonly Dictionary<Type, HashSet<string>> BoolFieldsByType = new();

        private static HashSet<string> GetBoolFieldNames(Type type)
        {
            if (BoolFieldsByType.TryGetValue(type, out var cached))
                return cached;

            var boolFields = type.GetProperties()
                .Where(x => x.PropertyType == typeof(bool) || x.PropertyType == typeof(bool?))
                .Select(x => x.GetCustomAttribute<XmlElementAttribute>()?.ElementName ?? x.Name)
                .ToHashSet();

            BoolFieldsByType[type] = boolFields;
            return boolFields;
        }

        #endregion
    }
}
