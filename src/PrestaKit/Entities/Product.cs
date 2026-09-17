using PrestaKit.Clients.Media.Images;
using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("product")]
[ApiResource("products")]
public class Product : PrestaShopEntity, IHasImages
{
    [XmlElement("id_manufacturer")]
    public long? IdManufacturer { get; set; }
    
    [XmlElement("id_supplier")]
    public long? IdSupplier { get; set; }

    [XmlElement("id_category_default")]
    public long? IdCategoryDefault { get; set; }

    [XmlElement("new")]
    public string? New { get; set; }

    [XmlElement("cache_default_attribute")]
    public string? CacheDefaultAttribute { get; set; }

    [XmlElement("id_default_image")]
    public long? IdDefaultImage { get; set; }

    [XmlElement("id_default_combination")]
    public long? IdDefaultCombination { get; set; }

    [XmlElement("id_tax_rules_group")]
    public long? IdTaxRulesGroup { get; set; }

    [XmlElement("position_in_category")]
    public string? PositionInCategory { get; set; }

    [XmlElement("manufacturer_name")]
    public string? ManufacturerName { get; set; }

    [XmlElement("quantity")]
    public int? Quantity { get; set; }

    [XmlElement("type")]
    public string? Type { get; set; }

    [XmlElement("product_type")]
    public string? ProductType { get; set; }

    [XmlElement("id_shop_default")]
    public long? IdShopDefault { get; set; }

    [XmlElement("reference")]
    public string? Reference { get; set; }

    [XmlElement("supplier_reference")]
    public string? SupplierReference { get; set; }

    [XmlElement("location")]
    public string? Location { get; set; }

    [XmlElement("width")]
    public decimal? Width { get; set; }

    [XmlElement("height")]
    public decimal? Height { get; set; }

    [XmlElement("depth")]
    public decimal? Depth { get; set; }

    [XmlElement("weight")]
    public decimal? Weight { get; set; }

    [XmlElement("quantity_discount")]
    public bool? QuantityDiscount { get; set; }

    [XmlElement("ean13")]
    public string? Ean13 { get; set; }

    [XmlElement("isbn")]
    public string? Isbn { get; set; }

    [XmlElement("upc")]
    public string? Upc { get; set; }

    [XmlElement("mpn")]
    public string? Mpn { get; set; }

    [XmlElement("cache_is_pack")]
    public bool? CacheIsPack { get; set; }

    [XmlElement("cache_has_attachments")]
    public bool? CacheHasAttachments { get; set; }

    [XmlElement("is_virtual")]
    public bool? IsVirtual { get; set; }

    [XmlElement("state")]
    public long? State { get; set; } = 1;

    [XmlElement("additional_delivery_times")]
    public long? AdditionalDeliveryTimes { get; set; } 

    [XmlElement("delivery_in_stock")]
    public TranslatedField? DeliveryInStock { get; set; }

    [XmlElement("delivery_out_stock")]
    public TranslatedField? DeliveryOutStock { get; set; }

    [XmlElement("on_sale")]
    public bool? OnSale { get; set; }

    [XmlElement("online_only")]
    public bool? OnlineOnly { get; set; }

    [XmlElement("ecotax")]
    public decimal? Ecotax { get; set; }

    [XmlElement("minimal_quantity")]
    public int? MinimalQuantity { get; set; }

    [XmlElement("low_stock_threshold")]
    public int? LowStockThreshold { get; set; }

    [XmlElement("low_stock_alert")]
    public bool? LowStockAlert { get; set; }

    [XmlElement("price")]
    public decimal? Price { get; set; }

    [XmlElement("unit_price")]
    public decimal? UnitPrice { get; set; }

    [XmlElement("wholesale_price")]
    public decimal? WholesalePrice { get; set; }

    [XmlElement("unity")]
    public string? Unity { get; set; }

    [XmlElement("unit_price_ratio")]
    public string? UnitPriceRatio { get; set; }

