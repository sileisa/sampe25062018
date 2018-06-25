using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
    public class KitPeca
    {
        [Key]
        public int KitPecaId { get; set; }

        [ForeignKey("OrdemProducaoPeca")]
        public String CodigoIdentificador { get; set; }
        public OrdemProducaoPeca OrdemProducaoPeca { get; set; }

        [ForeignKey("OrdemProducaoKit")]
        public String CodigoIdentificadorKit { get; set; }
        public OrdemProducaoKit OrdemProducaoKit { get; set; }
    }
}