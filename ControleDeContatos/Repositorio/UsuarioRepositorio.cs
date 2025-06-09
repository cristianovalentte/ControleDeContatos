using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            this._bancoContext = bancoContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel BuscarPorEmailLogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            //Gravar no BD
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash(); // Gera o hash da senha antes de salvar
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception($"Erro ao atualizar o contato com ID {usuario.Id}.");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAlteracao = DateTime.Now;

            //Atualização da alteração no BD
            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }
        
        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if(usuarioDB == null)
                throw new Exception($"Erro ao apagar o contato com ID {id}.");

            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }

        
    }
}
