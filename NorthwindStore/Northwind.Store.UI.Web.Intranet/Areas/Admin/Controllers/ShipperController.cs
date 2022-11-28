using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.Model;

namespace Northwind.Store.UI.Web.Intranet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShipperController : Controller
    {
        private readonly BaseRepository<Shipper, int> _repo;

        public ShipperController(BaseRepository<Shipper, int> repo)
        {
            _repo = repo;
        }

        // GET: Shipper
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetList());
        }

        // GET: Shipper/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipper = await _repo.Get(id.Value);
            if (shipper == null)
            {
                return NotFound();
            }

            return View(shipper);
        }

        // GET: Shipper/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shipper/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipperId,CompanyName,Phone")] Shipper shipper)
        {
            if (ModelState.IsValid)
            {
                shipper.State = Model.ModelState.Added;
                await _repo.Save(shipper);
                return RedirectToAction(nameof(Index));
            }
            return View(shipper);
        }

        // GET: Shipper/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipper = await _repo.Get(id.Value);
            if (shipper == null)
            {
                return NotFound();
            }
            return View(shipper);
        }

        // POST: Shipper/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipperId,CompanyName,Phone,RowVersion,ModifiedProperties")] Shipper shipper)
        {
            if (id != shipper.ShipperId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                shipper.State = Model.ModelState.Modified;
                await _repo.Save(shipper);

                return RedirectToAction(nameof(Index));
            }
            return View(shipper);
        }

        // GET: Shipper/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipper = await _repo.Get(id.Value);

            if (shipper == null)
            {
                return NotFound();
            }

            return View(shipper);
        }

        // POST: Shipper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
