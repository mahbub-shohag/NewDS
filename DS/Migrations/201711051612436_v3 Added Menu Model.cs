namespace DS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3AddedMenuModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        ParentId = c.Int(nullable: false),
                        DisplayPosition = c.Int(nullable: false),
                        Controller = c.String(),
                        Action = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Menus");
        }
    }
}
