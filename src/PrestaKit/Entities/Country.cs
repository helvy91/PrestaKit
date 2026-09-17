using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("country")]
[ApiResource("countries")]
public class Country : PrestaShopEntity
{
    [XmlElement("id_zone")]
    public long? IdZone { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("call_prefix")]
    public int? CallPrefix { get; set; }

    [XmlElement("iso_code")]
    public string? IsoCode { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("contains_states")]
    public bool? ContainsStates { get; set; }

    [XmlElement("need_identification_number")]
    public bool? NeedIdentificationNumber { get; set; }

    [XmlElement("need_zip_code")]
    public bool? NeedZipCode { get; set; }

    [XmlElement("zip_code_format")]
    public string? ZipCodeFormat { get; set; }

    [XmlElement("display_tax_label")]
    public bool? DisplayTaxLabel { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    public bool ShouldSerializeIdZone() => IdZone.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeCallPrefix() => CallPrefix.HasValue;
    public bool ShouldSerializeIsoCode() => !string.IsNullOrEmpty(IsoCode);
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeContainsStates() => ContainsStates.HasValue;
    public bool ShouldSerializeNeedIdentificationNumber() => NeedIdentificationNumber.HasValue;
    public bool ShouldSerializeNeedZipCode() => NeedZipCode.HasValue;
    public bool ShouldSerializeZipCodeFormat() => !string.IsNullOrEmpty(ZipCodeFormat);
    public bool ShouldSerializeDisplayTaxLabel() => DisplayTaxLabel.HasValue;
    public bool ShouldSerializeName() => Name != null;

}