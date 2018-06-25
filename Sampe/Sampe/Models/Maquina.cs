using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sampe
{
    public class Maquina
    {
        [Key]
        public int MaquinaId { get; set; }

        [Required(ErrorMessage = "Preencha este campo")]
        public string NomeMaquina { get; set; }
    }
}
