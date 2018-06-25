using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
	public class ControleDeQualidadeCopo
	{
		[Key]
		public int ControleDeQualidadeCopoId { get; set; }
		public Double Ciclo { get; set; }
		public String Hora { get; set; }
		public Double PesoDaPeca { get; set; }
		public Double PesoDaPeca2 { get; set; }
		public Boolean Peso { get; set; }
		public Boolean Cor { get; set; }
		public Boolean Dimensao { get; set; }
		public String Assinatura { get; set; }
		public Boolean Liberado { get; set; }
		[ForeignKey("OrdemProducaoCopo")]
		public String OrdemProducaoCopoId { get; set; }
		public OrdemProducaoCopo OrdemProducaoCopo { get; set; }

		[ForeignKey("Usuario")]
		public int UsuarioId { get; set; }
		public Usuario Usuario { get; set; }


		//Se todos os campos de inspeção estiverem 
		//preenchidos retorna true para liberar o formulário 
		public Boolean ValidaInspecao()
		{
			if (Peso && Cor && Dimensao)
				return true;

			return false;
		}
	}
}