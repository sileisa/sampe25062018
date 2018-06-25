namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bool2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrdemProducaoPecas", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrdemProducaoPecas", "Status", c => c.Boolean());
        }
    }
}
