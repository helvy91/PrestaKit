using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class ProductAssociations
    {
        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<EntityRef> Categories { get; set; } = [];

        [XmlArray("images")]
        [XmlArrayItem("image")]
        public List<EntityRef> Images { get; set; } = [];

        [XmlArray("combinations")]
        [XmlArrayItem("combination")]
        public List<EntityRef> Combinations { get; set; } = [];

        [XmlArray("product_option_values")]
        [XmlArrayItem("product_option_value")]
        public List<EntityRef> ProductOptionValues { get; set; } = [];

        [XmlArray("product_features")]
        [XmlArrayItem("product_feature")]
        public List<ProductFeatureRef> ProductFeatures { get; set; } = [];

        [XmlArray("tags")]
        [XmlArrayItem("tag")]
        public List<EntityRef> Tags { get; set; } = [];

        [XmlArray("stock_availables")]
        [XmlArrayItem("stock_available")]
        public List<StockAvailableRef> StockAvailables { get; set; } = [];

        [XmlArray("attachments")]
        [XmlArrayItem("attachment")]
        public List<EntityRef> Attachments { get; set; } = [];

        [XmlArray("accessories")]
        [XmlArrayItem("product")]
        public List<EntityRef> Accessories { get; set; } = [];

        [XmlArray("product_bundle")]
        [XmlArrayItem("product")]
        public List<BundleItemRef> ProductBundle { get; set; } = [];

        public bool ShouldSerializeCategories() => Categories.Count > 0;
        public bool ShouldSerializeImages() => Images.Count > 0;
        public bool ShouldSerializeCombinations() => Combinations.Count > 0;
        public bool ShouldSerializeProductOptionValues() => ProductOptionValues.Count > 0;
        public bool ShouldSerializeProductFeatures() => ProductFeatures.Count > 0;
        public bool ShouldSerializeTags() => Tags.Count > 0;
        public bool ShouldSerializeStockAvailables() => StockAvailables.Count > 0;
        public bool ShouldSerializeAttachments() => Attachments.Count > 0;
        public bool ShouldSerializeAccessories() => Accessories.Count > 0;
        public bool ShouldSerializeProductBundle() => ProductBundle.Count > 0;
    }

    public sealed class BundleItemRef
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("id_product_attribute")]
        public long IdProductAttribute { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }
    }

    public sealed class StockAvailableRef
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("id_product_attribute")]
        public long IdProductAttribute { get; set; }
    }

    public sealed class ProductFeatureRef
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("id_feature_value")]
        public long IdFeatureValue { get; set; }
    }
}
