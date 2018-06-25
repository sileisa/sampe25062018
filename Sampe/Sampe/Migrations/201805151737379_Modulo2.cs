namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modulo2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        NomeCliente = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.ControleDeQualidades",
                c => new
                    {
                        ControleDeQualidadeId = c.Int(nullable: false, identity: true),
                        Ciclo = c.Single(nullable: false),
                        Hora = c.String(unicode: false),
                        PesoDaPeca = c.Single(nullable: false),
                        Peso = c.Boolean(nullable: false),
                        Cor = c.Boolean(nullable: false),
                        Dimensao = c.Boolean(nullable: false),
                        Assinatura = c.String(unicode: false),
                        Liberado = c.Boolean(nullable: false),
                        OrdemProducaoPecaId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ControleDeQualidadeId)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.OrdemProducaoPecaId)
                .Index(t => t.OrdemProducaoPecaId);
            
            CreateTable(
                "dbo.OrdemProducaoPecas",
                c => new
                    {
                        CodigoIdentificador = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ExpectativaId = c.Int(nullable: false),
                        OPnoMes = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false, precision: 0),
                        MateriaPrima = c.String(nullable: false, unicode: false),
                        MPLote = c.String(unicode: false),
                        MPConsumo = c.Double(nullable: false),
                        ProdIncio = c.String(unicode: false),
                        ProdFim = c.String(unicode: false),
                        Maquina = c.String(unicode: false),
                        Produto = c.String(unicode: false),
                        ProdutoCor = c.String(unicode: false),
                        MasterLote = c.String(unicode: false),
                        Fornecedor = c.String(unicode: false),
                        TempAgua = c.Double(nullable: false),
                        NivelOleo = c.Double(nullable: false),
                        Galho = c.Double(nullable: false),
                        OffSpec = c.Double(nullable: false),
                        RefugoKg = c.Double(nullable: false),
                        UnidadesProduzidas = c.Int(nullable: false),
                        ContadorInicial = c.Double(nullable: false),
                        ContadorFinal = c.Double(nullable: false),
                        OrdemProducaoKit_CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.CodigoIdentificador)
                .ForeignKey("dbo.Expectativas", t => t.ExpectativaId, cascadeDelete: true)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.OrdemProducaoKit_CodigoIdentificadorKit)
                .Index(t => t.ExpectativaId)
                .Index(t => t.OrdemProducaoKit_CodigoIdentificadorKit);
            
            CreateTable(
                "dbo.Expectativas",
                c => new
                    {
                        ExpectativaId = c.Int(nullable: false, identity: true),
                        Produto = c.String(unicode: false),
                        CavidadeMolde = c.Int(nullable: false),
                        PesoPecaAproximado = c.Double(nullable: false),
                        PesoPecaCompleta = c.Double(nullable: false),
                        Ciclo = c.Single(nullable: false),
                        ProducaoEsperada = c.Int(nullable: false),
                        ProdInicio = c.String(unicode: false),
                        ProdFim = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ExpectativaId);
            
            CreateTable(
                "dbo.Paradas",
                c => new
                    {
                        ParadaId = c.Int(nullable: false, identity: true),
                        HoraParada = c.String(unicode: false),
                        HoraRetorno = c.String(unicode: false),
                        Motivo = c.String(unicode: false),
                        Observacoes = c.String(unicode: false),
                        OrdemProducaoPecaId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ParadaId)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.OrdemProducaoPecaId)
                .Index(t => t.OrdemProducaoPecaId);
            
            CreateTable(
                "dbo.Especificacaos",
                c => new
                    {
                        EspecificacaoId = c.Int(nullable: false, identity: true),
                        TipoKit = c.String(unicode: false),
                        CorKit = c.String(unicode: false),
                        Parafuso = c.Boolean(nullable: false),
                        QuantProduzido = c.Int(nullable: false),
                        CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoKit_CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.EspecificacaoId)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.OrdemProducaoKit_CodigoIdentificadorKit)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.CodigoIdentificadorKit)
                .Index(t => t.CodigoIdentificadorKit)
                .Index(t => t.OrdemProducaoKit_CodigoIdentificadorKit);
            
            CreateTable(
                "dbo.OrdemProducaoKits",
                c => new
                    {
                        CodigoIdentificadorKit = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Data = c.DateTime(nullable: false, precision: 0),
                        ProdIncio = c.String(unicode: false),
                        ProdFim = c.String(unicode: false),
                        TotalProduzido = c.Int(nullable: false),
                        NivelamentoBalanca = c.Boolean(nullable: false),
                        Obs = c.String(unicode: false),
                        OPnoMes = c.Int(nullable: false),
                        Operdor = c.String(unicode: false),
                        ClienteId = c.Int(nullable: false),
                        Especificacao_EspecificacaoId = c.Int(),
                        OrdemProducaoPeca_CodigoIdentificador = c.String(maxLength: 128, storeType: "nvarchar"),
                        ParadaKit_ParadaId = c.Int(),
                    })
                .PrimaryKey(t => t.CodigoIdentificadorKit)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Especificacaos", t => t.Especificacao_EspecificacaoId)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.OrdemProducaoPeca_CodigoIdentificador)
                .ForeignKey("dbo.ParadaKits", t => t.ParadaKit_ParadaId)
                .Index(t => t.ClienteId)
                .Index(t => t.Especificacao_EspecificacaoId)
                .Index(t => t.OrdemProducaoPeca_CodigoIdentificador)
                .Index(t => t.ParadaKit_ParadaId);
            
            CreateTable(
                "dbo.KitPecas",
                c => new
                    {
                        KitPecaId = c.Int(nullable: false, identity: true),
                        CodigoIdentificador = c.String(maxLength: 128, storeType: "nvarchar"),
                        CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.KitPecaId)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.CodigoIdentificadorKit)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.CodigoIdentificador)
                .Index(t => t.CodigoIdentificador)
                .Index(t => t.CodigoIdentificadorKit);
            
            CreateTable(
                "dbo.ParadaKits",
                c => new
                    {
                        ParadaId = c.Int(nullable: false, identity: true),
                        HoraParada = c.String(unicode: false),
                        HoraRetorno = c.String(unicode: false),
                        Motivo = c.String(unicode: false),
                        Observacoes = c.String(unicode: false),
                        CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoKit_CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ParadaId)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.CodigoIdentificadorKit)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.OrdemProducaoKit_CodigoIdentificadorKit)
                .Index(t => t.CodigoIdentificadorKit)
                .Index(t => t.OrdemProducaoKit_CodigoIdentificadorKit);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Especificacaos", "CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.ParadaKits", "OrdemProducaoKit_CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoKits", "ParadaKit_ParadaId", "dbo.ParadaKits");
            DropForeignKey("dbo.ParadaKits", "CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoPecas", "OrdemProducaoKit_CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoKits", "OrdemProducaoPeca_CodigoIdentificador", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.KitPecas", "CodigoIdentificador", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.KitPecas", "CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.Especificacaos", "OrdemProducaoKit_CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoKits", "Especificacao_EspecificacaoId", "dbo.Especificacaos");
            DropForeignKey("dbo.OrdemProducaoKits", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.ControleDeQualidades", "OrdemProducaoPecaId", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.Paradas", "OrdemProducaoPecaId", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.OrdemProducaoPecas", "ExpectativaId", "dbo.Expectativas");
            DropIndex("dbo.ParadaKits", new[] { "OrdemProducaoKit_CodigoIdentificadorKit" });
            DropIndex("dbo.ParadaKits", new[] { "CodigoIdentificadorKit" });
            DropIndex("dbo.KitPecas", new[] { "CodigoIdentificadorKit" });
            DropIndex("dbo.KitPecas", new[] { "CodigoIdentificador" });
            DropIndex("dbo.OrdemProducaoKits", new[] { "ParadaKit_ParadaId" });
            DropIndex("dbo.OrdemProducaoKits", new[] { "OrdemProducaoPeca_CodigoIdentificador" });
            DropIndex("dbo.OrdemProducaoKits", new[] { "Especificacao_EspecificacaoId" });
            DropIndex("dbo.OrdemProducaoKits", new[] { "ClienteId" });
            DropIndex("dbo.Especificacaos", new[] { "OrdemProducaoKit_CodigoIdentificadorKit" });
            DropIndex("dbo.Especificacaos", new[] { "CodigoIdentificadorKit" });
            DropIndex("dbo.Paradas", new[] { "OrdemProducaoPecaId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "OrdemProducaoKit_CodigoIdentificadorKit" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "ExpectativaId" });
            DropIndex("dbo.ControleDeQualidades", new[] { "OrdemProducaoPecaId" });
            DropTable("dbo.ParadaKits");
            DropTable("dbo.KitPecas");
            DropTable("dbo.OrdemProducaoKits");
            DropTable("dbo.Especificacaos");
            DropTable("dbo.Paradas");
            DropTable("dbo.Expectativas");
            DropTable("dbo.OrdemProducaoPecas");
            DropTable("dbo.ControleDeQualidades");
            DropTable("dbo.Clientes");
        }
    }
}
