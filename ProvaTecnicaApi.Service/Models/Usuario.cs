using System.ComponentModel.DataAnnotations;

namespace ProvaTecnicaApi.Service.Models
{
    public class Usuario
    {
        public Usuario()
        {

        }
        [Key]
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
