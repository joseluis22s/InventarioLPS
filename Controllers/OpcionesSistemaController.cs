using Microsoft.AspNetCore.Mvc;

namespace InventarioLPS.Controllers
{
    public class OpcionesSistemaController : Controller
    {
        // GET: OpcionesSistemaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OpcionesSistemaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OpcionesSistemaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OpcionesSistemaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OpcionesSistemaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OpcionesSistemaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OpcionesSistemaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OpcionesSistemaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
