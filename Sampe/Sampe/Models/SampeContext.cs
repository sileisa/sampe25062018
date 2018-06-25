using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
    public class SampeContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SampeContext() : base("name=SampeContext")
        {
        }

        public System.Data.Entity.DbSet<Sampe.FormularioTrocaMolde> FormularioTrocaMoldes { get; set; }

        public System.Data.Entity.DbSet<Sampe.FormularioOrdemServico> FormularioOrdemServicoes { get; set; }

        public System.Data.Entity.DbSet<Sampe.Cargo> Cargoes { get; set; }

        public System.Data.Entity.DbSet<Sampe.Maquina> Maquinas { get; set; }

        public System.Data.Entity.DbSet<Sampe.Molde> Moldes { get; set; }

        public System.Data.Entity.DbSet<Sampe.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<Sampe.AtividadeOS> AtividadeOS { get; set; }

        public System.Data.Entity.DbSet<Sampe.AtividadeTM> AtividadeTMs { get; set; }

        public System.Data.Entity.DbSet<Sampe.FormularioOSAtividade> FormularioOSAtividade { get; set; }

        public System.Data.Entity.DbSet<Sampe.FormularioTMAtividade> FormularioTMAtividade { get; set; }

        public System.Data.Entity.DbSet<Sampe.Login> Logins { get; set; }

        public System.Data.Entity.DbSet<Sampe.FormularioMolde> FormularioMolde { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.Expectativa> Expectativas { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.OrdemProducaoPeca> OrdemProducaoPecas { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.Parada> Paradas { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.ControleDeQualidade> ControleDeQualidades { get; set; }

        public System.Data.Entity.DbSet<Sampe.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.Especificacao> Especificacaos { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.OrdemProducaoKit> OrdemProducaoKits { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.KitPeca> KitPecas { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.ParadaKit> ParadaKits { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.EspecificacaoRefugo> EspecificacaoRefugoes { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.OrdemProducaoRefugo> OrdemProducaoRefugoes { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.ParadaRefugo> ParadaRefugoes { get; set; }

		public System.Data.Entity.DbSet<Sampe.Cor> Cors { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.EspecificacaoCopo> EspecificacaoCopoes { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.OrdemProducaoCopo> OrdemProducaoCopoes { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.ControleDeQualidadeCopo> ControleDeQualidadeCopoes { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.ParadaCopo> ParadaCopoes { get; set; }

		//public System.Data.Entity.DbSet<Sampe.Models.Expedicao> Expedicaos { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.Marcanti> Marcantis { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.ExpedicaoCopo> ExpedicaoCopoes { get; set; }

        public System.Data.Entity.DbSet<Sampe.Models.Venda> Vendas { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.VendaKit> VendaKits { get; set; }

		public System.Data.Entity.DbSet<Sampe.Models.ExpedicaoKit> ExpedicaoKits { get; set; }

		public System.Data.Entity.DbSet<Sampe.CorPeca> CorPecas { get; set; }
	}
}
