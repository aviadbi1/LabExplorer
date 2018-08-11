namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeasurementInstructionForeignKey2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MeasureInstructionDatas", "Measurement_id", "dbo.Activities");
            DropIndex("dbo.MeasureInstructionDatas", new[] { "Measurement_id" });
            DropColumn("dbo.MeasureInstructionDatas", "Measurement_id");
            AddColumn("dbo.MeasureInstructionDatas", "MeasurementData_id", c => c.Long());
            CreateIndex("dbo.MeasureInstructionDatas", "MeasurementData_id");
            AddForeignKey("dbo.MeasureInstructionDatas", "MeasurementData_id", "dbo.Activities", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeasureInstructionDatas", "MeasurementData_id", "dbo.Activities");
            DropIndex("dbo.MeasureInstructionDatas", new[] { "MeasurementData_id" });
            AlterColumn("dbo.MeasureInstructionDatas", "MeasurementData_id", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.MeasureInstructionDatas", name: "MeasurementData_id", newName: "MeasurementID");
            CreateIndex("dbo.MeasureInstructionDatas", "MeasurementID");
            AddForeignKey("dbo.MeasureInstructionDatas", "MeasurementID", "dbo.Activities", "id", cascadeDelete: true);
        }
    }
}
