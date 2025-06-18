using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        [Compare("NovaSenha", ErrorMessage = "A nova senha e a confirmação não coincidem.")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
