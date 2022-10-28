namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Suppliers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        sup_id = c.Int(nullable: false, identity: true),
                        sup_name = c.String(),
                        mobile = c.Int(nullable: false),
                        email = c.String(),
                        address = c.String(),
                    })
                .PrimaryKey(t => t.sup_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Suppliers");
        }
    }
}
