using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
    public class ExpedicaoCopo
    {
        [Key]
        public int ExpedicaoId { get; set; }

        public List<Venda> Vendas { get; set; }
        public Venda Venda { get; set; }

        [Required(ErrorMessage = "Preencha este campo")]
        public Double ValorTotal { get; set; }

        [Required(ErrorMessage = "Preencha este campo")]
        public DateTime Vencimento { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Marcanti")]
        public int MarcantiId { get; set; }
        public Marcanti Marcanti { get; set; }

        public void CalcValorTotal(Double subtotal)
        {
            this.ValorTotal = this.ValorTotal + subtotal;
        }

    }
}
