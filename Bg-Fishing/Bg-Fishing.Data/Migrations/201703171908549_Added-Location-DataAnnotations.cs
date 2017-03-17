namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLocationDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Name", c => c.String(maxLength: 50));
            CreateIndex("dbo.Locations", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Locations", new[] { "Name" });
            AlterColumn("dbo.Locations", "Name", c => c.String());
        }
    }
}
