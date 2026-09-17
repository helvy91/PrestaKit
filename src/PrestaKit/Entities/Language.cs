using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("language")]
[ApiResource("languages")]
public class Language : PrestaShopEntity
{
    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("iso_code")]
    public string? IsoCode { get; set; }

    [XmlElement("locale")]
    public string? Locale { get; set; }

    [XmlElement("language_code")]
    public string? LanguageCode { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("is_rtl")]
    public bool? IsRtl { get; set; }

    [XmlElement("date_format_lite")]
    public string? DateFormatLite { get; set; }

    [XmlElement("date_format_full")]
    public string? DateFormatFull { get; set; }

    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeIsoCode() => !string.IsNullOrEmpty(IsoCode);
    public bool ShouldSerializeLocale() => !string.IsNullOrEmpty(Locale);
    public bool ShouldSerializeLanguageCode() => !string.IsNullOrEmpty(LanguageCode);
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeIsRtl() => IsRtl.HasValue;
    public bool ShouldSerializeDateFormatLite() => !string.IsNullOrEmpty(DateFormatLite);
    public bool ShouldSerializeDateFormatFull() => !string.IsNullOrEmpty(DateFormatFull);
}