using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
	public class ParadaCopo
	{
        [Key]
		public int ParadaId { get; set; }
		public String HoraParada { get; set; }
		public String HoraRetorno { get; set; }
		public String Motivo { get; set; }
		public String Observacoes { get; set; }
		[ForeignKey("OrdemProducaoCopo")]
		public String OrdemProducaoCopoId { get; set; }
		public OrdemProducaoCopo OrdemProducaoCopo { get; set; }
	}
}