using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("order_detail")]
[ApiResource("order_details")]
public class OrderDetail : PrestaShopEntity
{
    [XmlElement("id_order")]
    public long? IdOrder { get; set; }

    [XmlElement("product_id")]
    public long? ProductId { get; set; }

    [XmlElement("product_attribute_id")]
    public long? ProductAttributeId { get; set; }

    [XmlElement("product_quantity_reinjected")]
    public int? ProductQuantityReinjected { get; set; }

    [XmlElement("group_reduction")]
    public decimal? GroupReduction { get; set; }

    [XmlElement("discount_quantity_applied")]
    public int? DiscountQuantityApplied { get; set; }

    [XmlElement("download_hash")]
    public string? DownloadHash { get; set; }

    [XmlElement("download_deadline")]
    public DateTime? DownloadDeadline { get; set; }

    [XmlElement("id_order_invoice")]
    public long? IdOrderInvoice { get; set; }

    [XmlElement("id_warehouse")]
    public long? IdWarehouse { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("id_customization")]
    public long? IdCustomization { get; set; }

    [XmlElement("product_name")]
    public string? ProductName { get; set; }

    [XmlElement("product_quantity")]
    public int? ProductQuantity { get; set; }

    [XmlElement("product_quantity_in_stock")]
    public int? ProductQuantityInStock { get; set; }

    [XmlElement("product_quantity_return")]
    public int? ProductQuantityReturn { get; set; }

    [XmlElement("product_quantity_refunded")]
    public int? ProductQuantityRefunded { get; set; }

    [XmlElement("product_price")]
    public decimal? ProductPrice { get; set; }

    [XmlElement("reduction_percent")]
    public decimal? ReductionPercent { get; set; }

    [XmlElement("reduction_amount")]
    public decimal? ReductionAmount { get; set; }

    [XmlElement("reduction_amount_tax_incl")]
    public decimal? ReductionAmountTaxIncl { get; set; }

    [XmlElement("reduction_amount_tax_excl")]
    public decimal? ReductionAmountTaxExcl { get; set; }

    [XmlElement("product_quantity_discount")]
    public decimal? ProductQuantityDiscount { get; set; }

    [XmlElement("product_ean13")]
    public string? ProductEan13 { get; set; }

    [XmlElement("product_isbn")]
    public string? ProductIsbn { get; set; }

    [XmlElement("product_upc")]
    public string? ProductUpc { get; set; }

    [XmlElement("product_mpn")]
    public string? ProductMpn { get; set; }

    [XmlElement("product_reference")]
    public string? ProductReference { get; set; }

    [XmlElement("product_supplier_reference")]
    public string? ProductSupplierReference { get; set; }

    [XmlElement("product_weight")]
    public decimal? ProductWeight { get; set; }

    [XmlElement("tax_computation_method")]
    public long? TaxComputationMethod { get; set; }

    [XmlElement("id_tax_rules_group")]
    public long? IdTaxRulesGroup { get; set; }

    [XmlElement("ecotax")]
    public decimal? Ecotax { get; set; }

    [XmlElement("ecotax_tax_rate")]
    public decimal? EcotaxTaxRate { get; set; }

    [XmlElement("download_nb")]
    public int? DownloadNb { get; set; }

    [XmlElement("unit_price_tax_incl")]
    public decimal? UnitPriceTaxIncl { get; set; }

    [XmlElement("unit_price_tax_excl")]
    public decimal? UnitPriceTaxExcl { get; set; }

    [XmlElement("total_price_tax_incl")]
    public decimal? TotalPriceTaxIncl { get; set; }

    [XmlElement("total_price_tax_excl")]
    public decimal? TotalPriceTaxExcl { get; set; }

    [XmlElement("total_shipping_price_tax_excl")]
    public decimal? TotalShippingPriceTaxExcl { get; set; }

    [XmlElement("total_shipping_price_tax_incl")]
    public decimal? TotalShippingPriceTaxIncl { get; set; }

    [XmlElement("purchase_supplier_price")]
    public decimal? PurchaseSupplierPrice { get; set; }

    [XmlElement("original_product_price")]
    public decimal? OriginalProductPrice { get; set; }

    [XmlElement("original_wholesale_price")]
    public decimal? OriginalWholesalePrice { get; set; }

    [XmlElement("total_refunded_tax_excl")]
    public decimal? TotalRefundedTaxExcl { get; set; }

    [XmlElement("total_refunded_tax_incl")]
    public decimal? TotalRefundedTaxIncl { get; set; }

