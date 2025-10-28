using LojaFacilCoreV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace LojaFacilCoreV2.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.SenhaHash == senha);

            if (usuario != null)
            {
                // Salva informações na sessão
                HttpContext.Session.SetString("NomeUsuario", usuario.Nome);
                HttpContext.Session.SetString("TipoUsuario", usuario.TipoUsuario);
                HttpContext.Session.SetString("EmailUsuario", usuario.Email);

                // Redireciona para página principal (Produtos)
                return RedirectToAction("Index", "Produtos");
            }

            // Caso falhe o login
            ViewBag.Erro = "E-mail ou senha incorretos!";
            return View();
        }

        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // limpa a sessão
            return RedirectToAction("Login");
        }
    }
}
