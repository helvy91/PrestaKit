using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("stock_mvt")]
[ApiResource("stock_movements")]
public class StockMvt : PrestaShopEntity
{
    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_product_attribute")]
    public long? IdProductAttribute { get; set; }

    [XmlElement("id_warehouse")]
    public long? IdWarehouse { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("management_type")]
    public string? ManagementType { get; set; }

    [XmlElement("id_employee")]
    public long? IdEmployee { get; set; }

    [XmlElement("id_stock")]
    public long? IdStock { get; set; }

    [XmlElement("id_stock_mvt_reason")]
    public long? IdStockMvtReason { get; set; }

    [XmlElement("id_order")]
    public long? IdOrder { get; set; }

    [XmlElement("id_supply_order")]
    public long? IdSupplyOrder { get; set; }

    [XmlElement("product_name")]
    public string? ProductName { get; set; }

    [XmlElement("ean13")]
    public string? Ean13 { get; set; }

    [XmlElement("upc")]
    public string? Upc { get; set; }

    [XmlElement("reference")]
    public string? Reference { get; set; }

    [XmlElement("mpn")]
    public string? Mpn { get; set; }

    [XmlElement("physical_quantity")]
    public int? PhysicalQuantity { get; set; }

    [XmlElement("sign")]
    public int? Sign { get; set; }

    [XmlElement("last_wa")]
    public decimal? LastWa { get; set; }

    [XmlElement("current_wa")]
    public decimal? CurrentWa { get; set; }

    [XmlElement("price_te")]
    public decimal? PriceTe { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;

    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdProductAttribute() => IdProductAttribute.HasValue;
    public bool ShouldSerializeIdWarehouse() => IdWarehouse.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeManagementType() => !string.IsNullOrEmpty(ManagementType);
    public bool ShouldSerializeIdEmployee() => IdEmployee.HasValue;
    public bool ShouldSerializeIdStock() => IdStock.HasValue;
    public bool ShouldSerializeIdStockMvtReason() => IdStockMvtReason.HasValue;
    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeIdSupplyOrder() => IdSupplyOrder.HasValue;
    public bool ShouldSerializeProductName() => !string.IsNullOrEmpty(ProductName);
    public bool ShouldSerializeEan13() => !string.IsNullOrEmpty(Ean13);
    public bool ShouldSerializeUpc() => !string.IsNullOrEmpty(Upc);
    public bool ShouldSerializeReference() => !string.IsNullOrEmpty(Reference);
    public bool ShouldSerializeMpn() => !string.IsNullOrEmpty(Mpn);
    public bool ShouldSerializePhysicalQuantity() => PhysicalQuantity.HasValue;
    public bool ShouldSerializeSign() => Sign.HasValue;
    public bool ShouldSerializeLastWa() => LastWa.HasValue;
    public bool ShouldSerializeCurrentWa() => CurrentWa.HasValue;
    public bool ShouldSerializePriceTe() => PriceTe.HasValue;
}