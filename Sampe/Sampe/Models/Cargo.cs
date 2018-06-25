using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sampe
{
    public class Cargo
    {
        [Key]
        public int CargoId { get; set; }

        [Required(ErrorMessage = "Preencha este campo")]
        public string NomeCargo { get; set; }
        
        
    }
}
