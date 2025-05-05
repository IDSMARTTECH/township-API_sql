using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Township_API.Controllers
{
    public class TestModuleController : Controller
    {
        // GET: TestModuleController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TestModuleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestModuleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestModuleController/Create
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

        // GET: TestModuleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestModuleController/Edit/5
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

        // GET: TestModuleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestModuleController/Delete/5
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
