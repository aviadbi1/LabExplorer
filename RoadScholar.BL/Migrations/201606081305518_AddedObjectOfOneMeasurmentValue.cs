namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedObjectOfOneMeasurmentValue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OneMeasurementEvalDatas",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        value = c.Double(nullable: false),
                        OneMeasureByGroupIdData_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.OneMeasureByGroupIdDatas", t => t.OneMeasureByGroupIdData_id)
                .Index(t => t.OneMeasureByGroupIdData_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OneMeasurementEvalDatas", "OneMeasureByGroupIdData_id", "dbo.OneMeasureByGroupIdDatas");
            DropIndex("dbo.OneMeasurementEvalDatas", new[] { "OneMeasureByGroupIdData_id" });
            DropTable("dbo.OneMeasurementEvalDatas");
        }
    }
}
