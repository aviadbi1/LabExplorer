namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedmeasurementsToGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "GroupData_GroupID", c => c.Long());
            CreateIndex("dbo.Activities", "GroupData_GroupID");
            AddForeignKey("dbo.Activities", "GroupData_GroupID", "dbo.GroupDatas", "GroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "GroupData_GroupID", "dbo.GroupDatas");
            DropIndex("dbo.Activities", new[] { "GroupData_GroupID" });
            DropColumn("dbo.Activities", "GroupData_GroupID");
        }
    }
}
