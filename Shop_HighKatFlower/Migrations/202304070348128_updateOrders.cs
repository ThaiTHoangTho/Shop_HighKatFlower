namespace Shop_HighKatFlower.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "Quanity", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "Price", c => c.String());
            AlterColumn("dbo.OrderDetails", "Quanity", c => c.String());
        }
    }
}
