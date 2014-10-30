namespace SanXing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false),
                        Sentiment = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        BlogTypeID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Url = c.String(nullable: false, maxLength: 20),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogType", t => t.BlogTypeID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.BlogTypeID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.BlogType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CateName = c.String(nullable: false, maxLength: 50),
                        PID = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        Level = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogType", t => t.PID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.PID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        Phone = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        ActiveStatus = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Sex = c.Boolean(nullable: false),
                        Age = c.Int(nullable: false),
                        Avtar = c.String(maxLength: 500),
                        Advantages = c.String(maxLength: 500),
                        Weakness = c.String(maxLength: 500),
                        Mettle = c.String(maxLength: 500),
                        Description = c.String(maxLength: 500),
                        RichTypeID = c.Int(nullable: false),
                        ContactScore = c.Int(nullable: false),
                        ContactTypeID = c.Int(nullable: false),
                        Pinyin = c.String(),
                        UserID = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactType", t => t.ContactTypeID)
                .ForeignKey("dbo.RichType", t => t.RichTypeID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.RichTypeID)
                .Index(t => t.ContactTypeID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ContactRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContactID = c.Int(nullable: false),
                        ContactWayID = c.Int(nullable: false),
                        Problem = c.String(nullable: false, maxLength: 500),
                        Description = c.String(nullable: false, maxLength: 500),
                        Sentiment = c.String(nullable: false, maxLength: 500),
                        Score = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contact", t => t.ContactID)
                .ForeignKey("dbo.ContactWay", t => t.ContactWayID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.ContactID)
                .Index(t => t.ContactWayID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ContactWay",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CateName = c.String(nullable: false, maxLength: 50),
                        PID = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        Level = c.Int(nullable: false),
                        Score = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactWay", t => t.PID)
                .Index(t => t.PID);
            
            CreateTable(
                "dbo.ContactType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CateName = c.String(nullable: false, maxLength: 50),
                        PID = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        Level = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactType", t => t.PID)
                .Index(t => t.PID);
            
            CreateTable(
                "dbo.RichType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CateName = c.String(nullable: false, maxLength: 50),
                        PID = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        Level = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RichType", t => t.PID)
                .Index(t => t.PID);
            
            CreateTable(
                "dbo.Cost",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Money = c.Double(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        CostTypeID = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        UserID = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CostType", t => t.CostTypeID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.CostTypeID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.CostType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CateName = c.String(nullable: false, maxLength: 50),
                        PID = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        Level = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CostType", t => t.PID)
                .Index(t => t.PID);
            
            CreateTable(
                "dbo.TimeCost",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Hour = c.Int(nullable: false),
                        TimeCostTypeID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TimeCostType", t => t.TimeCostTypeID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.TimeCostTypeID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.TimeCostType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CateName = c.String(nullable: false, maxLength: 50),
                        PID = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        Level = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TimeCostType", t => t.PID)
                .Index(t => t.PID);
            
            CreateTable(
                "dbo.Plan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        ContactID = c.Int(),
                        ContactCount = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blog", "UserID", "dbo.User");
            DropForeignKey("dbo.Blog", "BlogTypeID", "dbo.BlogType");
            DropForeignKey("dbo.BlogType", "UserID", "dbo.User");
            DropForeignKey("dbo.TimeCost", "UserID", "dbo.User");
            DropForeignKey("dbo.TimeCost", "TimeCostTypeID", "dbo.TimeCostType");
            DropForeignKey("dbo.TimeCostType", "PID", "dbo.TimeCostType");
            DropForeignKey("dbo.Cost", "UserID", "dbo.User");
            DropForeignKey("dbo.Cost", "CostTypeID", "dbo.CostType");
            DropForeignKey("dbo.CostType", "PID", "dbo.CostType");
            DropForeignKey("dbo.Contact", "UserID", "dbo.User");
            DropForeignKey("dbo.Contact", "RichTypeID", "dbo.RichType");
            DropForeignKey("dbo.RichType", "PID", "dbo.RichType");
            DropForeignKey("dbo.Contact", "ContactTypeID", "dbo.ContactType");
            DropForeignKey("dbo.ContactType", "PID", "dbo.ContactType");
            DropForeignKey("dbo.ContactRecord", "UserID", "dbo.User");
            DropForeignKey("dbo.ContactRecord", "ContactWayID", "dbo.ContactWay");
            DropForeignKey("dbo.ContactWay", "PID", "dbo.ContactWay");
            DropForeignKey("dbo.ContactRecord", "ContactID", "dbo.Contact");
            DropForeignKey("dbo.BlogType", "PID", "dbo.BlogType");
            DropIndex("dbo.TimeCostType", new[] { "PID" });
            DropIndex("dbo.TimeCost", new[] { "UserID" });
            DropIndex("dbo.TimeCost", new[] { "TimeCostTypeID" });
            DropIndex("dbo.CostType", new[] { "PID" });
            DropIndex("dbo.Cost", new[] { "UserID" });
            DropIndex("dbo.Cost", new[] { "CostTypeID" });
            DropIndex("dbo.RichType", new[] { "PID" });
            DropIndex("dbo.ContactType", new[] { "PID" });
            DropIndex("dbo.ContactWay", new[] { "PID" });
            DropIndex("dbo.ContactRecord", new[] { "UserID" });
            DropIndex("dbo.ContactRecord", new[] { "ContactWayID" });
            DropIndex("dbo.ContactRecord", new[] { "ContactID" });
            DropIndex("dbo.Contact", new[] { "UserID" });
            DropIndex("dbo.Contact", new[] { "ContactTypeID" });
            DropIndex("dbo.Contact", new[] { "RichTypeID" });
            DropIndex("dbo.BlogType", new[] { "UserID" });
            DropIndex("dbo.BlogType", new[] { "PID" });
            DropIndex("dbo.Blog", new[] { "UserID" });
            DropIndex("dbo.Blog", new[] { "BlogTypeID" });
            DropTable("dbo.Plan");
            DropTable("dbo.TimeCostType");
            DropTable("dbo.TimeCost");
            DropTable("dbo.CostType");
            DropTable("dbo.Cost");
            DropTable("dbo.RichType");
            DropTable("dbo.ContactType");
            DropTable("dbo.ContactWay");
            DropTable("dbo.ContactRecord");
            DropTable("dbo.Contact");
            DropTable("dbo.User");
            DropTable("dbo.BlogType");
            DropTable("dbo.Blog");
        }
    }
}
