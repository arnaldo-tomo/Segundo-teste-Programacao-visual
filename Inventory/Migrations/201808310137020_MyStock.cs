namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyStock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyStock",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        invoice = c.Int(nullable: false),
                        bill_date = c.DateTime(nullable: false),
                        supplier_name = c.String(),
                        receive_date = c.DateTime(nullable: false),
                        carrying_charge = c.Int(nullable: false),
                        item_name = c.String(),
                        quantity = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                        total_price_per_item = c.Int(nullable: false),
                        total_amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyStock");
        }
    }
}
