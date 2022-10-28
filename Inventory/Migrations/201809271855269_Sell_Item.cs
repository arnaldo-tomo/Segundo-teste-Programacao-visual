namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sell_Item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sell_Item",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        invoice = c.Int(nullable: false),
                        bill_date = c.DateTime(nullable: false),
                        customer_name = c.String(),
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
            DropTable("dbo.Sell_Item");
        }
    }
}
