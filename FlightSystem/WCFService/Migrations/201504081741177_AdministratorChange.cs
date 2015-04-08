namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdministratorChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administrators", "Username", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Administrators", "PasswordHash", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Administrators", "PasswordHash", c => c.String());
            AlterColumn("dbo.Administrators", "Username", c => c.String());
        }
    }
}
