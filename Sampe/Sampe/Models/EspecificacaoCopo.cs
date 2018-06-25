using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
	public class EspecificacaoCopo
	{
		[Key]
		public int EspecificacaoCopoId { get; set; }

		[ForeignKey("Cor")]
		public int CorId { get; set; }
		public Cor Cor { get; set; }

		public int UniProd { get; set; }
		public string LoteMaster { get; set; }

		[ForeignKey("OrdemProducaoCopo")]
		public String OrdemProducaoCopoId { get; set; }
		public OrdemProducaoCopo OrdemProducaoCopo { get; set; }
	}
}