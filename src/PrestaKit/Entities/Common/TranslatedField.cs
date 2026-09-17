using System.Diagnostics;
using System.Xml.Serialization;

namespace PrestaKit.Entities.Common;

public sealed class TranslatedField
{
    [XmlElement("language")]
    public List<LanguageValue> Values { get; set; } = [];

    public const int DefaultLanguageId = 1;

    public static implicit operator TranslatedField(string value)
        => new() { Values = { new LanguageValue { LanguageId = DefaultLanguageId, Value = value } } };

    public static implicit operator string?(TranslatedField? field)
        => field?.Values.FirstOrDefault(v => v.LanguageId == DefaultLanguageId)?.Value
           ?? field?.Values.FirstOrDefault()?.Value;

    public string? this[int languageId]
    {
        get => Values.FirstOrDefault(v => v.LanguageId == languageId)?.Value;
        set
        {
            var existing = Values.FirstOrDefault(v => v.LanguageId == languageId);
            if (existing is not null)
            {
                existing.Value = value ?? "";
            }
            else
            {
                Values.Add(new LanguageValue { LanguageId = languageId, Value = value ?? "" });
            }
        }
    }
}

public sealed class LanguageValue
{
    [XmlAttribute("id")]
    public int LanguageId { get; set; }

    [XmlText]
    public string Value { get; set; } = "";
}