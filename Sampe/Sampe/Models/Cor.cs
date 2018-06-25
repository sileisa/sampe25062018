using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sampe
{
	public class Cor
	{
		[Key]
		public int CorId { get; set; }
		public string NomeCor { get; set; }
	}
}