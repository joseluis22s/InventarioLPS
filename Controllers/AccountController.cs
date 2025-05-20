using System.Security.Claims;
using InventarioLPS.Data;
using InventarioLPS.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioLPS.Controllers
{
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AccountController : BaseController
    {
        private readonly InventarioLPSContext _context;

        public AccountController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET:
        // Muestra pantalla de login.
        public IActionResult Index()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        // POST: Iniciar sesión con las credenicales dadas.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("NombreUsuario,Contrasena")] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            try
            {
                var res = await _context.Usuario.Include(u => u.IdPerfilNavigation.PerfilPermisos)
                    .FirstOrDefaultAsync(u =>
                    u.NombreUsuario == usuario.NombreUsuario &&
                    u.Contrasena == usuario.Contrasena);

                if (res is null)
                {
                    ModelState.AddModelError("", "Credenciales incorrectas.");
                    return View(usuario);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, res.Id.ToString()),
                    new Claim(ClaimTypes.Name, res.NombreUsuario)
                };
                foreach (var perfilPermiso in res.IdPerfilNavigation.PerfilPermisos)
                {
                    claims.Add(new Claim("Permiso", perfilPermiso.IdPermisoNavigation.Nombre));
                }

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", principal, new AuthenticationProperties
                {
                    IsPersistent = true
                });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return ShowErrorPageFromException(ex);
            }
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Account");

        }
    }
}
