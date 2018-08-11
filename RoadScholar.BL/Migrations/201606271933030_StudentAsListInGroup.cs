namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAsListInGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupDatas", "studentListAsString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupDatas", "studentListAsString");
        }
    }
}
