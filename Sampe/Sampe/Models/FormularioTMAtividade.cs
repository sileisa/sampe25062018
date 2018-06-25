using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe
{
    public class FormularioTMAtividade
    {
        [Key]
        public int FormularioTMAtividadeId { get; set; }

        [ForeignKey("FormularioTrocaMolde")]
        public int FormularioTrocaMoldeId { get; set; }
        public FormularioTrocaMolde FormularioTrocaMolde { get; set; }

        [ForeignKey("AtividadeTM")]
        public int AtividadeTMId { get; set; }
        public AtividadeTM AtividadeTM { get; set; }

        public Boolean StatusTM { get; set; }

      
    }
}