using System.Text.Json.Serialization;

namespace _72220577_FEUTS.Model
{
    public class category
    {
        [JsonPropertyName("categoryId")]
        public int categoryId { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }
    }
}
