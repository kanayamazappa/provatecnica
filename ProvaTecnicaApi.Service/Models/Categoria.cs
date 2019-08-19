using System;
using System.ComponentModel.DataAnnotations;

namespace ProvaTecnicaApi.Service.Models
{
    public class Categoria
    {
        public Categoria()
        {

        }

        [Key]
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
