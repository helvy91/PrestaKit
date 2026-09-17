using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("employee")]
[ApiResource("employees")]
public class Employee : PrestaShopEntity
{
    [XmlElement("id_lang")]
    public long? IdLang { get; set; }

    [XmlElement("last_passwd_gen")]
    public string? LastPasswordGen { get; set; }

    [XmlElement("stats_date_from")]
    public DateTime? StatsDateFrom { get; set; }

    [XmlElement("stats_date_to")]
    public DateTime? StatsDateTo { get; set; }

    [XmlElement("stats_compare_from")]
    public DateTime? StatsCompareFrom { get; set; }

    [XmlElement("stats_compare_to")]
    public DateTime? StatsCompareTo { get; set; }

    [XmlElement("passwd")]
    public string? Passwd { get; set; }

    [XmlElement("lastname")]
    public string? Lastname { get; set; }

    [XmlElement("firstname")]
    public string? Firstname { get; set; }

    [XmlElement("email")]
    public string? Email { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("id_profile")]
    public long? IdProfile { get; set; }

    [XmlElement("bo_color")]
    public string? BoColor { get; set; }

    [XmlElement("default_tab")]
    public int? DefaultTab { get; set; }

    [XmlElement("bo_theme")]
    public string? BoTheme { get; set; }

    [XmlElement("bo_css")]
    public string? BoCss { get; set; }

    [XmlElement("bo_width")]
    public int? BoWidth { get; set; }

    [XmlElement("bo_menu")]
    public bool? BoMenu { get; set; }

    [XmlElement("stats_compare_option")]
    public int? StatsCompareOption { get; set; }

    [XmlElement("preselect_date_range")]
    public string? PreselectDateRange { get; set; }

    [XmlElement("id_last_order")]
    public long? IdLastOrder { get; set; }

    [XmlElement("id_last_customer_message")]
    public long? IdLastCustomerMessage { get; set; }

    [XmlElement("id_last_customer")]
    public long? IdLastCustomer { get; set; }

    [XmlElement("reset_password_token")]
    public string? ResetPasswordToken { get; set; }

    [XmlElement("reset_password_validity")]
    public string? ResetPasswordValidity { get; set; }

    [XmlElement("has_enabled_gravatar")]
    public bool? HasEnabledGravatar { get; set; }

    // Read-only fields
    public bool ShouldSerializeStatsCompareFrom() => StatsCompareFrom.HasValue && false;
    public bool ShouldSerializeLastPasswordGen() => !string.IsNullOrWhiteSpace(LastPasswordGen) && false;
    public bool ShouldSerializeStatsCompareTo() => StatsCompareTo.HasValue && false;
    public bool ShouldSerializeStatsDateFrom() => StatsDateFrom.HasValue && false;
    public bool ShouldSerializeStatsDateTo() => StatsDateTo.HasValue && false;

    public bool ShouldSerializeIdLang() => IdLang.HasValue;
    public bool ShouldSerializePasswd() => !string.IsNullOrEmpty(Passwd);
    public bool ShouldSerializeLastname() => !string.IsNullOrEmpty(Lastname);
    public bool ShouldSerializeFirstname() => !string.IsNullOrEmpty(Firstname);
    public bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeIdProfile() => IdProfile.HasValue;
    public bool ShouldSerializeBoColor() => !string.IsNullOrEmpty(BoColor);
    public bool ShouldSerializeDefaultTab() => DefaultTab.HasValue;
    public bool ShouldSerializeBoTheme() => !string.IsNullOrEmpty(BoTheme);
    public bool ShouldSerializeBoCss() => !string.IsNullOrEmpty(BoCss);
    public bool ShouldSerializeBoWidth() => BoWidth.HasValue;
    public bool ShouldSerializeBoMenu() => BoMenu.HasValue;
    public bool ShouldSerializeStatsCompareOption() => StatsCompareOption.HasValue;
    public bool ShouldSerializePreselectDateRange() => !string.IsNullOrEmpty(PreselectDateRange);
    public bool ShouldSerializeIdLastOrder() => IdLastOrder.HasValue;
    public bool ShouldSerializeIdLastCustomerMessage() => IdLastCustomerMessage.HasValue;
    public bool ShouldSerializeIdLastCustomer() => IdLastCustomer.HasValue;
    public bool ShouldSerializeResetPasswordToken() => !string.IsNullOrEmpty(ResetPasswordToken);
    public bool ShouldSerializeResetPasswordValidity() => !string.IsNullOrEmpty(ResetPasswordValidity);
    public bool ShouldSerializeHasEnabledGravatar() => HasEnabledGravatar.HasValue;
}