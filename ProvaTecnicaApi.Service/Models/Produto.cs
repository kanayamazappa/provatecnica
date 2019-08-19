using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProvaTecnicaApi.Service.Models
{
    public class Produto
    {
        public Produto()
        {
            
        }

        [Key]
        public int IdProduto { get; set; }

        public Categoria Categoria { get; set; }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
