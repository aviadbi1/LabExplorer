namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OneMeasureByGroupIdDatas", "MeasurementByGroupIdData_id", "dbo.MeasurementByGroupIdDatas");
            DropIndex("dbo.OneMeasureByGroupIdDatas", new[] { "MeasurementByGroupIdData_id" });
            DropTable("dbo.MeasurementByGroupIdDatas");
            DropTable("dbo.OneMeasureByGroupIdDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OneMeasureByGroupIdDatas",
                c => new
                    {
                        OneMeasureByGroupIDid = c.Long(nullable: false, identity: true),
                        MeasurementByGroupIDid = c.Long(nullable: false),
                        MeasurementByGroupIdData_id = c.Long(),
                    })
                .PrimaryKey(t => t.OneMeasureByGroupIDid);
            
            CreateTable(
                "dbo.MeasurementByGroupIdDatas",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        expID = c.Long(nullable: false),
                        RoomId = c.Long(nullable: false),
                        GroupID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.OneMeasureByGroupIdDatas", "MeasurementByGroupIdData_id");
            AddForeignKey("dbo.OneMeasureByGroupIdDatas", "MeasurementByGroupIdData_id", "dbo.MeasurementByGroupIdDatas", "id");
        }
    }
}
