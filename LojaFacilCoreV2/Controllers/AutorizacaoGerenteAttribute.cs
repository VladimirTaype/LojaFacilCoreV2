using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaFacilCoreV2.Controllers
{
    // Este atributo verifica se o usuário logado é um Gerente
    public class AutorizacaoGerenteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tipoUsuario = context.HttpContext.Session.GetString("TipoUsuario");

            if (tipoUsuario != "Gerente")
            {
                // Redireciona o usuário comum de volta para a página inicial de produtos
                context.Result = new RedirectToActionResult("Index", "Produtos", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
