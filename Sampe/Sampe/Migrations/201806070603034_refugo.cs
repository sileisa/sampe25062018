namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refugo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EspecificacaoRefugoes",
                c => new
                    {
                        EspecificacaoRefugoId = c.Int(nullable: false, identity: true),
                        Material = c.String(unicode: false),
                        Cor = c.String(unicode: false),
                        Peso = c.Double(nullable: false),
                        Limpeza = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        OrdemProducaoRefugoId = c.Int(nullable: false),
                        OrdemProducaoRefugo_OrdemProducaoRefugoId = c.Int(),
                    })
                .PrimaryKey(t => t.EspecificacaoRefugoId)
                .ForeignKey("dbo.OrdemProducaoRefugoes", t => t.OrdemProducaoRefugo_OrdemProducaoRefugoId)
                .ForeignKey("dbo.OrdemProducaoRefugoes", t => t.OrdemProducaoRefugoId, cascadeDelete: true)
                .Index(t => t.OrdemProducaoRefugoId)
                .Index(t => t.OrdemProducaoRefugo_OrdemProducaoRefugoId);
            
            CreateTable(
                "dbo.OrdemProducaoRefugoes",
                c => new
                    {
                        OrdemProducaoRefugoId = c.Int(nullable: false, identity: true),
                        Produto = c.String(unicode: false),
                        Data = c.DateTime(nullable: false, precision: 0),
                        UsuarioId = c.Int(nullable: false),
                        ProdIncio = c.String(unicode: false),
                        ProdFim = c.String(unicode: false),
                        Obs = c.String(unicode: false),
                        EspecificacaoRefugo_EspecificacaoRefugoId = c.Int(),
                        ParadaRefugo_ParadaRefugoId = c.Int(),
                    })
                .PrimaryKey(t => t.OrdemProducaoRefugoId)
                .ForeignKey("dbo.EspecificacaoRefugoes", t => t.EspecificacaoRefugo_EspecificacaoRefugoId)
                .ForeignKey("dbo.ParadaRefugoes", t => t.ParadaRefugo_ParadaRefugoId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.EspecificacaoRefugo_EspecificacaoRefugoId)
                .Index(t => t.ParadaRefugo_ParadaRefugoId);
            
            CreateTable(
                "dbo.ParadaRefugoes",
                c => new
                    {
                        ParadaRefugoId = c.Int(nullable: false, identity: true),
                        HoraParada = c.String(unicode: false),
                        HoraRetorno = c.String(unicode: false),
                        Motivo = c.String(unicode: false),
                        Observacoes = c.String(unicode: false),
                        OrdemProducaoRefugoId = c.Int(nullable: false),
                        OrdemProducaoRefugo_OrdemProducaoRefugoId = c.Int(),
                    })
                .PrimaryKey(t => t.ParadaRefugoId)
                .ForeignKey("dbo.OrdemProducaoRefugoes", t => t.OrdemProducaoRefugoId, cascadeDelete: true)
                .ForeignKey("dbo.OrdemProducaoRefugoes", t => t.OrdemProducaoRefugo_OrdemProducaoRefugoId)
                .Index(t => t.OrdemProducaoRefugoId)
                .Index(t => t.OrdemProducaoRefugo_OrdemProducaoRefugoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EspecificacaoRefugoes", "OrdemProducaoRefugoId", "dbo.OrdemProducaoRefugoes");
            DropForeignKey("dbo.OrdemProducaoRefugoes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.ParadaRefugoes", "OrdemProducaoRefugo_OrdemProducaoRefugoId", "dbo.OrdemProducaoRefugoes");
            DropForeignKey("dbo.OrdemProducaoRefugoes", "ParadaRefugo_ParadaRefugoId", "dbo.ParadaRefugoes");
            DropForeignKey("dbo.ParadaRefugoes", "OrdemProducaoRefugoId", "dbo.OrdemProducaoRefugoes");
            DropForeignKey("dbo.EspecificacaoRefugoes", "OrdemProducaoRefugo_OrdemProducaoRefugoId", "dbo.OrdemProducaoRefugoes");
            DropForeignKey("dbo.OrdemProducaoRefugoes", "EspecificacaoRefugo_EspecificacaoRefugoId", "dbo.EspecificacaoRefugoes");
            DropIndex("dbo.ParadaRefugoes", new[] { "OrdemProducaoRefugo_OrdemProducaoRefugoId" });
            DropIndex("dbo.ParadaRefugoes", new[] { "OrdemProducaoRefugoId" });
            DropIndex("dbo.OrdemProducaoRefugoes", new[] { "ParadaRefugo_ParadaRefugoId" });
            DropIndex("dbo.OrdemProducaoRefugoes", new[] { "EspecificacaoRefugo_EspecificacaoRefugoId" });
            DropIndex("dbo.OrdemProducaoRefugoes", new[] { "UsuarioId" });
            DropIndex("dbo.EspecificacaoRefugoes", new[] { "OrdemProducaoRefugo_OrdemProducaoRefugoId" });
            DropIndex("dbo.EspecificacaoRefugoes", new[] { "OrdemProducaoRefugoId" });
            DropTable("dbo.ParadaRefugoes");
            DropTable("dbo.OrdemProducaoRefugoes");
            DropTable("dbo.EspecificacaoRefugoes");
        }
    }
}
