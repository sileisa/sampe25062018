namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _bool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrdemProducaoPecas", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrdemProducaoPecas", "Status", c => c.Boolean(nullable: false));
        }
    }
}
