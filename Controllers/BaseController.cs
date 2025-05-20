using InventarioLPS.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventarioLPS.Controllers
{
    public class BaseController : Controller
    {
        // Muestra a ErrorPage.
        protected IActionResult ShowErrorPage(string error, string innerError)
        {
            var errorModel = new ErrorViewModel
            {
                Message = error,
                InnerMessage = innerError
            };
            return View("~/Views/Shared/Error.cshtml", errorModel);
        }


        // Redirige a ErrorPage.
        protected IActionResult ShowErrorPageFromException(Exception ex)
        {
            var error = $"ERROR: {ex.Message}";
            var innerError = $"ERROR-INTERNO: {ex.InnerException?.Message ?? "No hay información interna."}";
            Console.WriteLine($"{error}\n{innerError}");
            return ShowErrorPage(error, innerError);
        }
    }

}
