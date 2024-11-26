using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

public class XMLDetails
{
    [Key]
    public int XmlId { get; set; }

    [Column(TypeName = "Date")]
    [JsonConverter(typeof(JsonDateConverter))]
    public DateTime DataXml { get; set; }

    [Column(TypeName = "Text")]
    public string XmlContent { get; set; } = "";
}

public class JsonDateConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}