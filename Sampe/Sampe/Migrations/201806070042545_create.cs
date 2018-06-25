namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AtividadeOS",
                c => new
                    {
                        AtividadeOSId = c.Int(nullable: false, identity: true),
                        NomeAtvOs = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AtividadeOSId);
            
            CreateTable(
                "dbo.FormularioOrdemServicoes",
                c => new
                    {
                        FormularioOrdemServicoId = c.Int(nullable: false, identity: true),
                        TipoManutencao = c.String(unicode: false),
                        HoraInicio = c.String(unicode: false),
                        HoraFinal = c.String(unicode: false),
                        Dt = c.DateTime(nullable: false, precision: 0),
                        Intervalo = c.Boolean(nullable: false),
                        IntervaloInicio = c.String(unicode: false),
                        IntervaloFim = c.String(unicode: false),
                        ObsIntervalo = c.String(unicode: false),
                        MaterialUsado = c.String(unicode: false),
                        QuantUsado = c.Int(),
                        MaterialSobrante = c.String(unicode: false),
                        QuantSobrante = c.Int(),
                        Status = c.Boolean(nullable: false),
                        Supervisor = c.String(unicode: false),
                        MaquinaId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormularioOrdemServicoId)
                .ForeignKey("dbo.Maquinas", t => t.MaquinaId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.MaquinaId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.FormularioOSAtividades",
                c => new
                    {
                        FormularioOSAtividadeId = c.Int(nullable: false, identity: true),
                        FormularioOrdemServicoId = c.Int(nullable: false),
                        AtividadeOSId = c.Int(nullable: false),
                        StatusOS = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FormularioOSAtividadeId)
                .ForeignKey("dbo.AtividadeOS", t => t.AtividadeOSId, cascadeDelete: true)
                .ForeignKey("dbo.FormularioOrdemServicoes", t => t.FormularioOrdemServicoId, cascadeDelete: true)
                .Index(t => t.FormularioOrdemServicoId)
                .Index(t => t.AtividadeOSId);
            
            CreateTable(
                "dbo.Maquinas",
                c => new
                    {
                        MaquinaId = c.Int(nullable: false, identity: true),
                        NomeMaquina = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.MaquinaId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        NomeUsuario = c.String(nullable: false, unicode: false),
                        SobrenomeUsuario = c.String(nullable: false, unicode: false),
                        Login = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        Hierarquia = c.String(unicode: false),
                        CargoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Cargoes", t => t.CargoId, cascadeDelete: true)
                .Index(t => t.CargoId);
            
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        CargoId = c.Int(nullable: false, identity: true),
                        NomeCargo = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.CargoId);
            
            CreateTable(
                "dbo.AtividadeTMs",
                c => new
                    {
                        AtividadeTMId = c.Int(nullable: false, identity: true),
                        NomeAtvTm = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AtividadeTMId);
            
            CreateTable(
                "dbo.FormularioTrocaMoldes",
                c => new
                    {
                        FormularioTrocaMoldeId = c.Int(nullable: false, identity: true),
                        DtRetirada = c.DateTime(nullable: false, precision: 0),
                        DtColocar = c.DateTime(nullable: false, precision: 0),
                        ColocarInicio = c.String(unicode: false),
                        ColocarFim = c.String(unicode: false),
                        RetirarInicio = c.String(unicode: false),
                        RetirarFim = c.String(unicode: false),
                        Status = c.Boolean(nullable: false),
                        Supervisor = c.String(unicode: false),
                        MaquinaId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        Molde_MoldeId = c.Int(),
                    })
                .PrimaryKey(t => t.FormularioTrocaMoldeId)
                .ForeignKey("dbo.Maquinas", t => t.MaquinaId, cascadeDelete: true)
                .ForeignKey("dbo.Moldes", t => t.Molde_MoldeId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.MaquinaId)
                .Index(t => t.UsuarioId)
                .Index(t => t.Molde_MoldeId);
            
            CreateTable(
                "dbo.FormularioMoldes",
                c => new
                    {
                        FormularioMoldeId = c.Int(nullable: false, identity: true),
                        FormularioTrocaMoldeId = c.Int(nullable: false),
                        MoldeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormularioMoldeId)
                .ForeignKey("dbo.FormularioTrocaMoldes", t => t.FormularioTrocaMoldeId, cascadeDelete: true)
                .ForeignKey("dbo.Moldes", t => t.MoldeId, cascadeDelete: true)
                .Index(t => t.FormularioTrocaMoldeId)
                .Index(t => t.MoldeId);
            
            CreateTable(
                "dbo.Moldes",
                c => new
                    {
                        MoldeId = c.Int(nullable: false, identity: true),
                        NomeMolde = c.String(nullable: false, unicode: false),
                        Cavidade = c.Int(),
                        FormularioTrocaMolde_FormularioTrocaMoldeId = c.Int(),
                    })
                .PrimaryKey(t => t.MoldeId)
                .ForeignKey("dbo.FormularioTrocaMoldes", t => t.FormularioTrocaMolde_FormularioTrocaMoldeId)
                .Index(t => t.FormularioTrocaMolde_FormularioTrocaMoldeId);
            
            CreateTable(
                "dbo.FormularioTMAtividades",
                c => new
                    {
                        FormularioTMAtividadeId = c.Int(nullable: false, identity: true),
                        FormularioTrocaMoldeId = c.Int(nullable: false),
                        AtividadeTMId = c.Int(nullable: false),
                        StatusTM = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FormularioTMAtividadeId)
                .ForeignKey("dbo.AtividadeTMs", t => t.AtividadeTMId, cascadeDelete: true)
                .ForeignKey("dbo.FormularioTrocaMoldes", t => t.FormularioTrocaMoldeId, cascadeDelete: true)
                .Index(t => t.FormularioTrocaMoldeId)
                .Index(t => t.AtividadeTMId);
            
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
                        Assinatura = c.Int(nullable: false),
                        Liberado = c.Boolean(nullable: false),
                        OrdemProducaoPecaId = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoPeca_CodigoIdentificador = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ControleDeQualidadeId)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.OrdemProducaoPeca_CodigoIdentificador)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.OrdemProducaoPecaId)
                .ForeignKey("dbo.Usuarios", t => t.Assinatura, cascadeDelete: true)
                .Index(t => t.Assinatura)
                .Index(t => t.OrdemProducaoPecaId)
                .Index(t => t.OrdemProducaoPeca_CodigoIdentificador);
            
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
                        MaquinaId = c.Int(nullable: false),
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
                        Status = c.Boolean(nullable: false),
                        ControleDeQualidade_ControleDeQualidadeId = c.Int(),
                        Parada_ParadaId = c.Int(),
                        OrdemProducaoKit_CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.CodigoIdentificador)
                .ForeignKey("dbo.ControleDeQualidades", t => t.ControleDeQualidade_ControleDeQualidadeId)
                .ForeignKey("dbo.Expectativas", t => t.ExpectativaId, cascadeDelete: true)
                .ForeignKey("dbo.Maquinas", t => t.MaquinaId, cascadeDelete: true)
                .ForeignKey("dbo.Paradas", t => t.Parada_ParadaId)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.OrdemProducaoKit_CodigoIdentificadorKit)
                .Index(t => t.ExpectativaId)
                .Index(t => t.MaquinaId)
                .Index(t => t.ControleDeQualidade_ControleDeQualidadeId)
                .Index(t => t.Parada_ParadaId)
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
                        OrdemProducaoPeca_CodigoIdentificador = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ParadaId)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.OrdemProducaoPecaId)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.OrdemProducaoPeca_CodigoIdentificador)
                .Index(t => t.OrdemProducaoPecaId)
                .Index(t => t.OrdemProducaoPeca_CodigoIdentificador);
            
            CreateTable(
                "dbo.Especificacaos",
                c => new
                    {
                        EspecificacaoId = c.Int(nullable: false, identity: true),
                        TipoKit = c.String(unicode: false),
                        CorKit = c.String(unicode: false),
                        Parafuso = c.Boolean(nullable: false),
                        QuantProduzido = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoKit_CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.EspecificacaoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.OrdemProducaoKit_CodigoIdentificadorKit)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.CodigoIdentificadorKit)
                .Index(t => t.ClienteId)
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
                        Status = c.Boolean(nullable: false),
                        Operdor = c.Int(nullable: false),
                        Especificacao_EspecificacaoId = c.Int(),
                        OrdemProducaoPeca_CodigoIdentificador = c.String(maxLength: 128, storeType: "nvarchar"),
                        ParadaKit_ParadaId = c.Int(),
                    })
                .PrimaryKey(t => t.CodigoIdentificadorKit)
                .ForeignKey("dbo.Especificacaos", t => t.Especificacao_EspecificacaoId)
                .ForeignKey("dbo.OrdemProducaoPecas", t => t.OrdemProducaoPeca_CodigoIdentificador)
                .ForeignKey("dbo.ParadaKits", t => t.ParadaKit_ParadaId)
                .ForeignKey("dbo.Usuarios", t => t.Operdor, cascadeDelete: true)
                .Index(t => t.Operdor)
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
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        User = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.LoginId);
            
            CreateTable(
                "dbo.FormularioOrdemServicoAtividadeOS",
                c => new
                    {
                        FormularioOrdemServico_FormularioOrdemServicoId = c.Int(nullable: false),
                        AtividadeOS_AtividadeOSId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FormularioOrdemServico_FormularioOrdemServicoId, t.AtividadeOS_AtividadeOSId })
                .ForeignKey("dbo.FormularioOrdemServicoes", t => t.FormularioOrdemServico_FormularioOrdemServicoId, cascadeDelete: true)
                .ForeignKey("dbo.AtividadeOS", t => t.AtividadeOS_AtividadeOSId, cascadeDelete: true)
                .Index(t => t.FormularioOrdemServico_FormularioOrdemServicoId)
                .Index(t => t.AtividadeOS_AtividadeOSId);
            
            CreateTable(
                "dbo.FormularioTrocaMoldeAtividadeTMs",
                c => new
                    {
                        FormularioTrocaMolde_FormularioTrocaMoldeId = c.Int(nullable: false),
                        AtividadeTM_AtividadeTMId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FormularioTrocaMolde_FormularioTrocaMoldeId, t.AtividadeTM_AtividadeTMId })
                .ForeignKey("dbo.FormularioTrocaMoldes", t => t.FormularioTrocaMolde_FormularioTrocaMoldeId, cascadeDelete: true)
                .ForeignKey("dbo.AtividadeTMs", t => t.AtividadeTM_AtividadeTMId, cascadeDelete: true)
                .Index(t => t.FormularioTrocaMolde_FormularioTrocaMoldeId)
                .Index(t => t.AtividadeTM_AtividadeTMId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Especificacaos", "CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoKits", "Operdor", "dbo.Usuarios");
            DropForeignKey("dbo.ParadaKits", "OrdemProducaoKit_CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoKits", "ParadaKit_ParadaId", "dbo.ParadaKits");
            DropForeignKey("dbo.ParadaKits", "CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoPecas", "OrdemProducaoKit_CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoKits", "OrdemProducaoPeca_CodigoIdentificador", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.KitPecas", "CodigoIdentificador", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.KitPecas", "CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.Especificacaos", "OrdemProducaoKit_CodigoIdentificadorKit", "dbo.OrdemProducaoKits");
            DropForeignKey("dbo.OrdemProducaoKits", "Especificacao_EspecificacaoId", "dbo.Especificacaos");
            DropForeignKey("dbo.Especificacaos", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.ControleDeQualidades", "Assinatura", "dbo.Usuarios");
            DropForeignKey("dbo.ControleDeQualidades", "OrdemProducaoPecaId", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.Paradas", "OrdemProducaoPeca_CodigoIdentificador", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.OrdemProducaoPecas", "Parada_ParadaId", "dbo.Paradas");
            DropForeignKey("dbo.Paradas", "OrdemProducaoPecaId", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.OrdemProducaoPecas", "MaquinaId", "dbo.Maquinas");
            DropForeignKey("dbo.OrdemProducaoPecas", "ExpectativaId", "dbo.Expectativas");
            DropForeignKey("dbo.ControleDeQualidades", "OrdemProducaoPeca_CodigoIdentificador", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.OrdemProducaoPecas", "ControleDeQualidade_ControleDeQualidadeId", "dbo.ControleDeQualidades");
            DropForeignKey("dbo.FormularioTrocaMoldes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Moldes", "FormularioTrocaMolde_FormularioTrocaMoldeId", "dbo.FormularioTrocaMoldes");
            DropForeignKey("dbo.FormularioTrocaMoldes", "Molde_MoldeId", "dbo.Moldes");
            DropForeignKey("dbo.FormularioTrocaMoldes", "MaquinaId", "dbo.Maquinas");
            DropForeignKey("dbo.FormularioTMAtividades", "FormularioTrocaMoldeId", "dbo.FormularioTrocaMoldes");
            DropForeignKey("dbo.FormularioTMAtividades", "AtividadeTMId", "dbo.AtividadeTMs");
            DropForeignKey("dbo.FormularioMoldes", "MoldeId", "dbo.Moldes");
            DropForeignKey("dbo.FormularioMoldes", "FormularioTrocaMoldeId", "dbo.FormularioTrocaMoldes");
            DropForeignKey("dbo.FormularioTrocaMoldeAtividadeTMs", "AtividadeTM_AtividadeTMId", "dbo.AtividadeTMs");
            DropForeignKey("dbo.FormularioTrocaMoldeAtividadeTMs", "FormularioTrocaMolde_FormularioTrocaMoldeId", "dbo.FormularioTrocaMoldes");
            DropForeignKey("dbo.FormularioOrdemServicoes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "CargoId", "dbo.Cargoes");
            DropForeignKey("dbo.FormularioOrdemServicoes", "MaquinaId", "dbo.Maquinas");
            DropForeignKey("dbo.FormularioOSAtividades", "FormularioOrdemServicoId", "dbo.FormularioOrdemServicoes");
            DropForeignKey("dbo.FormularioOSAtividades", "AtividadeOSId", "dbo.AtividadeOS");
            DropForeignKey("dbo.FormularioOrdemServicoAtividadeOS", "AtividadeOS_AtividadeOSId", "dbo.AtividadeOS");
            DropForeignKey("dbo.FormularioOrdemServicoAtividadeOS", "FormularioOrdemServico_FormularioOrdemServicoId", "dbo.FormularioOrdemServicoes");
            DropIndex("dbo.FormularioTrocaMoldeAtividadeTMs", new[] { "AtividadeTM_AtividadeTMId" });
            DropIndex("dbo.FormularioTrocaMoldeAtividadeTMs", new[] { "FormularioTrocaMolde_FormularioTrocaMoldeId" });
            DropIndex("dbo.FormularioOrdemServicoAtividadeOS", new[] { "AtividadeOS_AtividadeOSId" });
            DropIndex("dbo.FormularioOrdemServicoAtividadeOS", new[] { "FormularioOrdemServico_FormularioOrdemServicoId" });
            DropIndex("dbo.ParadaKits", new[] { "OrdemProducaoKit_CodigoIdentificadorKit" });
            DropIndex("dbo.ParadaKits", new[] { "CodigoIdentificadorKit" });
            DropIndex("dbo.KitPecas", new[] { "CodigoIdentificadorKit" });
            DropIndex("dbo.KitPecas", new[] { "CodigoIdentificador" });
            DropIndex("dbo.OrdemProducaoKits", new[] { "ParadaKit_ParadaId" });
            DropIndex("dbo.OrdemProducaoKits", new[] { "OrdemProducaoPeca_CodigoIdentificador" });
            DropIndex("dbo.OrdemProducaoKits", new[] { "Especificacao_EspecificacaoId" });
            DropIndex("dbo.OrdemProducaoKits", new[] { "Operdor" });
            DropIndex("dbo.Especificacaos", new[] { "OrdemProducaoKit_CodigoIdentificadorKit" });
            DropIndex("dbo.Especificacaos", new[] { "CodigoIdentificadorKit" });
            DropIndex("dbo.Especificacaos", new[] { "ClienteId" });
            DropIndex("dbo.Paradas", new[] { "OrdemProducaoPeca_CodigoIdentificador" });
            DropIndex("dbo.Paradas", new[] { "OrdemProducaoPecaId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "OrdemProducaoKit_CodigoIdentificadorKit" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "Parada_ParadaId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "ControleDeQualidade_ControleDeQualidadeId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "MaquinaId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "ExpectativaId" });
            DropIndex("dbo.ControleDeQualidades", new[] { "OrdemProducaoPeca_CodigoIdentificador" });
            DropIndex("dbo.ControleDeQualidades", new[] { "OrdemProducaoPecaId" });
            DropIndex("dbo.ControleDeQualidades", new[] { "Assinatura" });
            DropIndex("dbo.FormularioTMAtividades", new[] { "AtividadeTMId" });
            DropIndex("dbo.FormularioTMAtividades", new[] { "FormularioTrocaMoldeId" });
            DropIndex("dbo.Moldes", new[] { "FormularioTrocaMolde_FormularioTrocaMoldeId" });
            DropIndex("dbo.FormularioMoldes", new[] { "MoldeId" });
            DropIndex("dbo.FormularioMoldes", new[] { "FormularioTrocaMoldeId" });
            DropIndex("dbo.FormularioTrocaMoldes", new[] { "Molde_MoldeId" });
            DropIndex("dbo.FormularioTrocaMoldes", new[] { "UsuarioId" });
            DropIndex("dbo.FormularioTrocaMoldes", new[] { "MaquinaId" });
            DropIndex("dbo.Usuarios", new[] { "CargoId" });
            DropIndex("dbo.FormularioOSAtividades", new[] { "AtividadeOSId" });
            DropIndex("dbo.FormularioOSAtividades", new[] { "FormularioOrdemServicoId" });
            DropIndex("dbo.FormularioOrdemServicoes", new[] { "UsuarioId" });
            DropIndex("dbo.FormularioOrdemServicoes", new[] { "MaquinaId" });
            DropTable("dbo.FormularioTrocaMoldeAtividadeTMs");
            DropTable("dbo.FormularioOrdemServicoAtividadeOS");
            DropTable("dbo.Logins");
            DropTable("dbo.ParadaKits");
            DropTable("dbo.KitPecas");
            DropTable("dbo.OrdemProducaoKits");
            DropTable("dbo.Especificacaos");
            DropTable("dbo.Paradas");
            DropTable("dbo.Expectativas");
            DropTable("dbo.OrdemProducaoPecas");
            DropTable("dbo.ControleDeQualidades");
            DropTable("dbo.Clientes");
            DropTable("dbo.FormularioTMAtividades");
            DropTable("dbo.Moldes");
            DropTable("dbo.FormularioMoldes");
            DropTable("dbo.FormularioTrocaMoldes");
            DropTable("dbo.AtividadeTMs");
            DropTable("dbo.Cargoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Maquinas");
            DropTable("dbo.FormularioOSAtividades");
            DropTable("dbo.FormularioOrdemServicoes");
            DropTable("dbo.AtividadeOS");
        }
    }
}