    [XmlElement("additional_shipping_cost")]
    public decimal? AdditionalShippingCost { get; set; }

    [XmlElement("customizable")]
    public int? Customizable { get; set; }

    [XmlElement("text_fields")]
    public int? TextFields { get; set; }

    [XmlElement("uploadable_files")]
    public int? UploadableFiles { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("redirect_type")]
    public string? RedirectType { get; set; }

    [XmlElement("id_type_redirected")]
    public long? IdTypeRedirected { get; set; }

    [XmlElement("available_for_order")]
    public bool? AvailableForOrder { get; set; }

    [XmlElement("available_date")]
    public DateTime? AvailableDate { get; set; }

    [XmlElement("show_condition")]
    public bool? ShowCondition { get; set; }

    [XmlElement("condition")]
    public string? Condition { get; set; }

    [XmlElement("show_price")]
    public bool? ShowPrice { get; set; }

    [XmlElement("indexed")]
    public bool? Indexed { get; set; }

    [XmlElement("visibility")]
    public string? Visibility { get; set; }

    [XmlElement("advanced_stock_management")]
    public bool? AdvancedStockManagement { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("pack_stock_type")]
    public int? PackStockType { get; set; }

    [XmlElement("meta_description")]
    public TranslatedField? MetaDescription { get; set; }

    [XmlElement("meta_keywords")]
    public TranslatedField? MetaKeywords { get; set; }

    [XmlElement("meta_title")]
    public TranslatedField? MetaTitle { get; set; }

