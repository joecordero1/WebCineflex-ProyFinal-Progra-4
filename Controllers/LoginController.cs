using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebCineflex.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace WebCineflex.Controllers

{
    public class LoginController : Controller
    {
        private readonly UsuariosCineflexContext _dbContext;

        public LoginController(UsuariosCineflexContext dbContext) // Reemplaza "YourDbContext" con tu propio contexto de base de datos
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String username, String password)
        {
            // Buscar al usuario en la base de datos según el nombre de usuario ingresado

            var user = await _dbContext.Usuarios.SingleOrDefaultAsync(u => u.NombreUsuario == username && u.Contrasenia == password);

            if (user != null)
            {
                // Iniciar sesión guardando el nombre de usuario en la sesión

                return RedirectToAction("Index", "Peliculas");
            }

            // Si la autenticación falla, se agrega un mensaje de error en la vista de inicio de sesión
            ModelState.AddModelError(string.Empty, "Credenciales inválidas");
            return View();
        }

    }
}

