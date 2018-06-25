using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
	public class EspecificacaoRefugo
	{
		[Key]
		public int EspecificacaoRefugoId { get; set; }
		public string Material { get; set; }
		[ForeignKey("CorPeca")]
		public int CorPecaId { get; set; }
		public CorPeca CorPeca { get; set; }

		public double Peso { get; set; }
		public Boolean Limpeza { get; set; }

		[ForeignKey("OrdemProducaoRefugo")]
		public string OrdemProducaoRefugoId { get; set; }
		public OrdemProducaoRefugo OrdemProducaoRefugo { get; set; }
	}
}