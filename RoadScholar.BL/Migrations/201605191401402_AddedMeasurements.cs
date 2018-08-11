namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMeasurements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OneMeasureDatas",
                c => new
                    {
                        OneMeasureID = c.Long(nullable: false, identity: true),
                        MeasurementID = c.Long(nullable: false),
                        MeasurementData_id = c.Long(),
                    })
                .PrimaryKey(t => t.OneMeasureID)
                .ForeignKey("dbo.Activities", t => t.MeasurementData_id)
                .Index(t => t.MeasurementData_id);
            
            AddColumn("dbo.Activities", "NumOfMeasures", c => c.Long());
            AddColumn("dbo.Activities", "DifferenceBetweenMeasures", c => c.Long());
            AddColumn("dbo.Activities", "NumOfParametersToMeasure", c => c.Long());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OneMeasureDatas", "MeasurementData_id", "dbo.Activities");
            DropIndex("dbo.OneMeasureDatas", new[] { "MeasurementData_id" });
            DropColumn("dbo.Activities", "NumOfParametersToMeasure");
            DropColumn("dbo.Activities", "DifferenceBetweenMeasures");
            DropColumn("dbo.Activities", "NumOfMeasures");
            DropTable("dbo.OneMeasureDatas");
        }
    }
}
