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
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao )
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
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

                        ViewBag.MensagemErro = "Enviamos para o seu e-mail cadastrado, uma nova senha.";
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
