using ControleDeContatos.Enums;
using ControleDeContatos.Helper;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ? Login { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
        public string? Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
