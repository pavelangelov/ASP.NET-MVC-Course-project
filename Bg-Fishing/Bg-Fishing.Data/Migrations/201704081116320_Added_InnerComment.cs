namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_InnerComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InnerComments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false),
                        PostedDate = c.DateTime(nullable: false),
                        Username = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InnerComments");
        }
    }
}
