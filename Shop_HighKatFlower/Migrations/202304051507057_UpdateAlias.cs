namespace Shop_HighKatFlower.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAlias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Alias", c => c.String());
            AddColumn("dbo.News", "Alias", c => c.String());
            AddColumn("dbo.Posts", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Alias");
            DropColumn("dbo.News", "Alias");
            DropColumn("dbo.Categories", "Alias");
        }
    }
}
