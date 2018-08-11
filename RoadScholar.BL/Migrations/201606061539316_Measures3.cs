namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Measures3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeasurementByGroupIdDatas",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        expID = c.Long(nullable: false),
                        RoomId = c.Long(nullable: false),
                        GroupID = c.Long(nullable: false),
                        NumOfMeasures = c.Long(nullable: false),
                        DifferenceBetweenMeasures = c.Long(nullable: false),
                        NumOfParametersToMeasure = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.OneMeasureByGroupIdDatas",
                c => new
                    {
                        OneMeasureByGroupIDid = c.Long(nullable: false, identity: true),
                        MeasurementByGroupIDid = c.Long(nullable: false),
                        MeasurementByGroupIdData_id = c.Long(),
                    })
                .PrimaryKey(t => t.OneMeasureByGroupIDid)
                .ForeignKey("dbo.MeasurementByGroupIdDatas", t => t.MeasurementByGroupIdData_id)
                .Index(t => t.MeasurementByGroupIdData_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OneMeasureByGroupIdDatas", "MeasurementByGroupIdData_id", "dbo.MeasurementByGroupIdDatas");
            DropIndex("dbo.OneMeasureByGroupIdDatas", new[] { "MeasurementByGroupIdData_id" });
            DropTable("dbo.OneMeasureByGroupIdDatas");
            DropTable("dbo.MeasurementByGroupIdDatas");
        }
    }
}
