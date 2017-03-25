namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsComment_Added : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "News_Id" });
            CreateTable(
                "dbo.NewsComments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                        Username = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                        News_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.News_Id);
            
            DropColumn("dbo.Comments", "News_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "News_Id", c => c.String(maxLength: 128));
            DropIndex("dbo.NewsComments", new[] { "News_Id" });
            DropTable("dbo.NewsComments");
            CreateIndex("dbo.Comments", "News_Id");
        }
    }
}
