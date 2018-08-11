namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldToMeasurementWindowTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "WindowOpenTimeSeconds", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "WindowOpenTimeSeconds");
        }
    }
}
