using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("supply_order_history")]
[ApiResource("supply_order_histories")]
public class SupplyOrderHistory : PrestaShopEntity
{
    [XmlElement("id_supply_order")]
    public long? IdSupplyOrder { get; set; }

    [XmlElement("id_employee")]
    public long? IdEmployee { get; set; }

    [XmlElement("id_state")]
    public long? IdState { get; set; }

    [XmlElement("employee_firstname")]
    public string? EmployeeFirstname { get; set; }

    [XmlElement("employee_lastname")]
    public string? EmployeeLastname { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    public bool ShouldSerializeIdSupplyOrder() => IdSupplyOrder.HasValue;
    public bool ShouldSerializeIdEmployee() => IdEmployee.HasValue;
    public bool ShouldSerializeIdState() => IdState.HasValue;
    public bool ShouldSerializeEmployeeFirstname() => !string.IsNullOrEmpty(EmployeeFirstname);
    public bool ShouldSerializeEmployeeLastname() => !string.IsNullOrEmpty(EmployeeLastname);
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
}