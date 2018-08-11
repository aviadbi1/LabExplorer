namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Video : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "URL1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "URL1");
        }
    }
}
