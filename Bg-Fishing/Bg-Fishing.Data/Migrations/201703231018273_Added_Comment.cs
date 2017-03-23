namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Comment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                        LakeName = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        Comment_Id = c.String(maxLength: 128),
                        Lake_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .ForeignKey("dbo.Lakes", t => t.Lake_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.Lake_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Lake_Id", "dbo.Lakes");
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "Lake_Id" });
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropTable("dbo.Comments");
        }
    }
}
