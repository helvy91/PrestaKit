using PrestaKit.Clients.Media.Images;
using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("manufacturer")]
[ApiResource("manufacturers")]
public class Manufacturer : PrestaShopEntity, IHasImages
{
    [XmlElement("active")]
    public string? Active { get; set; }

    [XmlElement("link_rewrite")]
    public TranslatedField? LinkRewrite { get; set; }
    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("description")]
    public TranslatedField? Description { get; set; }

    [XmlElement("short_description")]
    public TranslatedField? ShortDescription { get; set; }

    [XmlElement("meta_title")]
    public TranslatedField? MetaTitle { get; set; } 

    [XmlElement("meta_description")]
    public TranslatedField? MetaDescription { get; set; } 

    [XmlElement("meta_keywords")]
    public TranslatedField? MetaKeywords { get; set; } 

    [XmlElement("associations")]
    public ManufacturerAssociations? Associations { get; set; }

    // Read-only fields
    public static bool ShouldSerializeLinkRewrite() => false;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue && false;

    public bool ShouldSerializeActive() => !string.IsNullOrEmpty(Active);
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeDescription() => Description != null;
    public bool ShouldSerializeShortDescription() => ShortDescription != null;
    public bool ShouldSerializeMetaTitle() => MetaTitle != null;
    public bool ShouldSerializeMetaDescription() => MetaDescription != null;
    public bool ShouldSerializeMetaKeywords() => MetaKeywords != null;
    public bool ShouldSerializeAssociations() => Associations != null;
}