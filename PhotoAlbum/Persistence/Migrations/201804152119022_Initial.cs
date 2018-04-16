namespace Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 32),
                    Parent_Id = c.Guid(),
                    User_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Parent_Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Parent_Id)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.Images",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 32),
                    CreatedOn = c.DateTime(nullable: false),
                    Description = c.String(maxLength: 256),
                    Data = c.Binary(nullable: false),
                    Category_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 32),
                    PasswordHash = c.Binary(nullable: false, maxLength: 32, fixedLength: true),
                    FullName = c.String(nullable: false, maxLength: 64),
                    Email = c.String(nullable: false, maxLength: 64),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Categories", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Images", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Images", new[] { "Category_Id" });
            DropIndex("dbo.Categories", new[] { "User_Id" });
            DropIndex("dbo.Categories", new[] { "Parent_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Images");
            DropTable("dbo.Categories");
        }
    }
}
