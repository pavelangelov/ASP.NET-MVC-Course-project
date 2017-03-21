namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Location_Constraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Info", c => c.String(maxLength: 3000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Info", c => c.String());
        }
    }
}
