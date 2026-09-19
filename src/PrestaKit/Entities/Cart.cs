using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("cart")]
[ApiResource("carts")]
public class Cart : PrestaShopEntity
{
    [XmlElement("id_address_delivery")]
    public long? IdAddressDelivery { get; set; }

    [XmlElement("id_address_invoice")]
    public long? IdAddressInvoice { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; }

    [XmlElement("id_guest")]
    public long? IdGuest { get; set; }

    [XmlElement("id_lang")]
    public long? IdLang { get; set; }

    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("id_carrier")]
    public long? IdCarrier { get; set; }

    [XmlElement("recyclable")]
    public bool? Recyclable { get; set; }

    [XmlElement("gift")]
    public bool? Gift { get; set; }

    [XmlElement("gift_message")]
    public string? GiftMessage { get; set; }

    [XmlElement("mobile_theme")]
    public bool? MobileTheme { get; set; }

    [XmlElement("delivery_option")]
    public string? DeliveryOption { get; set; }

    [XmlElement("secure_key")]
    public string? SecureKey { get; set; }

    [XmlElement("allow_seperated_package")]
    public bool? AllowSeperatedPackage { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("associations")] 
    public CartAssociations? Associations { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue && false;

    public bool ShouldSerializeIdAddressDelivery() => IdAddressDelivery.HasValue;
    public bool ShouldSerializeIdAddressInvoice() => IdAddressInvoice.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeIdGuest() => IdGuest.HasValue;
    public bool ShouldSerializeIdLang() => IdLang.HasValue;
    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeIdCarrier() => IdCarrier.HasValue;
    public bool ShouldSerializeRecyclable() => Recyclable.HasValue;
    public bool ShouldSerializeGift() => Gift.HasValue;
    public bool ShouldSerializeGiftMessage() => !string.IsNullOrEmpty(GiftMessage);
    public bool ShouldSerializeMobileTheme() => MobileTheme.HasValue;
    public bool ShouldSerializeDeliveryOption() => !string.IsNullOrEmpty(DeliveryOption);
    public bool ShouldSerializeSecureKey() => !string.IsNullOrEmpty(SecureKey);
    public bool ShouldSerializeAllowSeperatedPackage() => AllowSeperatedPackage.HasValue;
    public bool ShouldSerializeAssociations() => Associations != null;
}