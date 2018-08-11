namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoggedInStudents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "RoomData_id", c => c.Long());
            CreateIndex("dbo.Students", "RoomData_id");
            AddForeignKey("dbo.Students", "RoomData_id", "dbo.Rooms", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "RoomData_id", "dbo.Rooms");
            DropIndex("dbo.Students", new[] { "RoomData_id" });
            DropColumn("dbo.Students", "RoomData_id");
        }
    }
}
