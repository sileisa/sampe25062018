namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sampe2306201818021 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrdemProducaoRefugoes", "Obs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrdemProducaoRefugoes", "Obs", c => c.String(unicode: false));
        }
    }
}
