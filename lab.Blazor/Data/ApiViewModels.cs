using System.Text.Json.Serialization;

namespace lab.Blazor.Data
{
    public class MusInstrumentListViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }

    public class MusInstrumentDetailsViewModel
    {

        [JsonPropertyName("name")]
        public string Name { get; set; } 
        
        [JsonPropertyName("description")]
        public string Description { get; set; } 
        
        [JsonPropertyName("price")]
        public double Price { get; set; } 
        
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
