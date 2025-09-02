using Newtonsoft.Json;

namespace EFakturCallback.Data.Entities
{
    public class FakturDetailStream
{
    [JsonProperty("no_faktur_dummy")]
    public string? no_faktur_dummy { get; set; }

    [JsonProperty("no_faktur_coretax")]
    public string? no_faktur_coretax { get; set; }

    [JsonProperty("company")]
    public string? company { get; set; }

    [JsonProperty("referensi")]
    public string? referensi { get; set; }
}
}