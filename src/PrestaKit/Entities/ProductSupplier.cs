using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("product_supplier")]
[ApiResource("product_suppliers")]
public class ProductSupplier : PrestaShopEntity
{
    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_product_attribute")]
    public long? IdProductAttribute { get; set; }

    [XmlElement("id_supplier")]
    public long? IdSupplier { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("product_supplier_reference")]
    public string? ProductSupplierReference { get; set; }

    [XmlElement("product_supplier_price_te")]
    public decimal? ProductSupplierPriceTe { get; set; }

    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdProductAttribute() => IdProductAttribute.HasValue;
    public bool ShouldSerializeIdSupplier() => IdSupplier.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeProductSupplierReference() => !string.IsNullOrEmpty(ProductSupplierReference);
    public bool ShouldSerializeProductSupplierPriceTe() => ProductSupplierPriceTe.HasValue;
}