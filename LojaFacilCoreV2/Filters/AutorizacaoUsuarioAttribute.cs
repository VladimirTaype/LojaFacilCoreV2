using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaFacilCoreV2.Filters
{
    // 🔹 Filtro que exige que o usuário esteja logado
    public class AutorizacaoUsuarioAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Verifica se há usuário logado na sessão
            var usuarioLogado = context.HttpContext.Session.GetString("NomeUsuario");

            if (string.IsNullOrEmpty(usuarioLogado))
            {
                // Redireciona corretamente para a tela de login do AuthController
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
