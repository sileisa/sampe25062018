using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sampe
{
	public class CorPeca
	{
		[Key]
		public int CorPecaId { get; set; }
		public string NomeCorPeca { get; set; }
	}
}