namespace DS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2changeserviceModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "DisplayDetail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "DisplayDetail");
        }
    }
}
