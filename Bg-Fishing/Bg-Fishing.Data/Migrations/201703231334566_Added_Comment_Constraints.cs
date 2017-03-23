namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Comment_Constraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Comments", "LakeName", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Username", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Username", c => c.String());
            AlterColumn("dbo.Comments", "LakeName", c => c.String());
            AlterColumn("dbo.Comments", "Content", c => c.String());
        }
    }
}