    [XmlElement("link_rewrite")]
    public TranslatedField? LinkRewrite { get; set; } 

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("description")]
    public TranslatedField? Description { get; set; } 

    [XmlElement("description_short")]
    public TranslatedField? DescriptionShort { get; set; }

    [XmlElement("available_now")]
    public TranslatedField? AvailableNow { get; set; }

    [XmlElement("available_later")]
    public TranslatedField? AvailableLater { get; set; }

    [XmlElement("associations")] 
    public ProductAssociations? Associations { get; set; }

    // Read-only fields
    public bool ShouldSerializeNew() => !string.IsNullOrWhiteSpace(New) && false;
    public bool ShouldSerializeManufacturerName() => !string.IsNullOrWhiteSpace(ManufacturerName) && false;
    public bool ShouldSerializeQuantity() => Quantity.HasValue && false;

    public bool ShouldSerializeIdManufacturer() => IdManufacturer.HasValue;
    public bool ShouldSerializeIdSupplier() => IdSupplier.HasValue;
    public bool ShouldSerializeIdCategoryDefault() => IdCategoryDefault.HasValue;
    public bool ShouldSerializeCacheDefaultAttribute() => !string.IsNullOrEmpty(CacheDefaultAttribute);
    public bool ShouldSerializeIdDefaultImage() => IdDefaultImage.HasValue;
    public bool ShouldSerializeIdDefaultCombination() => IdDefaultCombination.HasValue;
    public bool ShouldSerializeIdTaxRulesGroup() => IdTaxRulesGroup.HasValue;
    public bool ShouldSerializePositionInCategory() => !string.IsNullOrEmpty(PositionInCategory);
    public bool ShouldSerializeType() => !string.IsNullOrEmpty(Type);
    public bool ShouldSerializeProductType() => !string.IsNullOrEmpty(ProductType);
    public bool ShouldSerializeIdShopDefault() => IdShopDefault.HasValue;
    public bool ShouldSerializeReference() => !string.IsNullOrEmpty(Reference);
    public bool ShouldSerializeSupplierReference() => !string.IsNullOrEmpty(SupplierReference);
    public bool ShouldSerializeLocation() => !string.IsNullOrEmpty(Location);
    public bool ShouldSerializeWidth() => Width.HasValue;
    public bool ShouldSerializeHeight() => Height.HasValue;
    public bool ShouldSerializeDepth() => Depth.HasValue;
    public bool ShouldSerializeWeight() => Weight.HasValue;
    public bool ShouldSerializeQuantityDiscount() => QuantityDiscount.HasValue;
    public bool ShouldSerializeEan13() => !string.IsNullOrEmpty(Ean13);
    public bool ShouldSerializeIsbn() => !string.IsNullOrEmpty(Isbn);
    public bool ShouldSerializeUpc() => !string.IsNullOrEmpty(Upc);
    public bool ShouldSerializeMpn() => !string.IsNullOrEmpty(Mpn);
    public bool ShouldSerializeCacheIsPack() => CacheIsPack.HasValue;
    public bool ShouldSerializeCacheHasAttachments() => CacheHasAttachments.HasValue;
    public bool ShouldSerializeIsVirtual() => IsVirtual.HasValue;
    public bool ShouldSerializeState() => State.HasValue;
    public bool ShouldSerializeAdditionalDeliveryTimes() => AdditionalDeliveryTimes.HasValue;
    public bool ShouldSerializeDeliveryInStock() => DeliveryInStock != null;
    public bool ShouldSerializeDeliveryOutStock() => DeliveryOutStock != null;
    public bool ShouldSerializeOnSale() => OnSale.HasValue;
    public bool ShouldSerializeOnlineOnly() => OnlineOnly.HasValue;
    public bool ShouldSerializeEcotax() => Ecotax.HasValue;
    public bool ShouldSerializeMinimalQuantity() => MinimalQuantity.HasValue;
    public bool ShouldSerializeLowStockThreshold() => LowStockThreshold.HasValue;
    public bool ShouldSerializeLowStockAlert() => LowStockAlert.HasValue;
    public bool ShouldSerializePrice() => Price.HasValue;
    public bool ShouldSerializeUnitPrice() => UnitPrice.HasValue;
    public bool ShouldSerializeWholesalePrice() => WholesalePrice.HasValue;
    public bool ShouldSerializeUnity() => !string.IsNullOrEmpty(Unity);
    public bool ShouldSerializeUnitPriceRatio() => !string.IsNullOrEmpty(UnitPriceRatio);
    public bool ShouldSerializeAdditionalShippingCost() => AdditionalShippingCost.HasValue;
    public bool ShouldSerializeCustomizable() => Customizable.HasValue;
    public bool ShouldSerializeTextFields() => TextFields.HasValue;
    public bool ShouldSerializeUploadableFiles() => UploadableFiles.HasValue;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeRedirectType() => !string.IsNullOrEmpty(RedirectType);
    public bool ShouldSerializeIdTypeRedirected() => IdTypeRedirected.HasValue;
    public bool ShouldSerializeAvailableForOrder() => AvailableForOrder.HasValue;
    public bool ShouldSerializeAvailableDate() => AvailableDate.HasValue;
    public bool ShouldSerializeShowCondition() => ShowCondition.HasValue;
    public bool ShouldSerializeCondition() => !string.IsNullOrEmpty(Condition);
    public bool ShouldSerializeShowPrice() => ShowPrice.HasValue;
    public bool ShouldSerializeIndexed() => Indexed.HasValue;
    public bool ShouldSerializeVisibility() => !string.IsNullOrEmpty(Visibility);
    public bool ShouldSerializeAdvancedStockManagement() => AdvancedStockManagement.HasValue;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
    public bool ShouldSerializePackStockType() => PackStockType.HasValue;
    public bool ShouldSerializeMetaDescription() => MetaDescription != null;
    public bool ShouldSerializeMetaKeywords() => MetaKeywords != null;
    public bool ShouldSerializeMetaTitle() => MetaTitle != null;
    public bool ShouldSerializeLinkRewrite() => LinkRewrite != null;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeDescription() => Description != null;
    public bool ShouldSerializeDescriptionShort() => DescriptionShort != null;
    public bool ShouldSerializeAvailableNow() => AvailableNow != null;
    public bool ShouldSerializeAvailableLater() => AvailableLater != null;
    public bool ShouldSerializeAssociations() => Associations != null;
}
