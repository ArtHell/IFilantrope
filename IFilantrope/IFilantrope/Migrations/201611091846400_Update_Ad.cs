namespace IFilantrope.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Ad : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Ads", name: "Author_Id", newName: "AuthorId");
            RenameIndex(table: "dbo.Ads", name: "IX_Author_Id", newName: "IX_AuthorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Ads", name: "IX_AuthorId", newName: "IX_Author_Id");
            RenameColumn(table: "dbo.Ads", name: "AuthorId", newName: "Author_Id");
        }
    }
}
