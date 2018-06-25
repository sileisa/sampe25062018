using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
	public class OrdemProducaoCopo
	{
		public OrdemProducaoCopo()
		{
			
		}
		[Key]
		public String CodigoIdentificador { get; set; }
		//Codigo alfanumérico que identifica os formulários
		[ForeignKey("Expectativa")]
		public int ExpectativaId { get; set; }
		public Expectativa Expectativa { get; set; }
		public int OPnoMes { get; set; }
		public DateTime Data { get; set; }
		[Required]
		public String MateriaPrima { get; set; }
		public String MPLote { get; set; }
		public Double MPConsumo { get; set; }
		public String ProdIncio { get; set; }
		public String ProdFim { get; set; }
		[ForeignKey("Maquina")]
		public int MaquinaId { get; set; }
		public Maquina Maquina { get; set; }
		public Double TempAgua { get; set; }
		public Double NivelOleo { get; set; }
		public Double RefugoKg { get; set; }
		public int TotalProduzidos { get; set; }
		public Double ContadorInicial { get; set; }
		public Double ContadorFinal { get; set; }
		public bool Status { get; set; }

		public ICollection<ControleDeQualidadeCopo> ControleDeQualidadeCopos { get; set; }
		public ICollection<int> ControleDeQualidadeCopoId { get; set; }
		public ControleDeQualidadeCopo ControleDeQualidadeCopo { get; set; }

		public ICollection<ParadaCopo> ParadasCopo { get; set; }
		public ICollection<int> ParadasCopoId { get; set; }
		public ParadaCopo ParadaCopo { get; set; }

		public ICollection<EspecificacaoCopo> EspecificacoesCopo { get; set; }
		public ICollection<int> EspecificacoesCopoId { get; set; }
		public EspecificacaoCopo EspecificacaoCopo { get; set; }

		public void calculaProdTotal(int qtd, OrdemProducaoCopo op)
		{
            op. TotalProduzidos += qtd;
		}

		public void GerarCodigo()
		{
			//Ordem de concatenação "I",Ano,Mes,Ordem coronológica das ops no mês
			int ano = 0;
			if (Data.Year > 2017)
			{
				ano = (Data.Year - 2017) + 1;
			}

			CodigoIdentificador = String.Concat("T", ano.ToString(), CodigoMes(), OPnoMes.ToString());
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