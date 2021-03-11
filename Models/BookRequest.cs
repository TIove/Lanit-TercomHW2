using Models.Enums;

namespace Models
{
    public class BookRequest
    {
        public RequestMode Mode { get; set; }

        public int Id { get; set; }

        public Book ModelBody { get; set; }
    }
}