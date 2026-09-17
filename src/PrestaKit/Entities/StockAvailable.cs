using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("stock_available")]
[ApiResource("stock_availables")]
public class StockAvailable : PrestaShopEntity
{
    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_product_attribute")]
    public long? IdProductAttribute { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; }

    [XmlElement("quantity")]
    public int? Quantity { get; set; }

    [XmlElement("depends_on_stock")]
    public bool? DependsOnStock { get; set; }

    [XmlElement("out_of_stock")]
    public int? OutOfStock { get; set; }

    [XmlElement("location")]
    public string? Location { get; set; }

    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdProductAttribute() => IdProductAttribute.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializeQuantity() => Quantity.HasValue;
    public bool ShouldSerializeDependsOnStock() => DependsOnStock.HasValue;
    public bool ShouldSerializeOutOfStock() => OutOfStock.HasValue;
    public bool ShouldSerializeLocation() => !string.IsNullOrEmpty(Location);
}