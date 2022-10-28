namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Total_Customer_Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Total_Customer_Order",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        invoice_no_tc = c.Int(nullable: false),
                        total_order_amount = c.Int(nullable: false),
                        date = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Total_Customer_Order");
        }
    }
}
