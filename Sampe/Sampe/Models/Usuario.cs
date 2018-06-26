using Sampe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sampe
{
    public class Usuario
    {
        [Key]
        [Column(Order = 1)]
        public int UsuarioId { get; set; }

        [Column(Order = 2)]
        [Required(ErrorMessage = "Preencha este campo")]
        public string NomeUsuario { get; set; }

        [Column(Order = 3)]
        [Required(ErrorMessage = "Preencha este campo")]
        public string SobrenomeUsuario { get; set; }

        [Required(ErrorMessage = "Preencha este campo")]
        [Column(Order = 4)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Preencha este campo")]
        [Column(Order = 5)]
        public String Senha { get; set; }

        [Column(Order = 6)]
        public String Hierarquia { get; set; }

        [Column(Order = 7)]
        [ForeignKey("Cargo")]
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

    }
}
