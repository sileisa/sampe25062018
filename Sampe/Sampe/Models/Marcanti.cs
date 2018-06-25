using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
    public class Marcanti
    {
        [Key]
        public int MarcantiId { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public String NomeEmpresa { get; set; }        
        public string Cnpj { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public int Cep { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public String Uf { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public String Cidade { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public String Rua { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public String Bairro { get; set; }
        
        public String Complemento { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public int Numero { get; set; }
       
        public String Telefone { get; set; }
        [Required(ErrorMessage = "Preencha este campo")]
        public String Email { get; set; }
        
    }
}