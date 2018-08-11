namespace RoadScholar.BL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGrou : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActiveExperiments",
                c => new
                    {
                        ActiveExpID = c.Long(nullable: false, identity: true),
                        ExpID = c.Long(nullable: false),
                        NumberOfGroups = c.Int(nullable: false),
                        MaxStudentPerGroup = c.Int(nullable: false),
                        NumberCreatedGroups = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActiveExpID);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        ActivityName = c.String(),
                        isMainActivity = c.Boolean(nullable: false),
                        expID = c.Long(nullable: false),
                        RoomId = c.Long(nullable: false),
                        Type = c.String(),
                        ExperimentQuestion = c.String(),
                        ActiveExpID = c.Long(),
                        Command = c.String(),
                        question = c.String(),
                        explaination = c.String(),
                        correctAnswer = c.Int(),
                        firstAnswer = c.String(),
                        secondAnswer = c.String(),
                        thirdAnswer = c.String(),
                        fourthAnswer = c.String(),
                        counterFirst = c.Int(),
                        counterSecond = c.Int(),
                        counterThird = c.Int(),
                        counterFourth = c.Int(),
                        correctAnswerString = c.String(),
                        correctAnswerBool = c.Boolean(),
                        counterTrue = c.Int(),
                        counterFalse = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ExperimentData_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Activities", t => t.ExperimentData_id)
                .Index(t => t.ExperimentData_id);
            
            CreateTable(
                "dbo.AnswerByPhones",
                c => new
                    {
                        ActivityId = c.Long(nullable: false),
                        Phone = c.String(),
                        Id = c.Long(nullable: false, identity: true),
                        Answer = c.String(),
                        RoomID = c.Long(nullable: false),
                        QuestionData_id = c.Long(),
                        QLogData_id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.QuestionData_id)
                .ForeignKey("dbo.ActivityLogs", t => t.QLogData_id)
                .Index(t => t.QuestionData_id)
                .Index(t => t.QLogData_id);
            
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        ActivityName = c.String(),
                        isMainActivity = c.Boolean(nullable: false),
                        expID = c.Long(nullable: false),
                        RoomId = c.Long(nullable: false),
                        date = c.DateTime(nullable: false),
                        question = c.String(),
                        explaination = c.String(),
                        correctAnswer = c.Int(),
                        firstAnswer = c.String(),
                        secondAnswer = c.String(),
                        thirdAnswer = c.String(),
                        fourthAnswer = c.String(),
                        counterFirst = c.Int(),
                        counterSecond = c.Int(),
                        counterThird = c.Int(),
                        counterFourth = c.Int(),
                        correctAnswerString = c.String(),
                        correctAnswerBool = c.Boolean(),
                        counterTrue = c.Int(),
                        counterFalse = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.GroupDatas",
                c => new
                    {
                        GroupID = c.Long(nullable: false, identity: true),
                        ActiveExpID = c.Long(nullable: false),
                        Progress = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        CurrentActivityId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        PhoneNumber = c.String(nullable: false, maxLength: 128),
                        RoomId = c.Long(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => new { t.PhoneNumber, t.RoomId });
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                        RoomId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AnswerByPhones", "QLogData_id", "dbo.ActivityLogs");
            DropForeignKey("dbo.AnswerByPhones", "QuestionData_id", "dbo.Activities");
            DropForeignKey("dbo.Activities", "ExperimentData_id", "dbo.Activities");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AnswerByPhones", new[] { "QLogData_id" });
            DropIndex("dbo.AnswerByPhones", new[] { "QuestionData_id" });
            DropIndex("dbo.Activities", new[] { "ExperimentData_id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Rooms");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.GroupDatas");
            DropTable("dbo.ActivityLogs");
            DropTable("dbo.AnswerByPhones");
            DropTable("dbo.Activities");
            DropTable("dbo.ActiveExperiments");
        }
    }
}
