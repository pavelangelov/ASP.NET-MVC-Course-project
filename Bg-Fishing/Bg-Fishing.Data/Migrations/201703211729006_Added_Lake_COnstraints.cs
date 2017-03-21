namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Lake_COnstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Lakes", new[] { "Name" });
            AlterColumn("dbo.Lakes", "Name", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.Lakes", "Info", c => c.String(maxLength: 3000));
            CreateIndex("dbo.Lakes", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Lakes", new[] { "Name" });
            AlterColumn("dbo.Lakes", "Info", c => c.String());
            AlterColumn("dbo.Lakes", "Name", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.Lakes", "Name", unique: true);
        }
    }
}
