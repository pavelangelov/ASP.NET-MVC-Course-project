namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Comments : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            AddColumn("dbo.InnerComments", "Comment_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.InnerComments", "Comment_Id");
            DropColumn("dbo.Comments", "Comment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Comment_Id", c => c.String(maxLength: 128));
            DropIndex("dbo.InnerComments", new[] { "Comment_Id" });
            DropColumn("dbo.InnerComments", "Comment_Id");
            CreateIndex("dbo.Comments", "Comment_Id");
        }
    }
}
