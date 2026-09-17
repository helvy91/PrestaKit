using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("guest")]
[ApiResource("guests")]
public class Guest : PrestaShopEntity
{
    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; }

    [XmlElement("id_operating_system")]
    public long? IdOperatingSystem { get; set; }

    [XmlElement("id_web_browser")]
    public long? IdWebBrowser { get; set; }

    [XmlElement("javascript")]
    public bool? Javascript { get; set; }

    [XmlElement("screen_resolution_x")]
    public int? ScreenResolutionX { get; set; }

    [XmlElement("screen_resolution_y")]
    public int? ScreenResolutionY { get; set; }

    [XmlElement("screen_color")]
    public int? ScreenColor { get; set; }

    [XmlElement("sun_java")]
    public bool? SunJava { get; set; }

    [XmlElement("adobe_flash")]
    public bool? AdobeFlash { get; set; }

    [XmlElement("adobe_director")]
    public bool? AdobeDirector { get; set; }

    [XmlElement("apple_quicktime")]
    public bool? AppleQuicktime { get; set; }

    [XmlElement("real_player")]
    public bool? RealPlayer { get; set; }

    [XmlElement("windows_media")]
    public bool? WindowsMedia { get; set; }

    [XmlElement("accept_language")]
    public string? AcceptLanguage { get; set; }

    [XmlElement("mobile_theme")]
    public bool? MobileTheme { get; set; }

    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeIdOperatingSystem() => IdOperatingSystem.HasValue;
    public bool ShouldSerializeIdWebBrowser() => IdWebBrowser.HasValue;
    public bool ShouldSerializeJavascript() => Javascript.HasValue;
    public bool ShouldSerializeScreenResolutionX() => ScreenResolutionX.HasValue;
    public bool ShouldSerializeScreenResolutionY() => ScreenResolutionY.HasValue;
    public bool ShouldSerializeScreenColor() => ScreenColor.HasValue;
    public bool ShouldSerializeSunJava() => SunJava.HasValue;
    public bool ShouldSerializeAdobeFlash() => AdobeFlash.HasValue;
    public bool ShouldSerializeAdobeDirector() => AdobeDirector.HasValue;
    public bool ShouldSerializeAppleQuicktime() => AppleQuicktime.HasValue;
    public bool ShouldSerializeRealPlayer() => RealPlayer.HasValue;
    public bool ShouldSerializeWindowsMedia() => WindowsMedia.HasValue;
    public bool ShouldSerializeAcceptLanguage() => !string.IsNullOrEmpty(AcceptLanguage);
    public bool ShouldSerializeMobileTheme() => MobileTheme.HasValue;
}