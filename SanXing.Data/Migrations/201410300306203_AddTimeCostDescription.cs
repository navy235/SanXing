namespace SanXing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeCostDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeCostType", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeCostType", "Description");
        }
    }
}
