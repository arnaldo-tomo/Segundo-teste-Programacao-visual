namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sell_Item2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sell_Item", "date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sell_Item", "date");
        }
    }
}
