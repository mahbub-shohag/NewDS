namespace DS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "ImageOriginal", c => c.String());
            AddColumn("dbo.Services", "ImageThumb", c => c.String());
            AddColumn("dbo.Services", "ImageMid", c => c.String());
            DropColumn("dbo.Services", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "ImagePath", c => c.String());
            DropColumn("dbo.Services", "ImageMid");
            DropColumn("dbo.Services", "ImageThumb");
            DropColumn("dbo.Services", "ImageOriginal");
        }
    }
}
