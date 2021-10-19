namespace Assignment5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false, maxLength: 100),
                        Content = c.Binary(),
                        ContentType = c.String(),
                        StringId = c.String(nullable: false, maxLength: 100),
                        TimeStamp = c.DateTime(nullable: false),
                        Artist_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .Index(t => t.Artist_Id);
            
            AddColumn("dbo.Albums", "Depiction", c => c.String());
            AddColumn("dbo.Artists", "Portrayal", c => c.String());
            AddColumn("dbo.Tracks", "ClipContentType", c => c.String());
            AddColumn("dbo.Tracks", "ClipContent", c => c.Binary());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MediaItems", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.MediaItems", new[] { "Artist_Id" });
            DropColumn("dbo.Tracks", "ClipContent");
            DropColumn("dbo.Tracks", "ClipContentType");
            DropColumn("dbo.Artists", "Portrayal");
            DropColumn("dbo.Albums", "Depiction");
            DropTable("dbo.MediaItems");
        }
    }
}
