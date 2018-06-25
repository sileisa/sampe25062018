using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe
{
    public class FormularioMolde
    {
        [Key]
        public int FormularioMoldeId { get; set; }

        [ForeignKey("FormularioTrocaMolde")]
        public int FormularioTrocaMoldeId { get; set; }
        public FormularioTrocaMolde FormularioTrocaMolde { get; set; }

        [ForeignKey("Molde")]
        public int MoldeId { get; set; }
        public Molde Molde { get; set; }
    }
}