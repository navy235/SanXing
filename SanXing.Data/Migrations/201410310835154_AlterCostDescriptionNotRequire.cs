namespace SanXing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCostDescriptionNotRequire : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cost", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cost", "Description", c => c.String(nullable: false));
        }
    }
}
