using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe
{
    public class FormularioOSAtividade
    {
        [Key]
        public int FormularioOSAtividadeId { get; set; }

        [ForeignKey("FormularioOrdemServico")]
        public int FormularioOrdemServicoId { get; set; }
        public FormularioOrdemServico FormularioOrdemServico { get; set; }

        [ForeignKey("AtividadeOS")]
        public int AtividadeOSId { get; set; }
        public AtividadeOS AtividadeOS { get; set; }

        public Boolean StatusOS { get; set; }
        
    }
}