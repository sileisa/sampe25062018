namespace Sampe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
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
                        Dt = c.String(unicode: false),
                        Intervalo = c.Boolean(nullable: false),
                        IntervaloInicio = c.String(unicode: false),
                        IntervaloFim = c.String(unicode: false),
                        ObsIntervalo = c.String(unicode: false),
                        MaterialUsado = c.String(unicode: false),
                        QuantUsado = c.Int(),
                        MaterialSobrante = c.String(unicode: false),
                        QuantSobrante = c.Int(),
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
                        DescricaoCargo = c.String(unicode: false),
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
                        DtRetirada = c.String(unicode: false),
                        DtColocar = c.String(unicode: false),
                        ColocarInicio = c.String(unicode: false),
                        ColocarFim = c.String(unicode: false),
                        RetirarInicio = c.String(unicode: false),
                        RetirarFim = c.String(unicode: false),
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
                "dbo.Logins",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        User = c.String(unicode: false),
                        Senha = c.String(unicode: false),
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
