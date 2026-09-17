using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("customer")]
[ApiResource("customers")]
public class Customer : PrestaShopEntity
{
    [XmlElement("id_default_group")]
    public long? IdDefaultGroup { get; set; }

    [XmlElement("id_lang")]
    public long? IdLang { get; set; }

    [XmlElement("newsletter_date_add")]
    public string? NewsletterDateAdd { get; set; }

    [XmlElement("ip_registration_newsletter")]
    public string? IpRegistrationNewsletter { get; set; }

    [XmlElement("last_passwd_gen")]
    public string? LastPasswdGen { get; set; }

    [XmlElement("secure_key")]
    public string? SecureKey { get; set; }

    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    [XmlElement("passwd")]
    public string? Password { get; set; }

    [XmlElement("lastname")]
    public string? LastName { get; set; }

    [XmlElement("firstname")]
    public string? FirstName { get; set; }

    [XmlElement("email")]
    public string? Email { get; set; }

    [XmlElement("id_gender")]
    public long? IdGender { get; set; }

    [XmlElement("birthday")]
    public DateTime? Birthday { get; set; }

    [XmlElement("newsletter")]
    public bool? Newsletter { get; set; }

    [XmlElement("optin")]
    public bool? Optin { get; set; }

    [XmlElement("website")]
    public string? Website { get; set; }

    [XmlElement("company")]
    public string? Company { get; set; }

    [XmlElement("siret")]
    public string? Siret { get; set; }

    [XmlElement("ape")]
    public string? Ape { get; set; }

    [XmlElement("outstanding_allow_amount")]
    public decimal? OutstandingAllowAmount { get; set; }

    [XmlElement("show_public_prices")]
    public bool? ShowPublicPrices { get; set; }

    [XmlElement("id_risk")]
    public long? IdRisk { get; set; }

    [XmlElement("max_payment_days")]
    public int? MaxPaymentDays { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("note")]
    public string? Note { get; set; }

    [XmlElement("is_guest")]
    public bool? IsGuest { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("reset_password_token")]
    public string? ResetPasswordToken { get; set; }

    [XmlElement("reset_password_validity")]
    public string? ResetPasswordValidity { get; set; }

    // Read-only fields
    public  bool ShouldSerializeLastPasswdGen() => !string.IsNullOrWhiteSpace(LastPasswdGen) && false;
    public  bool ShouldSerializeSecureKey() => !string.IsNullOrWhiteSpace(SecureKey) && false;

    public bool ShouldSerializeIdDefaultGroup() => IdDefaultGroup.HasValue;
    public bool ShouldSerializeIdLang() => IdLang.HasValue;
    public bool ShouldSerializeNewsletterDateAdd() => !string.IsNullOrEmpty(NewsletterDateAdd);
    public bool ShouldSerializeIpRegistrationNewsletter() => !string.IsNullOrEmpty(IpRegistrationNewsletter);
    public bool ShouldSerializeDeleted() => Deleted.HasValue;
    public bool ShouldSerializePassword() => !string.IsNullOrEmpty(Password);
    public bool ShouldSerializeLastName() => !string.IsNullOrEmpty(LastName);
    public bool ShouldSerializeFirstName() => !string.IsNullOrEmpty(FirstName);
    public bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);
    public bool ShouldSerializeIdGender() => IdGender.HasValue;
    public bool ShouldSerializeBirthday() => Birthday.HasValue;
    public bool ShouldSerializeNewsletter() => Newsletter.HasValue;
    public bool ShouldSerializeOptin() => Optin.HasValue;
    public bool ShouldSerializeWebsite() => !string.IsNullOrEmpty(Website);
    public bool ShouldSerializeCompany() => !string.IsNullOrEmpty(Company);
    public bool ShouldSerializeSiret() => !string.IsNullOrEmpty(Siret);
    public bool ShouldSerializeApe() => !string.IsNullOrEmpty(Ape);
    public bool ShouldSerializeOutstandingAllowAmount() => OutstandingAllowAmount.HasValue;
    public bool ShouldSerializeShowPublicPrices() => ShowPublicPrices.HasValue;
    public bool ShouldSerializeIdRisk() => IdRisk.HasValue;
    public bool ShouldSerializeMaxPaymentDays() => MaxPaymentDays.HasValue;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeNote() => !string.IsNullOrEmpty(Note);
    public bool ShouldSerializeIsGuest() => IsGuest.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
    public bool ShouldSerializeResetPasswordToken() => !string.IsNullOrEmpty(ResetPasswordToken);
    public bool ShouldSerializeResetPasswordValidity() => !string.IsNullOrEmpty(ResetPasswordValidity);
}