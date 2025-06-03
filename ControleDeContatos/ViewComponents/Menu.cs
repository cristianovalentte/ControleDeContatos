using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleDeContatos.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuario");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                return Content(""); // Ou crie uma View de menu genérico, se preferir
            }

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

            return View(usuario); // Passa o modelo para a view
        }
    }
}
