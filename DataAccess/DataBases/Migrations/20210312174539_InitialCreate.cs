using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.DataBases.Migrations
{
    [DbContext(typeof(BooksDbContext))]
    [Migration("20210312174539_InitialCreate")]
    public class InitialCreate : Migration
    {
        private void CreateBookStore(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Books",
                table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false) 
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                }
            );
        }
        private void CreateBookAuthors(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Authors",
                table => new
                {
                    Id = table. Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Books",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id", /// mb BookId
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );
        }
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateBookStore(migrationBuilder);
            CreateBookAuthors(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Books");
            migrationBuilder.DropTable("Authors");
        }
    }
}