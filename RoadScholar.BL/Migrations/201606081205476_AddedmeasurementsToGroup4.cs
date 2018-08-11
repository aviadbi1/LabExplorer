namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedmeasurementsToGroup4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.MeasurementByGroupIdDatas", "GroupID");
            AddForeignKey("dbo.MeasurementByGroupIdDatas", "GroupID", "dbo.GroupDatas", "GroupID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeasurementByGroupIdDatas", "GroupID", "dbo.GroupDatas");
            DropIndex("dbo.MeasurementByGroupIdDatas", new[] { "GroupID" });
        }
    }
}
