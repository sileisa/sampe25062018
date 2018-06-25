using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sampe
{
    public class Login
    {
        public int LoginId { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public string User { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public string Senha { get; set; }
    }
}