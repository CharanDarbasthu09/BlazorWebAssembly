using System.Text.Json.Serialization;

namespace BlazorIntAuto.Data
{
  public class DashboardImage
  {
    [JsonPropertyName("image")]
    public required string Image { get; set; }

    [JsonPropertyName("info")]
    public required string Info { get; set; }

    [JsonPropertyName("size")]
    public required string Size { get; set; }
  }
}
