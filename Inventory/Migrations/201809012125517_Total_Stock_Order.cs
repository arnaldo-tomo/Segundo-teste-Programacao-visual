namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Total_Stock_Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Total_Stock_Order",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        invoice_no_t = c.Int(nullable: false),
                        total_order_amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Total_Stock_Order");
        }
    }
}
