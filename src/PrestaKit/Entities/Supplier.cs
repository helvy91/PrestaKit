using System.Xml.Serialization;
using PrestaKit.Clients.Media.Images;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("supplier")]
[ApiResource("suppliers")]
public class Supplier : PrestaShopEntity, IHasImages
{
    [XmlElement("link_rewrite")]
    public TranslatedField? LinkRewrite { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("active")]
    public string? Active { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("description")]
    public TranslatedField? Description { get; set; }

    [XmlElement("meta_title")]
    public TranslatedField? MetaTitle { get; set; }

    [XmlElement("meta_description")]
    public TranslatedField? MetaDescription { get; set; }

    [XmlElement("meta_keywords")]
    public TranslatedField? MetaKeywords { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;

    public bool ShouldSerializeLinkRewrite() => LinkRewrite != null;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeActive() => !string.IsNullOrEmpty(Active);
    public bool ShouldSerializeDescription() => Description != null;
    public bool ShouldSerializeMetaTitle() => MetaTitle != null;
    public bool ShouldSerializeMetaDescription() => MetaDescription != null;
    public bool ShouldSerializeMetaKeywords() => MetaKeywords != null;
}