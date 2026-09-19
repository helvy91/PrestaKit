using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("address")]
[ApiResource("addresses")]
public class Address : PrestaShopEntity
{
    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; }

    [XmlElement("id_manufacturer")]
    public long? IdManufacturer { get; set; }

    [XmlElement("id_supplier")]
    public long? IdSupplier { get; set; }

    [XmlElement("id_warehouse")]
    public long? IdWarehouse { get; set; }

    [XmlElement("id_country")]
    public long? IdCountry { get; set; }

    [XmlElement("id_state")]
    public long? IdState { get; set; }

    [XmlElement("alias")]
    public string? Alias { get; set; }

    [XmlElement("company")]
    public string? Company { get; set; }

    [XmlElement("lastname")]
    public string? LastName { get; set; }

    [XmlElement("firstname")]
    public string? FirstName { get; set; }

    [XmlElement("vat_number")]
    public string? VatNumber { get; set; }

    [XmlElement("address1")]
    public string? Address1 { get; set; }

    [XmlElement("address2")]
    public string? Address2 { get; set; }

    [XmlElement("postcode")]
    public string? PostCode { get; set; }

    [XmlElement("city")]
    public string? City { get; set; }

    [XmlElement("other")]
    public string? Other { get; set; }

    [XmlElement("phone")]
    public string? Phone { get; set; }

    [XmlElement("phone_mobile")]
    public string? PhoneMobile { get; set; }

    [XmlElement("dni")]
    public string? Dni { get; set; }

    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue && false;

    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeIdManufacturer() => IdManufacturer.HasValue;
    public bool ShouldSerializeIdSupplier() => IdSupplier.HasValue;
    public bool ShouldSerializeIdWarehouse() => IdWarehouse.HasValue;
    public bool ShouldSerializeIdCountry() => IdCountry.HasValue;
    public bool ShouldSerializeIdState() => IdState.HasValue;
    public bool ShouldSerializeAlias() => !string.IsNullOrEmpty(Alias);
    public bool ShouldSerializeCompany() => !string.IsNullOrEmpty(Company);
    public bool ShouldSerializeLastName() => !string.IsNullOrEmpty(LastName);
    public bool ShouldSerializeFirstName() => !string.IsNullOrEmpty(FirstName);
    public bool ShouldSerializeVatNumber() => !string.IsNullOrEmpty(VatNumber);
    public bool ShouldSerializeAddress1() => !string.IsNullOrEmpty(Address1);
    public bool ShouldSerializeAddress2() => !string.IsNullOrEmpty(Address2);
    public bool ShouldSerializePostCode() => !string.IsNullOrEmpty(PostCode);
    public bool ShouldSerializeCity() => !string.IsNullOrEmpty(City);
    public bool ShouldSerializeOther() => !string.IsNullOrEmpty(Other);
    public bool ShouldSerializePhone() => !string.IsNullOrEmpty(Phone);
    public bool ShouldSerializePhoneMobile() => !string.IsNullOrEmpty(PhoneMobile);
    public bool ShouldSerializeDni() => !string.IsNullOrEmpty(Dni);
    public bool ShouldSerializeDeleted() => Deleted.HasValue;
}
