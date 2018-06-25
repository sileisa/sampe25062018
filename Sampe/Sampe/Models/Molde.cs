using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sampe
{
    public class Molde
    {
        [Key]
        public int MoldeId { get; set; }

        [Required(ErrorMessage = "Preencha este campo")]
        public string NomeMolde { get; set; }

        public int? Cavidade { get; set; }

    }
}
