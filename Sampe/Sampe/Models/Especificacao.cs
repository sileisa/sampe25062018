using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
	public class Especificacao
	{
		[Key]
		public int EspecificacaoId { get; set; }
		public String TipoKit { get; set; }

		[ForeignKey("CorPeca")]
		public int CorPecaId { get; set; }
		public CorPeca CorPeca { get; set; }

		public Boolean Parafuso { get; set; }
		public int QuantProduzido { get; set; }

		[ForeignKey("Cliente")]
		public int ClienteId { get; set; }
		public Cliente Cliente { get; set; }

		[ForeignKey("OrdemProducaoKit")]
		public String CodigoIdentificadorKit { get; set; }
		public OrdemProducaoKit OrdemProducaoKit { get; set; }



	}
}