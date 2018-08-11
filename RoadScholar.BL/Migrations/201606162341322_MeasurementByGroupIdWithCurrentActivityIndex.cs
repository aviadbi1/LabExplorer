namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeasurementByGroupIdWithCurrentActivityIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeasurementByGroupIdDatas", "CurrentActivityIndex", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeasurementByGroupIdDatas", "CurrentActivityIndex");
        }
    }
}
