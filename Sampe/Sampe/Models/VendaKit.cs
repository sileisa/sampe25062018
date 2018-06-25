using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
	public class VendaKit
	{
		[Key]
		public int VendaKitId { get; set; }

		[Required(ErrorMessage = "Preencha este campo")]
		public Double ValorUnitario { get; set; }

		[Required(ErrorMessage = "Preencha este campo")]
		public int Quantidade { get; set; }

		[Required(ErrorMessage = "Preencha este campo")]
		public Double Subtotal { get; set; }

		[ForeignKey("ExpedicaoKit")]
		public int ExpedicaoKitId { get; set; }
		public ExpedicaoKit ExpedicaoKit { get; set; }

		[ForeignKey("Especificacao")]
		public int EspecificacaoId { get; set; }
		public Especificacao Especificacao { get; set; }

		public void CalcSubtotal(Double valorUnitario, int quantidade)
		{
			this.Subtotal = valorUnitario * quantidade;
		}
	}
}