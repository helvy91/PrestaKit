using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("order_payment")]
[ApiResource("order_payments")]
public class OrderPayment : PrestaShopEntity
{
    [XmlElement("order_reference")]
    public string? OrderReference { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("amount")]
    public decimal? Amount { get; set; }

    [XmlElement("payment_method")]
    public string? PaymentMethod { get; set; }

    [XmlElement("conversion_rate")]
    public decimal? ConversionRate { get; set; }

    [XmlElement("transaction_id")]
    public string? TransactionId { get; set; }

    [XmlElement("card_number")]
    public string? CardNumber { get; set; }

    [XmlElement("card_brand")]
    public string? CardBrand { get; set; }

    [XmlElement("card_expiration")]
    public string? CardExpiration { get; set; }

    [XmlElement("card_holder")]
    public string? CardHolder { get; set; }

    [XmlElement("id_employee")]
    public long? IdEmployee { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;

    public bool ShouldSerializeOrderReference() => !string.IsNullOrEmpty(OrderReference);
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeAmount() => Amount.HasValue;
    public bool ShouldSerializePaymentMethod() => !string.IsNullOrEmpty(PaymentMethod);
    public bool ShouldSerializeConversionRate() => ConversionRate.HasValue;
    public bool ShouldSerializeTransactionId() => !string.IsNullOrEmpty(TransactionId);
    public bool ShouldSerializeCardNumber() => !string.IsNullOrEmpty(CardNumber);
    public bool ShouldSerializeCardBrand() => !string.IsNullOrEmpty(CardBrand);
    public bool ShouldSerializeCardExpiration() => !string.IsNullOrEmpty(CardExpiration);
    public bool ShouldSerializeCardHolder() => !string.IsNullOrEmpty(CardHolder);
    public bool ShouldSerializeIdEmployee() => IdEmployee.HasValue;
}