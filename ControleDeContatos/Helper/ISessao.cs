using ControleDeContatos.Models;

namespace ControleDeContatos.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(UsuarioModel usuario);
        void EncerrarSessaoUsuario();
        UsuarioModel BuscarSessaoUsuario();
    }
}
