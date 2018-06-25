using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sampe
{
    public class AtividadeTM
    {
        public AtividadeTM()
        {
            this.FormularioTrocaMoldes = new HashSet<FormularioTrocaMolde>();
        }
        public int AtividadeTMId { get; set; }
        public string NomeAtvTm { get; set; }
        public ICollection<FormularioTrocaMolde> FormularioTrocaMoldes { get; set; }

    }
}