namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sampe230620181802 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrdemProducaoKits", "Obs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrdemProducaoKits", "Obs", c => c.String(unicode: false));
        }
    }
}
