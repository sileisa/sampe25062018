using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
	public class OrdemProducaoKit
	{
		public OrdemProducaoKit()
		{
			this.Especificacoes = new HashSet<Especificacao>();
			this.KitPecas = new HashSet<KitPeca>();
		}
		[Key]
		public String CodigoIdentificadorKit { get; set; }
		public DateTime Data { get; set; }
		public String ProdIncio { get; set; }
		public String ProdFim { get; set; }
		public int TotalProduzido { get; set; }
		public Boolean NivelamentoBalanca { get; set; }
		//public String Obs { get; set; }
		public int OPnoMes { get; set; }
		public bool Status { get; set; }

		[ForeignKey("Usuario")]
		public int Operdor { get; set; }
		public Usuario Usuario { get; set; }

		public ICollection<int> ParadasKitId { get; set; }
		public ICollection<ParadaKit> ParadasKit { get; set; }
		public ParadaKit ParadaKit { get; set; }

		public ICollection<String> OrdemProducaoPecasId { get; set; }
		public List<OrdemProducaoPeca> OrdemProducaoPecas { get; set; }
		public OrdemProducaoPeca OrdemProducaoPeca { get; set; }

		public ICollection<KitPeca> KitPecas { get; set; }
		public ICollection<int> KitPecasId { get; set; }

		public ICollection<int> EspecificacoesId { get; set; }
		public ICollection<Especificacao> Especificacoes { get; set; }
		public Especificacao Especificacao { get; set; }

		public void calculaProdTotal(int qtd)
		{
			TotalProduzido += qtd;
		}

		public void GerarCodigo()
		{
			//Ordem de concatenação "M",Ano,Mes,Ordem coronológica das ops no mês
			int ano = 0;
			if (Data.Year > 2017)
			{
				ano = (Data.Year - 2017) + 1;
			}

			CodigoIdentificadorKit = String.Concat("M", ano.ToString(), CodigoMes(), OPnoMes.ToString());
		}

		public String CodigoMes()
		{
			String mes = "";
			switch (Data.Month)
			{
				case 1:
					mes = "A";
					break;
				case 2:
					mes = "B";
					break;
				case 3:
					mes = "C";
					break;
				case 4:
					mes = "D";
					break;
				case 5:
					mes = "E";
					break;
				case 6:
					mes = "F";
					break;
				case 7:
					mes = "G";
					break;
				case 8:
					mes = "H";
					break;
				case 9:
					mes = "I";
					break;
				case 10:
					mes = "J";
					break;
				case 11:
					mes = "K";
					break;
				case 12:
					mes = "L";
					break;

			}
			return mes;

		}

		public List<string> RemoveExtraFalseFromCheckbox(List<string> val)
		{
			List<string> d_taxe1_list = new List<string>(val);

			int y = 0;

			foreach (string cbox in val)
			{

				if (val[y] == "false")
				{
					if (y > 0)
					{
						if (val[y - 1] == "true")
						{
							d_taxe1_list[y] = "remove";
						}
					}
				}
				y++;
			}

			val = new List<string>(d_taxe1_list);

			foreach (var del in d_taxe1_list)
				if (del == "remove") val.Remove(del);

			return val;

		}
	}
}