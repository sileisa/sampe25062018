namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sampe110520181553 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logins", "User", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Logins", "Senha", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logins", "Senha", c => c.String(unicode: false));
            AlterColumn("dbo.Logins", "User", c => c.String(unicode: false));
        }
    }
}
