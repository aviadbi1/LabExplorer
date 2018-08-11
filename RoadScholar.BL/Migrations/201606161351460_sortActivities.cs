namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sortActivities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "experimentOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "experimentOrder");
        }
    }
}
