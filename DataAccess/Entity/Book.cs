using System.Text.Json.Serialization;

namespace DataAccess.Entity
{
    public class Book
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("year")]
        public int Year { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("author")]
        public string Author { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}