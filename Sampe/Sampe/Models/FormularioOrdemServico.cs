using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Sampe.Models;

namespace Sampe
{
    public class FormularioOrdemServico
    {
        public FormularioOrdemServico()
        {
            this.AtividadesOs = new HashSet<AtividadeOS>();
            this.FormularioOSAtividades = new HashSet<FormularioOSAtividade>();
        }
        public int FormularioOrdemServicoId { get; set; }
        public string TipoManutencao { get; set; } 

        public string HoraInicio { get; set; }
        public string HoraFinal { get; set; }
        public DateTime Dt { get; set; }

        public Boolean Intervalo { get; set; }
        public string IntervaloInicio { get; set; }
        public string IntervaloFim { get; set; }
        public string ObsIntervalo { get; set; }
        public string MaterialUsado { get; set; }
        public int? QuantUsado { get; set; }
        public string MaterialSobrante { get; set; }
        public int? QuantSobrante { get; set; }
        public bool Status { get; set; }

        public String Supervisor { get; set; }

        [ForeignKey("Maquina")]
        public int MaquinaId { get; set; }
        public Maquina Maquina { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<AtividadeOS> AtividadesOs { get; set; }
        public ICollection<int> AtividadeOsId { get; set; }

        public ICollection<FormularioOSAtividade> FormularioOSAtividades { get; set; }
        public ICollection<int> FormularioOSAtividadeId { get; set; }
    }
}
