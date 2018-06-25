namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sampe220620181410 : DbMigration
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
                        Supervisor = c.String(nullable: false, unicode: false),
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
                        NomeCliente = c.String(nullable: false, unicode: false),
                        Cnpj = c.String(unicode: false),
                        Cep = c.Int(nullable: false),
                        Uf = c.String(unicode: false),
                        Cidade = c.String(unicode: false),
                        Rua = c.String(unicode: false),
                        Bairro = c.String(unicode: false),
                        Numero = c.Int(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.ControleDeQualidadeCopoes",
                c => new
                    {
                        ControleDeQualidadeCopoId = c.Int(nullable: false, identity: true),
                        Ciclo = c.Double(nullable: false),
                        Hora = c.String(unicode: false),
                        PesoDaPeca = c.Double(nullable: false),
                        PesoDaPeca2 = c.Double(nullable: false),
                        Peso = c.Boolean(nullable: false),
                        Cor = c.Boolean(nullable: false),
                        Dimensao = c.Boolean(nullable: false),
                        Assinatura = c.String(unicode: false),
                        Liberado = c.Boolean(nullable: false),
                        OrdemProducaoCopoId = c.String(maxLength: 128, storeType: "nvarchar"),
                        UsuarioId = c.Int(nullable: false),
                        OrdemProducaoCopo_CodigoIdentificador = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ControleDeQualidadeCopoId)
                .ForeignKey("dbo.OrdemProducaoCopoes", t => t.OrdemProducaoCopo_CodigoIdentificador)
                .ForeignKey("dbo.OrdemProducaoCopoes", t => t.OrdemProducaoCopoId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.OrdemProducaoCopoId)
                .Index(t => t.UsuarioId)
                .Index(t => t.OrdemProducaoCopo_CodigoIdentificador);
            
            CreateTable(
                "dbo.OrdemProducaoCopoes",
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
                        TempAgua = c.Double(nullable: false),
                        NivelOleo = c.Double(nullable: false),
                        RefugoKg = c.Double(nullable: false),
                        TotalProduzidos = c.Int(nullable: false),
                        ContadorInicial = c.Double(nullable: false),
                        ContadorFinal = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        ControleDeQualidadeCopo_ControleDeQualidadeCopoId = c.Int(),
                        EspecificacaoCopo_EspecificacaoCopoId = c.Int(),
                        ParadaCopo_ParadaId = c.Int(),
                    })
                .PrimaryKey(t => t.CodigoIdentificador)
                .ForeignKey("dbo.ControleDeQualidadeCopoes", t => t.ControleDeQualidadeCopo_ControleDeQualidadeCopoId)
                .ForeignKey("dbo.EspecificacaoCopoes", t => t.EspecificacaoCopo_EspecificacaoCopoId)
                .ForeignKey("dbo.Expectativas", t => t.ExpectativaId, cascadeDelete: true)
                .ForeignKey("dbo.Maquinas", t => t.MaquinaId, cascadeDelete: true)
                .ForeignKey("dbo.ParadaCopoes", t => t.ParadaCopo_ParadaId)
                .Index(t => t.ExpectativaId)
                .Index(t => t.MaquinaId)
                .Index(t => t.ControleDeQualidadeCopo_ControleDeQualidadeCopoId)
                .Index(t => t.EspecificacaoCopo_EspecificacaoCopoId)
                .Index(t => t.ParadaCopo_ParadaId);
            
            CreateTable(
                "dbo.EspecificacaoCopoes",
                c => new
                    {
                        EspecificacaoCopoId = c.Int(nullable: false, identity: true),
                        CorId = c.Int(nullable: false),
                        UniProd = c.Int(nullable: false),
                        LoteMaster = c.String(unicode: false),
                        OrdemProducaoCopoId = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoCopo_CodigoIdentificador = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.EspecificacaoCopoId)
                .ForeignKey("dbo.Cors", t => t.CorId, cascadeDelete: true)
                .ForeignKey("dbo.OrdemProducaoCopoes", t => t.OrdemProducaoCopoId)
                .ForeignKey("dbo.OrdemProducaoCopoes", t => t.OrdemProducaoCopo_CodigoIdentificador)
                .Index(t => t.CorId)
                .Index(t => t.OrdemProducaoCopoId)
                .Index(t => t.OrdemProducaoCopo_CodigoIdentificador);
            
            CreateTable(
                "dbo.Cors",
                c => new
                    {
                        CorId = c.Int(nullable: false, identity: true),
                        NomeCor = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CorId);
            
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
                "dbo.ParadaCopoes",
                c => new
                    {
                        ParadaId = c.Int(nullable: false, identity: true),
                        HoraParada = c.String(unicode: false),
                        HoraRetorno = c.String(unicode: false),
                        Motivo = c.String(unicode: false),
                        Observacoes = c.String(unicode: false),
                        OrdemProducaoCopoId = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoCopo_CodigoIdentificador = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ParadaId)
                .ForeignKey("dbo.OrdemProducaoCopoes", t => t.OrdemProducaoCopoId)
                .ForeignKey("dbo.OrdemProducaoCopoes", t => t.OrdemProducaoCopo_CodigoIdentificador)
                .Index(t => t.OrdemProducaoCopoId)
                .Index(t => t.OrdemProducaoCopo_CodigoIdentificador);
            
            CreateTable(
                "dbo.ControleDeQualidades",
                c => new
                    {
                        ControleDeQualidadeId = c.Int(nullable: false, identity: true),
                        Ciclo = c.Double(nullable: false),
                        Hora = c.String(unicode: false),
                        PesoDaPeca = c.Double(nullable: false),
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
                        CorPecaId = c.Int(nullable: false),
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
                .ForeignKey("dbo.CorPecas", t => t.CorPecaId, cascadeDelete: true)
                .ForeignKey("dbo.Expectativas", t => t.ExpectativaId, cascadeDelete: true)
                .ForeignKey("dbo.Maquinas", t => t.MaquinaId, cascadeDelete: true)
                .ForeignKey("dbo.Paradas", t => t.Parada_ParadaId)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.OrdemProducaoKit_CodigoIdentificadorKit)
                .Index(t => t.ExpectativaId)
                .Index(t => t.MaquinaId)
                .Index(t => t.CorPecaId)
                .Index(t => t.ControleDeQualidade_ControleDeQualidadeId)
                .Index(t => t.Parada_ParadaId)
                .Index(t => t.OrdemProducaoKit_CodigoIdentificadorKit);
            
            CreateTable(
                "dbo.CorPecas",
                c => new
                    {
                        CorPecaId = c.Int(nullable: false, identity: true),
                        NomeCorPeca = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CorPecaId);
            
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
                "dbo.EspecificacaoRefugoes",
                c => new
                    {
                        EspecificacaoRefugoId = c.Int(nullable: false, identity: true),
                        Material = c.String(unicode: false),
                        CorPecaId = c.Int(nullable: false),
                        Peso = c.Double(nullable: false),
                        Limpeza = c.Boolean(nullable: false),
                        OrdemProducaoRefugoId = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoRefugo_OrdemProducaoRefugoId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.EspecificacaoRefugoId)
                .ForeignKey("dbo.CorPecas", t => t.CorPecaId, cascadeDelete: true)
                .ForeignKey("dbo.OrdemProducaoRefugoes", t => t.OrdemProducaoRefugo_OrdemProducaoRefugoId)
                .ForeignKey("dbo.OrdemProducaoRefugoes", t => t.OrdemProducaoRefugoId)
                .Index(t => t.CorPecaId)
                .Index(t => t.OrdemProducaoRefugoId)
                .Index(t => t.OrdemProducaoRefugo_OrdemProducaoRefugoId);
            
            CreateTable(
                "dbo.OrdemProducaoRefugoes",
                c => new
                    {
                        OrdemProducaoRefugoId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Produto = c.String(unicode: false),
                        Data = c.DateTime(nullable: false, precision: 0),
                        OPnoMes = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        ProdIncio = c.String(unicode: false),
                        ProdFim = c.String(unicode: false),
                        Obs = c.String(unicode: false),
                        Status = c.Boolean(nullable: false),
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
                        OrdemProducaoRefugoId = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoRefugo_OrdemProducaoRefugoId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ParadaRefugoId)
                .ForeignKey("dbo.OrdemProducaoRefugoes", t => t.OrdemProducaoRefugoId)
                .ForeignKey("dbo.OrdemProducaoRefugoes", t => t.OrdemProducaoRefugo_OrdemProducaoRefugoId)
                .Index(t => t.OrdemProducaoRefugoId)
                .Index(t => t.OrdemProducaoRefugo_OrdemProducaoRefugoId);
            
            CreateTable(
                "dbo.Especificacaos",
                c => new
                    {
                        EspecificacaoId = c.Int(nullable: false, identity: true),
                        TipoKit = c.String(unicode: false),
                        CorPecaId = c.Int(nullable: false),
                        Parafuso = c.Boolean(nullable: false),
                        QuantProduzido = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                        OrdemProducaoKit_CodigoIdentificadorKit = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.EspecificacaoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.CorPecas", t => t.CorPecaId, cascadeDelete: true)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.OrdemProducaoKit_CodigoIdentificadorKit)
                .ForeignKey("dbo.OrdemProducaoKits", t => t.CodigoIdentificadorKit)
                .Index(t => t.CorPecaId)
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
                "dbo.ExpedicaoCopoes",
                c => new
                    {
                        ExpedicaoId = c.Int(nullable: false, identity: true),
                        ValorTotal = c.Double(nullable: false),
                        Vencimento = c.DateTime(nullable: false, precision: 0),
                        ClienteId = c.Int(nullable: false),
                        MarcantiId = c.Int(nullable: false),
                        Venda_VendaId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpedicaoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Marcantis", t => t.MarcantiId, cascadeDelete: true)
                .ForeignKey("dbo.Vendas", t => t.Venda_VendaId)
                .Index(t => t.ClienteId)
                .Index(t => t.MarcantiId)
                .Index(t => t.Venda_VendaId);
            
            CreateTable(
                "dbo.Marcantis",
                c => new
                    {
                        MarcantiId = c.Int(nullable: false, identity: true),
                        NomeEmpresa = c.String(nullable: false, unicode: false),
                        Cnpj = c.String(unicode: false),
                        Cep = c.Int(nullable: false),
                        Uf = c.String(nullable: false, unicode: false),
                        Cidade = c.String(nullable: false, unicode: false),
                        Rua = c.String(nullable: false, unicode: false),
                        Bairro = c.String(nullable: false, unicode: false),
                        Complemento = c.String(unicode: false),
                        Numero = c.Int(nullable: false),
                        Telefone = c.String(unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.MarcantiId);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        VendaId = c.Int(nullable: false, identity: true),
                        ValorUnitario = c.Double(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Subtotal = c.Double(nullable: false),
                        ExpedicaoCopoId = c.Int(nullable: false),
                        EspecificacaoCopoId = c.Int(nullable: false),
                        ExpedicaoCopo_ExpedicaoId = c.Int(),
                    })
                .PrimaryKey(t => t.VendaId)
                .ForeignKey("dbo.EspecificacaoCopoes", t => t.EspecificacaoCopoId, cascadeDelete: true)
                .ForeignKey("dbo.ExpedicaoCopoes", t => t.ExpedicaoCopoId, cascadeDelete: true)
                .ForeignKey("dbo.ExpedicaoCopoes", t => t.ExpedicaoCopo_ExpedicaoId)
                .Index(t => t.ExpedicaoCopoId)
                .Index(t => t.EspecificacaoCopoId)
                .Index(t => t.ExpedicaoCopo_ExpedicaoId);
            
            CreateTable(
                "dbo.ExpedicaoKits",
                c => new
                    {
                        ExpedicaoKitId = c.Int(nullable: false, identity: true),
                        ValorTotal = c.Double(nullable: false),
                        Vencimento = c.DateTime(nullable: false, precision: 0),
                        ClienteId = c.Int(nullable: false),
                        MarcantiId = c.Int(nullable: false),
                        VendaKit_VendaKitId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpedicaoKitId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Marcantis", t => t.MarcantiId, cascadeDelete: true)
                .ForeignKey("dbo.VendaKits", t => t.VendaKit_VendaKitId)
                .Index(t => t.ClienteId)
                .Index(t => t.MarcantiId)
                .Index(t => t.VendaKit_VendaKitId);
            
            CreateTable(
                "dbo.VendaKits",
                c => new
                    {
                        VendaKitId = c.Int(nullable: false, identity: true),
                        ValorUnitario = c.Double(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Subtotal = c.Double(nullable: false),
                        ExpedicaoKitId = c.Int(nullable: false),
                        EspecificacaoId = c.Int(nullable: false),
                        ExpedicaoKit_ExpedicaoKitId = c.Int(),
                    })
                .PrimaryKey(t => t.VendaKitId)
                .ForeignKey("dbo.Especificacaos", t => t.EspecificacaoId, cascadeDelete: true)
                .ForeignKey("dbo.ExpedicaoKits", t => t.ExpedicaoKitId, cascadeDelete: true)
                .ForeignKey("dbo.ExpedicaoKits", t => t.ExpedicaoKit_ExpedicaoKitId)
                .Index(t => t.ExpedicaoKitId)
                .Index(t => t.EspecificacaoId)
                .Index(t => t.ExpedicaoKit_ExpedicaoKitId);
            
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
            DropForeignKey("dbo.VendaKits", "ExpedicaoKit_ExpedicaoKitId", "dbo.ExpedicaoKits");
            DropForeignKey("dbo.ExpedicaoKits", "VendaKit_VendaKitId", "dbo.VendaKits");
            DropForeignKey("dbo.VendaKits", "ExpedicaoKitId", "dbo.ExpedicaoKits");
            DropForeignKey("dbo.VendaKits", "EspecificacaoId", "dbo.Especificacaos");
            DropForeignKey("dbo.ExpedicaoKits", "MarcantiId", "dbo.Marcantis");
            DropForeignKey("dbo.ExpedicaoKits", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Vendas", "ExpedicaoCopo_ExpedicaoId", "dbo.ExpedicaoCopoes");
            DropForeignKey("dbo.ExpedicaoCopoes", "Venda_VendaId", "dbo.Vendas");
            DropForeignKey("dbo.Vendas", "ExpedicaoCopoId", "dbo.ExpedicaoCopoes");
            DropForeignKey("dbo.Vendas", "EspecificacaoCopoId", "dbo.EspecificacaoCopoes");
            DropForeignKey("dbo.ExpedicaoCopoes", "MarcantiId", "dbo.Marcantis");
            DropForeignKey("dbo.ExpedicaoCopoes", "ClienteId", "dbo.Clientes");
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
            DropForeignKey("dbo.Especificacaos", "CorPecaId", "dbo.CorPecas");
            DropForeignKey("dbo.Especificacaos", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.EspecificacaoRefugoes", "OrdemProducaoRefugoId", "dbo.OrdemProducaoRefugoes");
            DropForeignKey("dbo.OrdemProducaoRefugoes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.ParadaRefugoes", "OrdemProducaoRefugo_OrdemProducaoRefugoId", "dbo.OrdemProducaoRefugoes");
            DropForeignKey("dbo.OrdemProducaoRefugoes", "ParadaRefugo_ParadaRefugoId", "dbo.ParadaRefugoes");
            DropForeignKey("dbo.ParadaRefugoes", "OrdemProducaoRefugoId", "dbo.OrdemProducaoRefugoes");
            DropForeignKey("dbo.EspecificacaoRefugoes", "OrdemProducaoRefugo_OrdemProducaoRefugoId", "dbo.OrdemProducaoRefugoes");
            DropForeignKey("dbo.OrdemProducaoRefugoes", "EspecificacaoRefugo_EspecificacaoRefugoId", "dbo.EspecificacaoRefugoes");
            DropForeignKey("dbo.EspecificacaoRefugoes", "CorPecaId", "dbo.CorPecas");
            DropForeignKey("dbo.ControleDeQualidades", "Assinatura", "dbo.Usuarios");
            DropForeignKey("dbo.ControleDeQualidades", "OrdemProducaoPecaId", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.Paradas", "OrdemProducaoPeca_CodigoIdentificador", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.OrdemProducaoPecas", "Parada_ParadaId", "dbo.Paradas");
            DropForeignKey("dbo.Paradas", "OrdemProducaoPecaId", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.OrdemProducaoPecas", "MaquinaId", "dbo.Maquinas");
            DropForeignKey("dbo.OrdemProducaoPecas", "ExpectativaId", "dbo.Expectativas");
            DropForeignKey("dbo.OrdemProducaoPecas", "CorPecaId", "dbo.CorPecas");
            DropForeignKey("dbo.ControleDeQualidades", "OrdemProducaoPeca_CodigoIdentificador", "dbo.OrdemProducaoPecas");
            DropForeignKey("dbo.OrdemProducaoPecas", "ControleDeQualidade_ControleDeQualidadeId", "dbo.ControleDeQualidades");
            DropForeignKey("dbo.ControleDeQualidadeCopoes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.ControleDeQualidadeCopoes", "OrdemProducaoCopoId", "dbo.OrdemProducaoCopoes");
            DropForeignKey("dbo.ParadaCopoes", "OrdemProducaoCopo_CodigoIdentificador", "dbo.OrdemProducaoCopoes");
            DropForeignKey("dbo.OrdemProducaoCopoes", "ParadaCopo_ParadaId", "dbo.ParadaCopoes");
            DropForeignKey("dbo.ParadaCopoes", "OrdemProducaoCopoId", "dbo.OrdemProducaoCopoes");
            DropForeignKey("dbo.OrdemProducaoCopoes", "MaquinaId", "dbo.Maquinas");
            DropForeignKey("dbo.OrdemProducaoCopoes", "ExpectativaId", "dbo.Expectativas");
            DropForeignKey("dbo.EspecificacaoCopoes", "OrdemProducaoCopo_CodigoIdentificador", "dbo.OrdemProducaoCopoes");
            DropForeignKey("dbo.OrdemProducaoCopoes", "EspecificacaoCopo_EspecificacaoCopoId", "dbo.EspecificacaoCopoes");
            DropForeignKey("dbo.EspecificacaoCopoes", "OrdemProducaoCopoId", "dbo.OrdemProducaoCopoes");
            DropForeignKey("dbo.EspecificacaoCopoes", "CorId", "dbo.Cors");
            DropForeignKey("dbo.ControleDeQualidadeCopoes", "OrdemProducaoCopo_CodigoIdentificador", "dbo.OrdemProducaoCopoes");
            DropForeignKey("dbo.OrdemProducaoCopoes", "ControleDeQualidadeCopo_ControleDeQualidadeCopoId", "dbo.ControleDeQualidadeCopoes");
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
            DropIndex("dbo.VendaKits", new[] { "ExpedicaoKit_ExpedicaoKitId" });
            DropIndex("dbo.VendaKits", new[] { "EspecificacaoId" });
            DropIndex("dbo.VendaKits", new[] { "ExpedicaoKitId" });
            DropIndex("dbo.ExpedicaoKits", new[] { "VendaKit_VendaKitId" });
            DropIndex("dbo.ExpedicaoKits", new[] { "MarcantiId" });
            DropIndex("dbo.ExpedicaoKits", new[] { "ClienteId" });
            DropIndex("dbo.Vendas", new[] { "ExpedicaoCopo_ExpedicaoId" });
            DropIndex("dbo.Vendas", new[] { "EspecificacaoCopoId" });
            DropIndex("dbo.Vendas", new[] { "ExpedicaoCopoId" });
            DropIndex("dbo.ExpedicaoCopoes", new[] { "Venda_VendaId" });
            DropIndex("dbo.ExpedicaoCopoes", new[] { "MarcantiId" });
            DropIndex("dbo.ExpedicaoCopoes", new[] { "ClienteId" });
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
            DropIndex("dbo.Especificacaos", new[] { "CorPecaId" });
            DropIndex("dbo.ParadaRefugoes", new[] { "OrdemProducaoRefugo_OrdemProducaoRefugoId" });
            DropIndex("dbo.ParadaRefugoes", new[] { "OrdemProducaoRefugoId" });
            DropIndex("dbo.OrdemProducaoRefugoes", new[] { "ParadaRefugo_ParadaRefugoId" });
            DropIndex("dbo.OrdemProducaoRefugoes", new[] { "EspecificacaoRefugo_EspecificacaoRefugoId" });
            DropIndex("dbo.OrdemProducaoRefugoes", new[] { "UsuarioId" });
            DropIndex("dbo.EspecificacaoRefugoes", new[] { "OrdemProducaoRefugo_OrdemProducaoRefugoId" });
            DropIndex("dbo.EspecificacaoRefugoes", new[] { "OrdemProducaoRefugoId" });
            DropIndex("dbo.EspecificacaoRefugoes", new[] { "CorPecaId" });
            DropIndex("dbo.Paradas", new[] { "OrdemProducaoPeca_CodigoIdentificador" });
            DropIndex("dbo.Paradas", new[] { "OrdemProducaoPecaId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "OrdemProducaoKit_CodigoIdentificadorKit" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "Parada_ParadaId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "ControleDeQualidade_ControleDeQualidadeId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "CorPecaId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "MaquinaId" });
            DropIndex("dbo.OrdemProducaoPecas", new[] { "ExpectativaId" });
            DropIndex("dbo.ControleDeQualidades", new[] { "OrdemProducaoPeca_CodigoIdentificador" });
            DropIndex("dbo.ControleDeQualidades", new[] { "OrdemProducaoPecaId" });
            DropIndex("dbo.ControleDeQualidades", new[] { "Assinatura" });
            DropIndex("dbo.ParadaCopoes", new[] { "OrdemProducaoCopo_CodigoIdentificador" });
            DropIndex("dbo.ParadaCopoes", new[] { "OrdemProducaoCopoId" });
            DropIndex("dbo.EspecificacaoCopoes", new[] { "OrdemProducaoCopo_CodigoIdentificador" });
            DropIndex("dbo.EspecificacaoCopoes", new[] { "OrdemProducaoCopoId" });
            DropIndex("dbo.EspecificacaoCopoes", new[] { "CorId" });
            DropIndex("dbo.OrdemProducaoCopoes", new[] { "ParadaCopo_ParadaId" });
            DropIndex("dbo.OrdemProducaoCopoes", new[] { "EspecificacaoCopo_EspecificacaoCopoId" });
            DropIndex("dbo.OrdemProducaoCopoes", new[] { "ControleDeQualidadeCopo_ControleDeQualidadeCopoId" });
            DropIndex("dbo.OrdemProducaoCopoes", new[] { "MaquinaId" });
            DropIndex("dbo.OrdemProducaoCopoes", new[] { "ExpectativaId" });
            DropIndex("dbo.ControleDeQualidadeCopoes", new[] { "OrdemProducaoCopo_CodigoIdentificador" });
            DropIndex("dbo.ControleDeQualidadeCopoes", new[] { "UsuarioId" });
            DropIndex("dbo.ControleDeQualidadeCopoes", new[] { "OrdemProducaoCopoId" });
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
            DropTable("dbo.VendaKits");
            DropTable("dbo.ExpedicaoKits");
            DropTable("dbo.Vendas");
            DropTable("dbo.Marcantis");
            DropTable("dbo.ExpedicaoCopoes");
            DropTable("dbo.ParadaKits");
            DropTable("dbo.KitPecas");
            DropTable("dbo.OrdemProducaoKits");
            DropTable("dbo.Especificacaos");
            DropTable("dbo.ParadaRefugoes");
            DropTable("dbo.OrdemProducaoRefugoes");
            DropTable("dbo.EspecificacaoRefugoes");
            DropTable("dbo.Paradas");
            DropTable("dbo.CorPecas");
            DropTable("dbo.OrdemProducaoPecas");
            DropTable("dbo.ControleDeQualidades");
            DropTable("dbo.ParadaCopoes");
            DropTable("dbo.Expectativas");
            DropTable("dbo.Cors");
            DropTable("dbo.EspecificacaoCopoes");
            DropTable("dbo.OrdemProducaoCopoes");
            DropTable("dbo.ControleDeQualidadeCopoes");
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
