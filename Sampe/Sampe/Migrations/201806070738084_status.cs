namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdemProducaoRefugoes", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.EspecificacaoRefugoes", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EspecificacaoRefugoes", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.OrdemProducaoRefugoes", "Status");
        }
    }
}
