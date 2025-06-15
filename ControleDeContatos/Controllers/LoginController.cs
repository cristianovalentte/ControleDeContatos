using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email )
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            //Se o usuário já estiver logado, redireciona para a página inicial
            if(_sessao.BuscarSessaoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult RedefinirSenha()
        {
            return View();
        }
        public IActionResult Sair()
        {
            _sessao.EncerrarSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null && usuario.SenhaValida(loginModel.Senha))
                    {
                        _sessao.CriarSessaoUsuario(usuario);
                        return RedirectToAction("Index", "Home");
                    }

                    ViewBag.MensagemErro = "Login ou senha inválidos.";
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = $"Erro ao tentar fazer login: {ex.Message}";
                return View("Index");
            }
        }
        [HttpPost] 
        public IActionResult EnivarLinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Olá {usuario.Nome}, sua nova senha é: {novaSenha}. ";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            ViewBag.MensagemErro = "Enviamos para o seu e-mail cadastrado, uma nova senha.";
                        }
                        else
                        {
                            ViewBag.MensagemErro = "Ops, não conseguimos enviar o e-mail com a nova senha. Tente novamente.";
                        }
                            return RedirectToAction("Index", "Login");
                    }

                    ViewBag.MensagemErro = "Ops, não conseguimos redefinir sua senha! Tente novamente.";
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = $"Não conseguimos redefinir sua senha, tente novamente, detalhe do erro: {ex.Message}";
                return View("Index");
            }
        }

    }
}
