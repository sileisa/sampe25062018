using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sampe
{
    public class AtividadeOS
    {
        public AtividadeOS()
        {
            this.FormularioOrdemServicos = new HashSet<FormularioOrdemServico>();
        }

        public int AtividadeOSId { get; set; }
        public string NomeAtvOs { get; set; }
        public ICollection<FormularioOrdemServico> FormularioOrdemServicos { get; set; }
    }
}