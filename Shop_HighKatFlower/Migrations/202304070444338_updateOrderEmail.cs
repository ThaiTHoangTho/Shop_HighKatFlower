namespace Shop_HighKatFlower.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrderEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Email");
        }
    }
}
