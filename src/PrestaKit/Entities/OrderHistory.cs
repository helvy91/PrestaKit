using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("order_history")]
[ApiResource("order_histories")]
public class OrderHistory : PrestaShopEntity
{
    [XmlElement("id_employee")]
    public long? IdEmployee { get; set; }

    [XmlElement("id_order_state")]
    public long? IdOrderState { get; set; }

    [XmlElement("id_order")]
    public long? IdOrder { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    public bool ShouldSerializeIdEmployee() => IdEmployee.HasValue;
    public bool ShouldSerializeIdOrderState() => IdOrderState.HasValue;
    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
}