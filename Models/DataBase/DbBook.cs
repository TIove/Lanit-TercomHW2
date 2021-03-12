using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.DataBase
{
    public class DbBook
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Year { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
    
    public class DbBookConfiguration : IEntityTypeConfiguration<DbBook>
    {
        public void Configure(EntityTypeBuilder<DbBook> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(u => u.Id);
        }
    }
}