using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class OrderAssociations
    {
        [XmlArray("order_rows")]
        [XmlArrayItem("order_row")]
        public List<OrderRow> OrderRows { get; set; } = [];
    }

    public sealed class OrderRow
    {
        [XmlElement("id")]
        public long? Id { get; set; }

        [XmlElement("product_id")]
        public long? ProductId { get; set; }

        [XmlElement("product_attribute_id")]
        public long? ProductAttributeId { get; set; }

        [XmlElement("product_quantity")]
        public int? ProductQuantity { get; set; }

        [XmlElement("product_name")]
        public string? ProductName { get; set; }

        [XmlElement("product_reference")]
        public string? ProductReference { get; set; }

        [XmlElement("product_ean13")]
        public string? ProductEan13 { get; set; }

        [XmlElement("product_isbn")]
        public string? ProductIsbn { get; set; }

        [XmlElement("product_mpn")] 
        public string? ProductMpn { get; set; }

        [XmlElement("product_upc")]
        public string? ProductUpc { get; set; }

        [XmlElement("product_price")]
        public decimal? ProductPrice { get; set; }

        [XmlElement("id_customization")]
        public long? IdCustomization { get; set; }

        [XmlElement("unit_price_tax_incl")]
        public decimal? UnitPriceTaxIncl { get; set; }

        [XmlElement("unit_price_tax_excl")]
        public decimal? UnitPriceTaxExcl { get; set; }

        public bool ShouldSerializeProductPrice() => ProductPrice.HasValue;
        public bool ShouldSerializeUnitPriceTaxIncl() => UnitPriceTaxIncl.HasValue;
        public bool ShouldSerializeUnitPriceTaxExcl() => UnitPriceTaxExcl.HasValue;
        public bool ShouldSerializeProductId() => ProductId.HasValue;
        public bool ShouldSerializeProductAttributeId() => ProductAttributeId.HasValue;
        public bool ShouldSerializeProductQuantity() => ProductQuantity.HasValue;
        public bool ShouldSerializeIdCustomization() => IdCustomization.HasValue;

        // PrestaShop read-only fields
        public bool ShouldSerializeId() => Id.HasValue && false;
        public bool ShouldSerializeProductName() => !string.IsNullOrWhiteSpace(ProductName) && false;
        public bool ShouldSerializeProductReference() => !string.IsNullOrWhiteSpace(ProductReference) && false;
        public bool ShouldSerializeProductEan13() => !string.IsNullOrWhiteSpace(ProductEan13) && false;
        public bool ShouldSerializeProductIsbn() => !string.IsNullOrWhiteSpace(ProductIsbn) && false;
        public bool ShouldSerializeProductMpn() => !string.IsNullOrWhiteSpace(ProductMpn) && false;
        public bool ShouldSerializeProductUpc() => !string.IsNullOrWhiteSpace(ProductUpc) && false;
    }
}
