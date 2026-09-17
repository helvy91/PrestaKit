using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("content")]
[ApiResource("content_management_system")]
public class Content : PrestaShopEntity
{
    [XmlElement("id_cms_category")]
    public long? IdCmsCategory { get; set; }

    [XmlElement("position")]
    public string? Position { get; set; }

    [XmlElement("indexation")]
    public string? Indexation { get; set; }

    [XmlElement("active")]
    public string? Active { get; set; }

    [XmlElement("meta_description")]
    public TranslatedField? MetaDescription { get; set; }

    [XmlElement("meta_keywords")]
    public TranslatedField? MetaKeywords { get; set; }

    [XmlElement("meta_title")]
    public TranslatedField? MetaTitle { get; set; }

    [XmlElement("head_seo_title")]
    public string? HeadSeoTitle { get; set; }

    [XmlElement("link_rewrite")]
    public TranslatedField? LinkRewrite { get; set; }

    [XmlElement("content")]
    public TranslatedField? ContentName { get; set; }

    public bool ShouldSerializeIdCmsCategory() => IdCmsCategory.HasValue;
    public bool ShouldSerializePosition() => !string.IsNullOrEmpty(Position);
    public bool ShouldSerializeIndexation() => !string.IsNullOrEmpty(Indexation);
    public bool ShouldSerializeActive() => !string.IsNullOrEmpty(Active);
    public bool ShouldSerializeMetaDescription() => MetaDescription != null;
    public bool ShouldSerializeMetaKeywords() => MetaKeywords != null;
    public bool ShouldSerializeMetaTitle() => MetaTitle != null;
    public bool ShouldSerializeHeadSeoTitle() => !string.IsNullOrEmpty(HeadSeoTitle);
    public bool ShouldSerializeLinkRewrite() => LinkRewrite != null;
    public bool ShouldSerializeContentName() => ContentName != null;
}