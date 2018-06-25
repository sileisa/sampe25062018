using PagedList;
using Rotativa;
using Rotativa.Options;
using Sampe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sampe.Controllers
{
    public class RelatorioController : Controller
    {
        private SampeContext db = new SampeContext();

		public ActionResult ExibeOpPeca(int? pagina, Boolean? gerarPdf, string id)
		{
			OrdemProducaoPeca ordemProducaoPeca = db.OrdemProducaoPecas.Find(id);
			ordemProducaoPeca.Expectativa = db.Expectativas.Find(ordemProducaoPeca.ExpectativaId);
			db.Entry(ordemProducaoPeca).Reference(f => f.Expectativa).Load();
			db.Entry(ordemProducaoPeca).Reference(f => f.Maquina).Load();
			var busca = db.Paradas.Where(o => o.OrdemProducaoPecaId == id).ToList();
			ordemProducaoPeca.Paradas = busca;
			var busca2 = db.ControleDeQualidades.Where(o => o.OrdemProducaoPecaId == id).Include(o => o.Usuario).ToList();
			ordemProducaoPeca.ControlesDeQualidade = busca2;
			List<OrdemProducaoPeca> producoes = new List<OrdemProducaoPeca>();
			producoes.Add(ordemProducaoPeca);
			if (gerarPdf != true)
			{
				int pgQtdRegistro = 2;
				int pgNav = (pagina ?? 1);
				return View(producoes.ToPagedList(pgNav, pgQtdRegistro));
			}
			else
			{
				int paginaNumero = 1;
				var pdf = new ViewAsPdf
				{
					ViewName = "ExibeOpPeca",
					PageSize = Size.A4,
					IsGrayScale = false,
					Model = producoes.ToPagedList(paginaNumero, producoes.Count)
				};
				return pdf;
			}
		}
		public ActionResult ExibeOpKit(int? pagina, Boolean? gerarPdf, string id)
		{
			OrdemProducaoKit ordemProducaoKit = db.OrdemProducaoKits.Find(id);
			db.Entry(ordemProducaoKit).Reference(o => o.Usuario).Load();
			var busca = db.Especificacaos.Where(o => o.CodigoIdentificadorKit == id).Include(o => o.Cliente).Include(o => o.CorPeca).ToList();
			ordemProducaoKit.Especificacoes = busca;
			var busca2 = db.ParadaKits.Where(o => o.CodigoIdentificadorKit == id).ToList();
			ordemProducaoKit.ParadasKit = busca2;
			var busca3 = from Kit in db.OrdemProducaoKits
						 where Kit.CodigoIdentificadorKit == ordemProducaoKit.CodigoIdentificadorKit
						 join kitPecas in db.KitPecas
						 on Kit.CodigoIdentificadorKit equals kitPecas.CodigoIdentificadorKit
						 join peca in db.OrdemProducaoPecas
						 on kitPecas.CodigoIdentificador equals peca.CodigoIdentificador
						 select kitPecas.OrdemProducaoPeca;
			ordemProducaoKit.OrdemProducaoPecas = busca3.ToList();
			List<OrdemProducaoKit> producoes = new List<OrdemProducaoKit>();
			producoes.Add(ordemProducaoKit);
			if (gerarPdf != true)
			{
				int pgQtdRegistro = 2;
				int pgNav = (pagina ?? 1);
				return View(producoes.ToPagedList(pgNav, pgQtdRegistro));
			}
			else
			{
				int paginaNumero = 1;
				var pdf = new ViewAsPdf
				{
					ViewName = "ExibeOpKit",
					PageSize = Size.A4,
					IsGrayScale = false,
					Model = producoes.ToPagedList(paginaNumero, producoes.Count)
				};
				return pdf;
			}
		}
		public ActionResult ExibeOpRefugo(int? pagina, Boolean? gerarPdf, string id)
		{
			OrdemProducaoRefugo ordemProducaoRefugo = db.OrdemProducaoRefugoes.Find(id);
			var busca = db.ParadaRefugoes.Where(o => o.OrdemProducaoRefugoId == id).ToList();
			ordemProducaoRefugo.ParadasRefugo = busca;
			var busca2 = db.EspecificacaoRefugoes.Where(o => o.OrdemProducaoRefugoId == id).Include(o => o.CorPeca).ToList();
			ordemProducaoRefugo.EspecificacoesRefugo = busca2;
			List<OrdemProducaoRefugo> producoes = new List<OrdemProducaoRefugo>();
			producoes.Add(ordemProducaoRefugo);
			if (gerarPdf != true)
			{
				int pgQtdRegistro = 2;
				int pgNav = (pagina ?? 1);
				return View(producoes.ToPagedList(pgNav, pgQtdRegistro));
			}
			else
			{
				int paginaNumero = 1;
				var pdf = new ViewAsPdf
				{
					ViewName = "ExibeOpRefugo",
					PageSize = Size.A4,
					IsGrayScale = false,
					Model = producoes.ToPagedList(paginaNumero, producoes.Count)
				};
				return pdf;
			}
		}
		public ActionResult ExibeOpCopo(int? pagina, Boolean? gerarPdf, string id)
		{
			OrdemProducaoCopo ordemProducaoCopo = db.OrdemProducaoCopoes.Find(id);
			var busca = db.EspecificacaoCopoes.Where(o => o.OrdemProducaoCopoId == id).Include(o => o.Cor).ToArray();
			var busca2 = db.ParadaCopoes.Where(o => o.OrdemProducaoCopoId == id).ToArray();
			var busca3 = db.ControleDeQualidadeCopoes.Where(o => o.OrdemProducaoCopoId == id).Include(o => o.Usuario).ToArray();
			ordemProducaoCopo.EspecificacoesCopo = busca;
			ordemProducaoCopo.ParadasCopo = busca2;
			ordemProducaoCopo.ControleDeQualidadeCopos = busca3;
			List<OrdemProducaoCopo> producoes = new List<OrdemProducaoCopo>();
			producoes.Add(ordemProducaoCopo);
			if (gerarPdf != true)
			{
				int pgQtdRegistro = 2;
				int pgNav = (pagina ?? 1);
				return View(producoes.ToPagedList(pgNav, pgQtdRegistro));
			}
			else
			{
				int paginaNumero = 1;
				var pdf = new ViewAsPdf
				{
					ViewName = "ExibeOpCopo",
					PageSize = Size.A4,
					IsGrayScale = false,
					Model = producoes.ToPagedList(paginaNumero, producoes.Count)
				};
				return pdf;
			}
		}
		public ActionResult ExibeExpedKit(int? pagina, Boolean? gerarPdf, int id)
		{
			ExpedicaoKit expedicaoKit = db.ExpedicaoKits.Find(id);
			db.Entry(expedicaoKit).Reference(f => f.Cliente).Load();
			db.Entry(expedicaoKit).Reference(f => f.Marcanti).Load();
			var busca = from venda in db.VendaKits
						where venda.ExpedicaoKitId == id
						select venda;
			//var busca2 = db.Especificacaos.Where(o=>o.OrdemProducaoKit== id)
			List<ExpedicaoKit> expedicoes = new List<ExpedicaoKit>();
			expedicaoKit.VendasKit = busca.Include(o => o.Especificacao).Include(o=>o.Especificacao.CorPeca).ToList();
			expedicoes.Add(expedicaoKit);
			if (gerarPdf != true)
			{
				int pgQtdRegistro = 2;
				int pgNav = (pagina ?? 1);
				return View(expedicoes.ToPagedList(pgNav, pgQtdRegistro));
			}
			else
			{
				int paginaNumero = 1;
				var pdf = new ViewAsPdf
				{
					ViewName = "ExibeExpedKit",
					PageSize = Size.A4,
					IsGrayScale = false,
					Model = expedicoes.ToPagedList(paginaNumero, expedicoes.Count)
				};
				return pdf;
			}
		}

		public ActionResult ExibeExped(int? pagina, Boolean? gerarPdf, int id)
		{
			ExpedicaoCopo expedicaoCopo = db.ExpedicaoCopoes.Find(id);
			db.Entry(expedicaoCopo).Reference(f => f.Cliente).Load();
			db.Entry(expedicaoCopo).Reference(f => f.Marcanti).Load();
			var busca = from venda in db.Vendas
						where venda.ExpedicaoCopoId == id
						select venda;
			List<ExpedicaoCopo> expedicoes = new List<ExpedicaoCopo>();
			expedicaoCopo.Vendas = busca.Include(o => o.EspecificacaoCopo).Include(o => o.EspecificacaoCopo.Cor).ToList();
			expedicoes.Add(expedicaoCopo);
			if (gerarPdf != true)
			{
				int pgQtdRegistro = 2;
				int pgNav = (pagina ?? 1);
				return View(expedicoes.ToPagedList(pgNav, pgQtdRegistro));
			}
			else
			{
				int paginaNumero = 1;
				var pdf = new ViewAsPdf
				{
					ViewName = "ExibeExped",
					PageSize = Size.A4,
					IsGrayScale = false,
					Model = expedicoes.ToPagedList(paginaNumero, expedicoes.Count)
				};
				return pdf;
			}
		}

		public ActionResult BuscaCliente()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaCliente(int? pagina, Boolean? gerarPdf, int ClienteId)
        {
            var kits = db.Especificacaos.Where(o => o.ClienteId == ClienteId).ToList();
            ViewBag.cliente = db.Clientes.Find(ClienteId).NomeCliente;
            if (gerarPdf == true)
            {
                int pgQtdRegistro = 10;
                int pgNav = (pagina ?? 1);

                return View(kits.ToPagedList(pgNav, pgQtdRegistro));
            }
            else
            {
                int paginaNumero = 1;
                var pdf = new ViewAsPdf
                {
                    ViewName = "ExibeKit",
                    PageSize = Size.A4,
                    IsGrayScale = false,
                    Model = kits.ToPagedList(paginaNumero, kits.Count)
                };
                return pdf;
            }
        }

        
        public ActionResult ExibeOs(int? pagina, Boolean? gerarPdf, int id)
        {
            FormularioOrdemServico formularioOrdemServico = db.FormularioOrdemServicoes.Find(id);
            var busca = from FormularioOrdemServicos in db.FormularioOrdemServicoes
                        where FormularioOrdemServicos.FormularioOrdemServicoId == formularioOrdemServico.FormularioOrdemServicoId
                        join FormularioOSAtividades in db.FormularioOSAtividade
                        on FormularioOrdemServicos.FormularioOrdemServicoId equals FormularioOSAtividades.FormularioOrdemServicoId
                        join AtividadeOS in db.AtividadeOS
                        on FormularioOSAtividades.AtividadeOSId equals AtividadeOS.AtividadeOSId
                        select FormularioOSAtividades;

            var busca2 = from FormularioOrdemServicos in db.FormularioOrdemServicoes
                         where FormularioOrdemServicos.FormularioOrdemServicoId == formularioOrdemServico.FormularioOrdemServicoId
                         join FormularioOSAtividades in db.FormularioOSAtividade
                        on FormularioOrdemServicos.FormularioOrdemServicoId equals FormularioOSAtividades.FormularioOrdemServicoId
                         join AtividadeOS in db.AtividadeOS
                         on FormularioOSAtividades.AtividadeOSId equals AtividadeOS.AtividadeOSId
                         select FormularioOSAtividades.AtividadeOS;
            List<FormularioOrdemServico> buscaOs = new List<FormularioOrdemServico>();

            db.Entry(formularioOrdemServico).Reference(f => f.Maquina).Load();
            db.Entry(formularioOrdemServico).Reference(f => f.Usuario).Load();
            formularioOrdemServico.FormularioOSAtividades = busca.ToArray();
            formularioOrdemServico.AtividadesOs = busca2.ToArray();
            buscaOs.Add(formularioOrdemServico);
            if (gerarPdf != true)
            {
                int pgQtdRegistro = 2;
                int pgNav = (pagina ?? 1);
                return View(buscaOs.ToPagedList(pgNav, pgQtdRegistro));
            }
            else
            {
                int paginaNumero = 1;
                var pdf = new ViewAsPdf
                {
                    ViewName = "ExibeOs",
                    PageSize = Size.A4,
                    IsGrayScale = false,
                    Model = buscaOs.ToPagedList(paginaNumero, buscaOs.Count)
                };
                return pdf;
            }
        }
        public ActionResult ExibeTm(int? pagina, Boolean? gerarPdf, int id)
        {
            FormularioTrocaMolde formularioTrocaMolde = db.FormularioTrocaMoldes.Find(id);

            var busca = from FormularioTrocaMoldes in db.FormularioTrocaMoldes
                        where FormularioTrocaMoldes.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                        join FormularioTMAtividades in db.FormularioTMAtividade
                        on FormularioTrocaMoldes.FormularioTrocaMoldeId equals FormularioTMAtividades.FormularioTrocaMoldeId
                        join AtividadeTM in db.AtividadeTMs
                        on FormularioTMAtividades.AtividadeTMId equals AtividadeTM.AtividadeTMId
                        select FormularioTMAtividades;

            var busca2 = from Formulario in db.FormularioTrocaMoldes
                         where Formulario.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                         join Relacional in db.FormularioTMAtividade
                         on Formulario.FormularioTrocaMoldeId equals Relacional.FormularioTrocaMoldeId
                         join Atividade in db.AtividadeTMs
                         on Relacional.AtividadeTMId equals Atividade.AtividadeTMId
                         select Relacional.AtividadeTM;

            var busca3 = from Formulario in db.FormularioTrocaMoldes
                         where Formulario.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                         join Relacional in db.FormularioMolde
                         on Formulario.FormularioTrocaMoldeId equals Relacional.FormularioTrocaMoldeId
                         join Molde in db.Moldes
                         on Relacional.MoldeId equals Molde.MoldeId
                         select Relacional.Molde;

            //var user = User.Identity.Name;
            //db.Entry(formularioTrocaMolde).Reference(f => f.Molde).Load();
            db.Entry(formularioTrocaMolde).Reference(f => f.Maquina).Load();
            db.Entry(formularioTrocaMolde).Reference(f => f.Usuario).Load();
            formularioTrocaMolde.FormularioTMAtividades = busca.ToList();
            formularioTrocaMolde.AtividadesTM = busca2.ToList();
            formularioTrocaMolde.Moldes = busca3.ToList();

            List<FormularioTrocaMolde> buscaTm = new List<FormularioTrocaMolde>();
            buscaTm.Add(formularioTrocaMolde);
            if (gerarPdf != true)
            {
                int pgQtdRegistro = 2;
                int pgNav = (pagina ?? 1);
                return View(buscaTm.ToPagedList(pgNav, pgQtdRegistro));
            }
            else
            {
                int paginaNumero = 1;
                var pdf = new ViewAsPdf
                {
                    ViewName = "ExibeTm",
                    PageSize = Size.A4,
                    IsGrayScale = false,
                    Model = buscaTm.ToPagedList(paginaNumero, buscaTm.Count)
                };
                return pdf;
            }
        }

        // GET: Relatorio

        public ActionResult BuscaOS()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaOS(DateTime? dt1, DateTime? dt2, int? pagina, Boolean? gerarPdf)
        {
            var buscaOS = db.FormularioOrdemServicoes.Where(a => a.Dt >= dt1 && a.Dt <= dt2 && a.Status == true).ToList();
            if (dt1 == null)
            {
                ViewBag.Error1 = "Preencha este Campo";
            }
            if (dt2 == null)
            {
                ViewBag.Error2 = "Preencha este Campo";
            }

            var cont = buscaOS.Count();
            if (cont == 0)
            {
                ViewBag.Error3 = "Não há formulários concuídos neste período.";

            }
            else
            {
                ViewBag.dt1 = dt1;
                ViewBag.dt2 = dt2;
                foreach (var item in buscaOS)
                {
                    FormularioOrdemServico formularioOrdemServico = db.FormularioOrdemServicoes.Find(item.FormularioOrdemServicoId);
                    db.Entry(formularioOrdemServico).Reference(f => f.Maquina).Load();
                    db.Entry(formularioOrdemServico).Reference(f => f.Usuario).Load();
                }
                if (gerarPdf == true)
                {
                    int pgQtdRegistro = 2;
                    int pgNav = (pagina ?? 1);

                    return View(buscaOS.ToPagedList(pgNav, pgQtdRegistro));
                }
                else
                {
                    int paginaNumero = 1;
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "OsData",
                        PageSize = Size.A4,
                        IsGrayScale = false,
                        Model = buscaOS.ToPagedList(paginaNumero, buscaOS.Count)
                    };
                    return pdf;
                }
            }
            return View();

        }
        public ActionResult BuscaTM()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaTM(DateTime? dt1, DateTime? dt2, int? pagina, Boolean? gerarPdf, int? id)
        {

            var buscaTM = db.FormularioTrocaMoldes.Where(a => a.DtRetirada >= dt1 && a.DtRetirada <= dt2 && a.Status == true).ToList();
            if (dt1 == null)
            {
                ViewBag.Error1 = "Preencha este Campo";
            }
            if (dt2 == null)
            {
                ViewBag.Error2 = "Preencha este Campo";
            }

            var cont = buscaTM.Count();
            if (cont == 0)
            {
                ViewBag.Error3 = "Não há formulários concuídos neste período.";

            }
            else
            {
                ViewBag.dt1 = dt1;
                ViewBag.dt2 = dt2;
                foreach (var item in buscaTM)
                {
                    FormularioTrocaMolde formularioTrocaMolde = db.FormularioTrocaMoldes.Find(item.FormularioTrocaMoldeId);
                    db.Entry(formularioTrocaMolde).Reference(f => f.Maquina).Load();
                    db.Entry(formularioTrocaMolde).Reference(f => f.Usuario).Load();
                }

                if (gerarPdf == true)
                {
                    int pgQtdRegistro = 10;
                    int pgNav = (pagina ?? 1);

                    return View(buscaTM.ToPagedList(pgNav, pgQtdRegistro));
                }
                else
                {
                    int paginaNumero = 1;
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "TmData",
                        PageSize = Size.A4,
                        IsGrayScale = false,
                        Model = buscaTM.ToPagedList(paginaNumero, buscaTM.Count)
                    };

                    return pdf;

                }
            }
            return View();

        }
        public ActionResult BuscaTMporMaquina()
        {
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaTMporMaquina(int MaquinaId, int? pagina, Boolean? gerarPdf, int? id)
        {

            var buscaTM = db.FormularioTrocaMoldes.Where(a => a.MaquinaId == MaquinaId && a.Status == true).ToList();
            var cont = buscaTM.Count();

            if (cont == 0)
            {
                ViewBag.Error = "Não há formulários concuídos nesta máquina.";

            }
            else
            {
                foreach (var item in buscaTM)
                {

                    FormularioTrocaMolde formularioTrocaMolde = db.FormularioTrocaMoldes.Find(item.FormularioTrocaMoldeId);
                    db.Entry(formularioTrocaMolde).Reference(f => f.Maquina).Load();
                    ViewBag.MaquinaId = formularioTrocaMolde.Maquina.NomeMaquina;
                    db.Entry(formularioTrocaMolde).Reference(f => f.Usuario).Load();

                }

                if (gerarPdf == true)
                {
                    int pgQtdRegistro = 10;
                    int pgNav = (pagina ?? 1);

                    return View(buscaTM.ToPagedList(pgNav, pgQtdRegistro));
                }
                else
                {
                    int paginaNumero = 1;
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "TmMaquina",
                        PageSize = Size.A4,
                        IsGrayScale = false,
                        Model = buscaTM.ToPagedList(paginaNumero, buscaTM.Count)
                    };

                    return pdf;

                }
            }
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina");
            return View();

        }
        public ActionResult BuscaOSporMaquina()
        {
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaOSporMaquina(int MaquinaId, int? pagina, Boolean? gerarPdf, int? id)
        {

            var buscaOS = db.FormularioOrdemServicoes.Where(a => a.MaquinaId == MaquinaId && a.Status == true).ToList();            
            
            var cont = buscaOS.Count();            

            if (cont == 0)
            {
                ViewBag.Error = "Não há formulários concuídos nesta máquina.";

            }
            else
            {
                foreach (var item in buscaOS)
                {
                    FormularioOrdemServico formularioOrdemServico = db.FormularioOrdemServicoes.Find(item.FormularioOrdemServicoId);
                    db.Entry(formularioOrdemServico).Reference(f => f.Maquina).Load();
                    db.Entry(formularioOrdemServico).Reference(f => f.Usuario).Load();
                    ViewBag.MaquinaId = formularioOrdemServico.Maquina.NomeMaquina;

                }
                if (gerarPdf == true)
                {
                    int pgQtdRegistro = 10;
                    int pgNav = (pagina ?? 1);
                    
                    return View(buscaOS.ToPagedList(pgNav, pgQtdRegistro));
                }
                else
                {
                    int paginaNumero = 1;
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "OsMaquina",
                        PageSize = Size.A4,
                        IsGrayScale = false,
                        Model = buscaOS.ToPagedList(paginaNumero, buscaOS.Count)
                    };

                    return pdf;

                }
            }
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina");
            return View();

        }
        public ActionResult BuscaOpPeca()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaOpPeca(DateTime? dt1, DateTime? dt2, int? pagina, Boolean? gerarPdf, string id)
        {
            var buscaOpPeca = db.OrdemProducaoPecas.Where(a => a.Data >= dt1 && a.Data <= dt2 && a.Status == true).ToList();

            if (dt1 == null)
            {
                ViewBag.Error1 = "Preencha este Campo";
            }
            if (dt2 == null)
            {
                ViewBag.Error2 = "Preencha este Campo";
            }

            var cont = buscaOpPeca.Count();
            if (cont == 0)
            {
                ViewBag.Error3 = "Não há ordens de produção concuídas neste período.";

            }
            else
            {
                ViewBag.dt1 = dt1;
                ViewBag.dt2 = dt2;
                foreach (var item in buscaOpPeca)
                {
                    OrdemProducaoPeca ordemProducaoPeca = db.OrdemProducaoPecas.Find(item.CodigoIdentificador);
                    db.Entry(ordemProducaoPeca).Reference(f => f.Maquina).Load();
                    db.Entry(ordemProducaoPeca).Reference(f => f.Expectativa).Load();
                    db.Entry(ordemProducaoPeca).Reference(f => f.ControleDeQualidade).Load();
                    db.Entry(ordemProducaoPeca).Reference(f => f.Parada).Load();
                }
                if (gerarPdf == true)
                {
                    int pgQtdRegistro = 10;
                    int pgNav = (pagina ?? 1);

                    return View(buscaOpPeca.ToPagedList(pgNav, pgQtdRegistro));
                }
                else
                {
                    int paginaNumero = 1;
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "OpPecaData",
                        PageSize = Size.A4,
                        IsGrayScale = false,
                        Model = buscaOpPeca.ToPagedList(paginaNumero, buscaOpPeca.Count)
                    };
                    return pdf;
                }
            }
            return View();
        }

        public ActionResult BuscaOpKit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaOpKit(DateTime? dt1, DateTime? dt2, int? pagina, Boolean? gerarPdf, string id)
        {
            var buscaOpKit = db.OrdemProducaoKits.Where(a => a.Data >= dt1 && a.Data <= dt2 && a.Status == true).ToList();
            if (dt1 == null)
            {
                ViewBag.Error1 = "Preencha este Campo";
            }
            if (dt2 == null)
            {
                ViewBag.Error2 = "Preencha este Campo";
            }

            var cont = buscaOpKit.Count();
            if (cont == 0)
            {
                ViewBag.Error3 = "Não há ordens de produção concuídas neste período.";

            }
            else
            {
                ViewBag.dt1 = dt1;
                ViewBag.dt2 = dt2;
                foreach (var item in buscaOpKit)
                {
                    OrdemProducaoKit ordemProducaoKit = db.OrdemProducaoKits.Find(item.CodigoIdentificadorKit);
                    db.Entry(ordemProducaoKit).Reference(f => f.Especificacao).Load();
                    db.Entry(ordemProducaoKit).Reference(f => f.OrdemProducaoPeca).Load();
                    db.Entry(ordemProducaoKit).Reference(f => f.ParadaKit).Load();
                    db.Entry(ordemProducaoKit).Reference(f => f.Usuario).Load();
                }
                if (gerarPdf == true)
                {
                    int pgQtdRegistro = 10;
                    int pgNav = (pagina ?? 1);

                    return View(buscaOpKit.ToPagedList(pgNav, pgQtdRegistro));
                }
                else
                {
                    int paginaNumero = 1;
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "OpKitData",
                        PageSize = Size.A4,
                        IsGrayScale = false,
                        Model = buscaOpKit.ToPagedList(paginaNumero, buscaOpKit.Count)
                    };
                    return pdf;
                }
            }
            return View();
        }
        public ActionResult BuscaOpCopo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaOpCopo(DateTime? dt1, DateTime? dt2, int? pagina, Boolean? gerarPdf)
        {
            var buscaOpCopo = db.OrdemProducaoCopoes.Where(a => a.Data >= dt1 && a.Data <= dt2 && a.Status == true).ToList();


            if (dt1 == null)
            {
                ViewBag.Error1 = "Preencha este Campo";
            }
            if (dt2 == null)
            {
                ViewBag.Error2 = "Preencha este Campo";
            }

            var cont = buscaOpCopo.Count();
            if (cont == 0)
            {
                ViewBag.Error3 = "Não há ordens de produção concuídas neste período.";

            }
            else
            {
                ViewBag.dt1 = dt1;
                ViewBag.dt2 = dt2;
                foreach (var item in buscaOpCopo)
                {
                    OrdemProducaoCopo ordemProducaoCopo = db.OrdemProducaoCopoes.Find(item.CodigoIdentificador);
                    db.Entry(ordemProducaoCopo).Reference(f => f.Maquina).Load();
                    db.Entry(ordemProducaoCopo).Reference(f => f.Expectativa).Load();
                    db.Entry(ordemProducaoCopo).Reference(f => f.ControleDeQualidadeCopo).Load();
                    db.Entry(ordemProducaoCopo).Reference(f => f.ParadaCopo).Load();
                    db.Entry(ordemProducaoCopo).Reference(f => f.EspecificacaoCopo).Load();

                }
                if (gerarPdf == true)
                {
                    int pgQtdRegistro = 2;
                    int pgNav = (pagina ?? 1);

                    return View(buscaOpCopo.ToPagedList(pgNav, pgQtdRegistro));
                }
                else
                {
                    int paginaNumero = 1;
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "OpCopoData",
                        PageSize = Size.A4,
                        IsGrayScale = false,
                        Model = buscaOpCopo.ToPagedList(paginaNumero, buscaOpCopo.Count)
                    };
                    return pdf;
                }
            }
            return View();


        }
        public ActionResult BuscaOpRefugo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaOpRefugo(DateTime? dt1, DateTime? dt2, int? pagina, Boolean? gerarPdf)
        {
            var buscaOpRefugo = db.OrdemProducaoRefugoes.Where(a => a.Data >= dt1 && a.Data <= dt2 && a.Status == true).ToList();
            if (dt1 == null)
            {
                ViewBag.Error1 = "Preencha este Campo";
            }
            if (dt2 == null)
            {
                ViewBag.Error2 = "Preencha este Campo";
            }

            var cont = buscaOpRefugo.Count();
            if (cont == 0)
            {
                ViewBag.Error3 = "Não há ordens de produção concuídas neste período.";

            }
            else
            {
                ViewBag.dt1 = dt1;
                ViewBag.dt2 = dt2;
                foreach (var item in buscaOpRefugo)
                {
                    OrdemProducaoRefugo ordemProducaoRefugo = db.OrdemProducaoRefugoes.Find(item.OrdemProducaoRefugoId);
                    db.Entry(ordemProducaoRefugo).Reference(f => f.Usuario).Load();
                    db.Entry(ordemProducaoRefugo).Reference(f => f.EspecificacaoRefugo).Load();
                    db.Entry(ordemProducaoRefugo).Reference(f => f.ParadaRefugo).Load();

                }
                if (gerarPdf == true)
                {
                    int pgQtdRegistro = 2;
                    int pgNav = (pagina ?? 1);

                    return View(buscaOpRefugo.ToPagedList(pgNav, pgQtdRegistro));
                }
                else
                {
                    int paginaNumero = 1;
                    var pdf = new ViewAsPdf
                    {
                        ViewName = "OpRefugoData",
                        PageSize = Size.A4,
                        IsGrayScale = false,
                        Model = buscaOpRefugo.ToPagedList(paginaNumero, buscaOpRefugo.Count)
                    };
                    return pdf;
                }
            }
            return View();

        }

    }
}