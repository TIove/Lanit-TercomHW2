using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.DataBase
{
    public class DbAuthorBook
    {
        private static int _lastId = 1;
        public DbAuthorBook(int bookId, string author)
        {
            BookId = bookId;
            Author = author;
            Id = _lastId;
            _lastId++;
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Author { get; set; }
    }
    public class DbAuthorBookConfiguration : IEntityTypeConfiguration<DbAuthorBook>
    {
        public void Configure(EntityTypeBuilder<DbAuthorBook> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Authors");
        }
    }
}