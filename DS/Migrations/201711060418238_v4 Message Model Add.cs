namespace DS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4MessageModelAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.MessageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
