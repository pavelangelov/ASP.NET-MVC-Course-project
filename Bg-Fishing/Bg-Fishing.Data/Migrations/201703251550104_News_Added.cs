namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class News_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false, maxLength: 3500),
                        ImageUrl = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Comments", "News_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "News_Id");
            AddForeignKey("dbo.Comments", "News_Id", "dbo.News", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "News_Id", "dbo.News");
            DropIndex("dbo.Comments", new[] { "News_Id" });
            DropColumn("dbo.Comments", "News_Id");
            DropTable("dbo.News");
        }
    }
}
