using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace ControleDeContatos.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;
        private Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public void CriarSessaoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);

            _httpContext.HttpContext.Session.SetString("SessaoUsuarioLogado", valor);
        }
        public void EncerrarSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("SessaoUsuarioLogado");
        }
        public UsuarioModel BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("SessaoUsuarioLogado");

            if(string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            }
        }
    }
}
