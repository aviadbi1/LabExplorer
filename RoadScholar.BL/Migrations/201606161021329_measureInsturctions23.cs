namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class measureInsturctions23 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MeasureInstructionDatas");

            CreateTable(
                            "dbo.MeasureInstructionDatas",
                            c => new
                            {
                                id = c.Long(nullable: false, identity: true),
                                MeasurementInstruction = c.String(),
                            })
                            .PrimaryKey(t => t.id);
        }
        
        public override void Down()
        {
        }
    }
}
