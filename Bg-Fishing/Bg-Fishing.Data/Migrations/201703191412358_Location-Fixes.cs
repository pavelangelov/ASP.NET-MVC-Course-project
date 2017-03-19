namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lakes", "Location_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Lakes", "Location_Id");
            AddForeignKey("dbo.Lakes", "Location_Id", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lakes", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Lakes", new[] { "Location_Id" });
            DropColumn("dbo.Lakes", "Location_Id");
        }
    }
}
