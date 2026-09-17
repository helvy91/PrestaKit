using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("supply_order")]
[ApiResource("supply_orders")]
public class SupplyOrder : PrestaShopEntity
{
    [XmlElement("id_supplier")]
    public long? IdSupplier { get; set; }

    [XmlElement("id_lang")]
    public long? IdLang { get; set; }

    [XmlElement("id_warehouse")]
    public long? IdWarehouse { get; set; }

    [XmlElement("id_supply_order_state")]
    public long? IdSupplyOrderState { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("supplier_name")]
    public string? SupplierName { get; set; }

    [XmlElement("reference")]
    public string? Reference { get; set; }

    [XmlElement("date_delivery_expected")]
    public DateTime? DateDeliveryExpected { get; set; }

    [XmlElement("total_te")]
    public decimal? TotalTe { get; set; }

    [XmlElement("total_with_discount_te")]
    public decimal? TotalWithDiscountTe { get; set; }

    [XmlElement("total_ti")]
    public decimal? TotalTi { get; set; }

    [XmlElement("total_tax")]
    public decimal? TotalTax { get; set; }

    [XmlElement("discount_rate")]
    public decimal? DiscountRate { get; set; }

    [XmlElement("discount_value_te")]
    public decimal? DiscountValueTe { get; set; }

    [XmlElement("is_template")]
    public bool? IsTemplate { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("associations")]
    public SupplyOrderAssociations? Associations { get; set; }

    public bool ShouldSerializeIdSupplier() => IdSupplier.HasValue;
    public bool ShouldSerializeIdLang() => IdLang.HasValue;
    public bool ShouldSerializeIdWarehouse() => IdWarehouse.HasValue;
    public bool ShouldSerializeIdSupplyOrderState() => IdSupplyOrderState.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeSupplierName() => !string.IsNullOrEmpty(SupplierName);
    public bool ShouldSerializeReference() => !string.IsNullOrEmpty(Reference);
    public bool ShouldSerializeDateDeliveryExpected() => DateDeliveryExpected.HasValue;
    public bool ShouldSerializeTotalTe() => TotalTe.HasValue;
    public bool ShouldSerializeTotalWithDiscountTe() => TotalWithDiscountTe.HasValue;
    public bool ShouldSerializeTotalTi() => TotalTi.HasValue;
    public bool ShouldSerializeTotalTax() => TotalTax.HasValue;
    public bool ShouldSerializeDiscountRate() => DiscountRate.HasValue;
    public bool ShouldSerializeDiscountValueTe() => DiscountValueTe.HasValue;
    public bool ShouldSerializeIsTemplate() => IsTemplate.HasValue;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
    public bool ShouldSerializeAssociations() => Associations != null;
}