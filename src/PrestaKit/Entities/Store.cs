using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("store")]
[ApiResource("stores")]
public class Store : PrestaShopEntity
{
    [XmlElement("id_country")]
    public long? IdCountry { get; set; }

    [XmlElement("id_state")]
    public long? IdState { get; set; }

    [XmlElement("hours")]
    public string? Hours { get; set; }

    [XmlElement("postcode")]
    public string? Postcode { get; set; }

    [XmlElement("city")]
    public string? City { get; set; }

    [XmlElement("latitude")]
    public string? Latitude { get; set; }

    [XmlElement("longitude")]
    public string? Longitude { get; set; }

    [XmlElement("phone")]
    public string? Phone { get; set; }

    [XmlElement("fax")]
    public string? Fax { get; set; }

    [XmlElement("email")]
    public string? Email { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("address1")]
    public string? Address1 { get; set; }

    [XmlElement("address2")]
    public string? Address2 { get; set; }

    [XmlElement("note")]
    public string? Note { get; set; }

    public bool ShouldSerializeIdCountry() => IdCountry.HasValue;
    public bool ShouldSerializeIdState() => IdState.HasValue;
    public bool ShouldSerializeHours() => !string.IsNullOrEmpty(Hours);
    public bool ShouldSerializePostcode() => !string.IsNullOrEmpty(Postcode);
    public bool ShouldSerializeCity() => !string.IsNullOrEmpty(City);
    public bool ShouldSerializeLatitude() => !string.IsNullOrEmpty(Latitude);
    public bool ShouldSerializeLongitude() => !string.IsNullOrEmpty(Longitude);
    public bool ShouldSerializePhone() => !string.IsNullOrEmpty(Phone);
    public bool ShouldSerializeFax() => !string.IsNullOrEmpty(Fax);
    public bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeAddress1() => !string.IsNullOrEmpty(Address1);
    public bool ShouldSerializeAddress2() => !string.IsNullOrEmpty(Address2);
    public bool ShouldSerializeNote() => !string.IsNullOrEmpty(Note);
}