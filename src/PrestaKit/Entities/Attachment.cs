using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("attachment")]
[ApiResource("attachments")]
public class Attachment : PrestaShopEntity
{
    [XmlElement("file")]
    public string? File { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("file_name")]
    public string? FileName { get; set; }

    [XmlElement("file_size")]
    public long? FileSize { get; set; }

    [XmlElement("mime")]
    public string? Mime { get; set; }

    [XmlElement("description")]
    public TranslatedField? Description { get; set; }

    [XmlElement("associations")] 
    public AttachmentAssociations? Associations { get; set; }

    public bool ShouldSerializeFile() => !string.IsNullOrEmpty(File);
    public bool ShouldSerializeFileName() => !string.IsNullOrEmpty(FileName);
    public bool ShouldSerializeFileSize() => FileSize.HasValue;
    public bool ShouldSerializeMime() => !string.IsNullOrEmpty(Mime);
    public bool ShouldSerializeDescription() => Description != null;
    public bool ShouldSerializeAssociations() => Associations != null;
}