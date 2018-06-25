using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
    public class ParadaKit
    {
        [Key]
        public int ParadaId { get; set; }
        public String HoraParada { get; set; }
        public String HoraRetorno { get; set; }
        public String Motivo { get; set; }
        public String Observacoes { get; set; }
        [ForeignKey("OrdemProducaoKit")]
        public String CodigoIdentificadorKit { get; set; }
        public OrdemProducaoKit OrdemProducaoKit { get; set; }
    }
}