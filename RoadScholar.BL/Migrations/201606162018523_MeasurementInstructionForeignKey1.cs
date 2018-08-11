namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeasurementInstructionForeignKey1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeasureInstructionDatas", "Measurement_id", c => c.Long());
            CreateIndex("dbo.MeasureInstructionDatas", "Measurement_id");
            AddForeignKey("dbo.MeasureInstructionDatas", "Measurement_id", "dbo.Activities", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeasureInstructionDatas", "Measurement_id", "dbo.Activities");
            DropIndex("dbo.MeasureInstructionDatas", new[] { "Measurement_id" });
            DropColumn("dbo.MeasureInstructionDatas", "Measurement_id");
        }
    }
}
