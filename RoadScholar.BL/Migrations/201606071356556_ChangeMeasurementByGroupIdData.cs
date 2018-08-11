namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMeasurementByGroupIdData : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MeasurementByGroupIdDatas", "NumOfMeasures");
            DropColumn("dbo.MeasurementByGroupIdDatas", "DifferenceBetweenMeasures");
            DropColumn("dbo.MeasurementByGroupIdDatas", "NumOfParametersToMeasure");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MeasurementByGroupIdDatas", "NumOfParametersToMeasure", c => c.Long(nullable: false));
            AddColumn("dbo.MeasurementByGroupIdDatas", "DifferenceBetweenMeasures", c => c.Long(nullable: false));
            AddColumn("dbo.MeasurementByGroupIdDatas", "NumOfMeasures", c => c.Long(nullable: false));
        }
    }
}
