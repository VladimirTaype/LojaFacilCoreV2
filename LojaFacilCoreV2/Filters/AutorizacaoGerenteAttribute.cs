using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaFacilCoreV2.Filters
{
    // 🔹 Este filtro exige login E cargo de "Gerente"
    public class AutorizacaoGerenteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuarioLogado = context.HttpContext.Session.GetString("UsuarioLogado");
            var cargo = context.HttpContext.Session.GetString("CargoUsuario");

            if (string.IsNullOrEmpty(usuarioLogado))
            {
                context.Result = new RedirectToActionResult("Login", "Usuarios", null);
                return;
            }

            if (cargo != "Gerente")
            {
                context.Result = new RedirectToActionResult("AcessoNegado", "Home", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
