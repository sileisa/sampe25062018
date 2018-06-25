using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
    public class Expectativa
      //Equivalente ao "controle de produção" no formulário
    {
        [Key]
        public int ExpectativaId { get; set; }
        public String Produto { get; set; }
        public int CavidadeMolde { get; set; }
        public double PesoPecaAproximado { get; set; }
        public double PesoPecaCompleta { get; set; }
        public float Ciclo { get; set; }
        public int ProducaoEsperada { get; set; }
        //Horario de inicio e fim da produção
        public String ProdInicio { get; set; }
        public String ProdFim { get; set; }
    }
}