namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Images : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "IsConfirmed");
        }
    }
}
