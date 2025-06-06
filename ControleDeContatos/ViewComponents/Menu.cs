using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleDeContatos.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("SessaoUsuarioLogado");

            UsuarioModel usuario = null;

            if (!string.IsNullOrEmpty(sessaoUsuario))
            {
                usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            }

            return View("Default", usuario);
        }
    }

}
