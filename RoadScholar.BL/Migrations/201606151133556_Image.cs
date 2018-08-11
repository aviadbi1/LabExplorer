namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Title", c => c.String());
            AddColumn("dbo.Activities", "URL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "URL");
            DropColumn("dbo.Activities", "Title");
        }
    }
}