    [XmlElement("associations")] 
    public OrderDetailAssociations? Associations { get; set; }

    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeProductId() => ProductId.HasValue;
    public bool ShouldSerializeProductAttributeId() => ProductAttributeId.HasValue;
    public bool ShouldSerializeProductQuantityReinjected() => ProductQuantityReinjected.HasValue;
    public bool ShouldSerializeGroupReduction() => GroupReduction.HasValue;
    public bool ShouldSerializeDiscountQuantityApplied() => DiscountQuantityApplied.HasValue;
    public bool ShouldSerializeDownloadHash() => !string.IsNullOrEmpty(DownloadHash);
    public bool ShouldSerializeDownloadDeadline() => DownloadDeadline.HasValue;
    public bool ShouldSerializeIdOrderInvoice() => IdOrderInvoice.HasValue;
    public bool ShouldSerializeIdWarehouse() => IdWarehouse.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeIdCustomization() => IdCustomization.HasValue;
    public bool ShouldSerializeProductName() => !string.IsNullOrEmpty(ProductName);
    public bool ShouldSerializeProductQuantity() => ProductQuantity.HasValue;
    public bool ShouldSerializeProductQuantityInStock() => ProductQuantityInStock.HasValue;
    public bool ShouldSerializeProductQuantityReturn() => ProductQuantityReturn.HasValue;
    public bool ShouldSerializeProductQuantityRefunded() => ProductQuantityRefunded.HasValue;
    public bool ShouldSerializeProductPrice() => ProductPrice.HasValue;
    public bool ShouldSerializeReductionPercent() => ReductionPercent.HasValue;
    public bool ShouldSerializeReductionAmount() => ReductionAmount.HasValue;
    public bool ShouldSerializeReductionAmountTaxIncl() => ReductionAmountTaxIncl.HasValue;
    public bool ShouldSerializeReductionAmountTaxExcl() => ReductionAmountTaxExcl.HasValue;
    public bool ShouldSerializeProductQuantityDiscount() => ProductQuantityDiscount.HasValue;
    public bool ShouldSerializeProductEan13() => !string.IsNullOrEmpty(ProductEan13);
    public bool ShouldSerializeProductIsbn() => !string.IsNullOrEmpty(ProductIsbn);
    public bool ShouldSerializeProductUpc() => !string.IsNullOrEmpty(ProductUpc);
    public bool ShouldSerializeProductMpn() => !string.IsNullOrEmpty(ProductMpn);
    public bool ShouldSerializeProductReference() => !string.IsNullOrEmpty(ProductReference);
    public bool ShouldSerializeProductSupplierReference() => !string.IsNullOrEmpty(ProductSupplierReference);
    public bool ShouldSerializeProductWeight() => ProductWeight.HasValue;
    public bool ShouldSerializeTaxComputationMethod() => TaxComputationMethod.HasValue;
    public bool ShouldSerializeIdTaxRulesGroup() => IdTaxRulesGroup.HasValue;
    public bool ShouldSerializeEcotax() => Ecotax.HasValue;
    public bool ShouldSerializeEcotaxTaxRate() => EcotaxTaxRate.HasValue;
    public bool ShouldSerializeDownloadNb() => DownloadNb.HasValue;
    public bool ShouldSerializeUnitPriceTaxIncl() => UnitPriceTaxIncl.HasValue;
    public bool ShouldSerializeUnitPriceTaxExcl() => UnitPriceTaxExcl.HasValue;
    public bool ShouldSerializeTotalPriceTaxIncl() => TotalPriceTaxIncl.HasValue;
    public bool ShouldSerializeTotalPriceTaxExcl() => TotalPriceTaxExcl.HasValue;
    public bool ShouldSerializeTotalShippingPriceTaxExcl() => TotalShippingPriceTaxExcl.HasValue;
    public bool ShouldSerializeTotalShippingPriceTaxIncl() => TotalShippingPriceTaxIncl.HasValue;
    public bool ShouldSerializePurchaseSupplierPrice() => PurchaseSupplierPrice.HasValue;
    public bool ShouldSerializeOriginalProductPrice() => OriginalProductPrice.HasValue;
    public bool ShouldSerializeOriginalWholesalePrice() => OriginalWholesalePrice.HasValue;
    public bool ShouldSerializeTotalRefundedTaxExcl() => TotalRefundedTaxExcl.HasValue;
    public bool ShouldSerializeTotalRefundedTaxIncl() => TotalRefundedTaxIncl.HasValue;
    public bool ShouldSerializeAssociations() => Associations != null;
}