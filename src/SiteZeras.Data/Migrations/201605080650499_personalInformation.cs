namespace SiteZeras.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class personalInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonalInformations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(nullable: false, maxLength: 128),
                        lastName = c.String(nullable: false, maxLength: 128),
                        AccountId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalInformations", "AccountId", "dbo.Accounts");
            DropIndex("dbo.PersonalInformations", new[] { "AccountId" });
            DropTable("dbo.PersonalInformations");
        }
    }
}
