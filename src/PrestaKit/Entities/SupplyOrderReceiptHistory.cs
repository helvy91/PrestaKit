using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("supply_order_receipt_history")]
[ApiResource("supply_order_receipt_histories")]
public class SupplyOrderReceiptHistory : PrestaShopEntity
{
    [XmlElement("id_supply_order_detail")]
    public long? IdSupplyOrderDetail { get; set; }

    [XmlElement("id_employee")]
    public long? IdEmployee { get; set; }

    [XmlElement("id_supply_order_state")]
    public long? IdSupplyOrderState { get; set; }

    [XmlElement("employee_firstname")]
    public string? EmployeeFirstname { get; set; }

    [XmlElement("employee_lastname")]
    public string? EmployeeLastname { get; set; }

    [XmlElement("quantity")]
    public int? Quantity { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    public bool ShouldSerializeIdSupplyOrderDetail() => IdSupplyOrderDetail.HasValue;
    public bool ShouldSerializeIdEmployee() => IdEmployee.HasValue;
    public bool ShouldSerializeIdSupplyOrderState() => IdSupplyOrderState.HasValue;
    public bool ShouldSerializeEmployeeFirstname() => !string.IsNullOrEmpty(EmployeeFirstname);
    public bool ShouldSerializeEmployeeLastname() => !string.IsNullOrEmpty(EmployeeLastname);
    public bool ShouldSerializeQuantity() => Quantity.HasValue;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
}
