using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaFacilCoreV2.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Verifica se o usuário está logado
            if (context.HttpContext.Session.GetString("NomeUsuario") == null)
            {
                // Se não estiver logado, redireciona para a tela de login
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
