namespace IFilantrope.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Ad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ads", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ads", new[] { "Author_Id" });
            DropTable("dbo.Ads");
        }
    }
}
