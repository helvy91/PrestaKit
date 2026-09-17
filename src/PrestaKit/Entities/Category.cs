using PrestaKit.Clients.Media.Images;
using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("category")]
[ApiResource("categories")]
public class Category : PrestaShopEntity, IHasImages
{
    [XmlElement("id_parent")]
    public long? IdParent { get; set; }

    [XmlElement("level_depth")]
    public int? LevelDepth { get; set; }

    [XmlElement("nb_products_recursive")]
    public string? NbProductsRecursive { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("id_shop_default")]
    public long? IdShopDefault { get; set; }

    [XmlElement("is_root_category")]
    public bool? IsRootCategory { get; set; }

    [XmlElement("position")]
    public string? Position { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; } 

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("link_rewrite")]
    public TranslatedField? LinkRewrite { get; set; }

    [XmlElement("description")]
    public TranslatedField? Description { get; set; }

    [XmlElement("meta_title")]
    public TranslatedField? MetaTitle { get; set; }

    [XmlElement("meta_description")]
    public TranslatedField? MetaDescription { get; set; }

    [XmlElement("meta_keywords")]
    public TranslatedField? MetaKeywords { get; set; }

    [XmlElement("additional_description")]
    public TranslatedField? AdditionalDescription { get; set; }

    [XmlElement("associations")] 
    public CategoryAssociations? Associations { get; set; }

    // Read-only fields
    public bool ShouldSerializeLevelDepth() => LevelDepth.HasValue && false;
    public bool ShouldSerializeNbProductsRecursive() => !string.IsNullOrWhiteSpace(NbProductsRecursive) && false;

    public bool ShouldSerializeIdParent() => IdParent.HasValue;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeIdShopDefault() => IdShopDefault.HasValue;
    public bool ShouldSerializeIsRootCategory() => IsRootCategory.HasValue;
    public bool ShouldSerializePosition() => !string.IsNullOrEmpty(Position);
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeLinkRewrite() => LinkRewrite != null;
    public bool ShouldSerializeDescription() => Description != null;
    public bool ShouldSerializeAdditionalDescription() => AdditionalDescription != null;
    public bool ShouldSerializeMetaTitle() => MetaTitle != null;
    public bool ShouldSerializeMetaDescription() => MetaDescription != null;
    public bool ShouldSerializeMetaKeywords() => MetaKeywords != null;
    public bool ShouldSerializeAssociations() => Associations != null;
}