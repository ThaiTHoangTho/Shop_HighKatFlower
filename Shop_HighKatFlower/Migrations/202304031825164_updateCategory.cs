namespace Shop_HighKatFlower.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Categories", "SeoTitle", c => c.String(maxLength: 150));
            AlterColumn("dbo.Categories", "SeoDescription", c => c.String(maxLength: 150));
            AlterColumn("dbo.Categories", "SeoKeywords", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "SeoKeywords", c => c.String());
            AlterColumn("dbo.Categories", "SeoDescription", c => c.String());
            AlterColumn("dbo.Categories", "SeoTitle", c => c.String());
            AlterColumn("dbo.Categories", "Title", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
