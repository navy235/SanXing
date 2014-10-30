namespace SanXing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateScoreFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactWay", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactWay", "Score", c => c.String());
        }
    }
}
