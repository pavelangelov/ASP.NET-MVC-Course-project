namespace Bg_Fishing.Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Users_Constraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.AspNetUsers", "MiddleName", c => c.String(maxLength: 35));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
        }
    }
}
