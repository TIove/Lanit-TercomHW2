using System.Text.Json.Serialization;
using Models.DataBase;
using Models.Enums;

namespace Models
{
    public class BookRequest
    {
        [JsonIgnore]
        public RequestMode Mode { get; set; }

        public int Id { get; set; }

        public int Year { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Author { get; set; }
    }
}