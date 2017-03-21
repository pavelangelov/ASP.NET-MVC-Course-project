namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FishConstraintsAdded : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Fish", new[] { "Name" });
            AlterColumn("dbo.Fish", "Name", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.Fish", "Info", c => c.String(maxLength: 3000));
            CreateIndex("dbo.Fish", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Fish", new[] { "Name" });
            AlterColumn("dbo.Fish", "Info", c => c.String());
            AlterColumn("dbo.Fish", "Name", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.Fish", "Name", unique: true);
        }
    }
}
