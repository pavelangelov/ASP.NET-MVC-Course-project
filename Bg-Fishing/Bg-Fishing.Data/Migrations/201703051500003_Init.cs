namespace Bg_Fishing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fish",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 25),
                        Info = c.String(),
                        FishType = c.Int(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Lakes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 25),
                        Info = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ImageGalleries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 25),
                        LakeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lakes", t => t.LakeId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.LakeId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ImageUrl = c.String(nullable: false),
                        Date = c.DateTime(),
                        Info = c.String(),
                        ImageGallery_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageGalleries", t => t.ImageGallery_Id)
                .Index(t => t.ImageGallery_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Info = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoGalleries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Url = c.String(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        VideoGallery_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VideoGalleries", t => t.VideoGallery_Id)
                .Index(t => t.VideoGallery_Id);
            
            CreateTable(
                "dbo.LakeFish",
                c => new
                    {
                        Lake_Id = c.String(nullable: false, maxLength: 128),
                        Fish_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Lake_Id, t.Fish_Id })
                .ForeignKey("dbo.Lakes", t => t.Lake_Id, cascadeDelete: true)
                .ForeignKey("dbo.Fish", t => t.Fish_Id, cascadeDelete: true)
                .Index(t => t.Lake_Id)
                .Index(t => t.Fish_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "VideoGallery_Id", "dbo.VideoGalleries");
            DropForeignKey("dbo.ImageGalleries", "LakeId", "dbo.Lakes");
            DropForeignKey("dbo.Images", "ImageGallery_Id", "dbo.ImageGalleries");
            DropForeignKey("dbo.LakeFish", "Fish_Id", "dbo.Fish");
            DropForeignKey("dbo.LakeFish", "Lake_Id", "dbo.Lakes");
            DropIndex("dbo.LakeFish", new[] { "Fish_Id" });
            DropIndex("dbo.LakeFish", new[] { "Lake_Id" });
            DropIndex("dbo.Videos", new[] { "VideoGallery_Id" });
            DropIndex("dbo.VideoGalleries", new[] { "Name" });
            DropIndex("dbo.Images", new[] { "ImageGallery_Id" });
            DropIndex("dbo.ImageGalleries", new[] { "LakeId" });
            DropIndex("dbo.ImageGalleries", new[] { "Name" });
            DropIndex("dbo.Lakes", new[] { "Name" });
            DropIndex("dbo.Fish", new[] { "Name" });
            DropTable("dbo.LakeFish");
            DropTable("dbo.Videos");
            DropTable("dbo.VideoGalleries");
            DropTable("dbo.Locations");
            DropTable("dbo.Images");
            DropTable("dbo.ImageGalleries");
            DropTable("dbo.Lakes");
            DropTable("dbo.Fish");
        }
    }
}
