using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        [Required(ErrorMessage = "Preencha este campo")]
        public Double ValorUnitario { get; set; }

		[Required(ErrorMessage = "Preencha este campo")]
		public int Quantidade { get; set; }

		[Required(ErrorMessage = "Preencha este campo")]
        public Double Subtotal { get; set; }

        [ForeignKey("ExpedicaoCopo")]
        public int ExpedicaoCopoId { get; set; }
        public ExpedicaoCopo ExpedicaoCopo { get; set; }

        [ForeignKey("EspecificacaoCopo")]
        public int EspecificacaoCopoId { get; set; }
        public EspecificacaoCopo EspecificacaoCopo { get; set; }

        public void CalcSubtotal(Double valorUnitario, int quantidade)
        {           
                this.Subtotal = valorUnitario * quantidade;           
        }

	}
}