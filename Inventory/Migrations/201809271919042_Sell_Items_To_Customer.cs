namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sell_Items_To_Customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sell_Items_To_Customer",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        invoice = c.Int(nullable: false),
                        bill_date = c.String(),
                        customer_name = c.String(),
                        item_name = c.String(),
                        quantity = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                        total_price_per_item = c.Int(nullable: false),
                        total_amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AlterColumn("dbo.Sell_Item", "bill_date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sell_Item", "bill_date", c => c.DateTime(nullable: false));
            DropTable("dbo.Sell_Items_To_Customer");
        }
    }
}
