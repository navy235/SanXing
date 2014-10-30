namespace SanXing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cost", "CostDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cost", "Money", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cost", "Money", c => c.Double(nullable: false));
            DropColumn("dbo.Cost", "CostDate");
        }
    }
}
