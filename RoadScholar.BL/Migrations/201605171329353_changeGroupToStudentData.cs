namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeGroupToStudentData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "GroupID", c => c.Long(nullable: false));
            CreateIndex("dbo.Students", "GroupID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Students", new[] { "GroupID" });
            DropColumn("dbo.Students", "GroupID");
        }
    }
}
