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
    public class TerritoryController : Controller
    {
        private readonly TerritoryRepository _repo;

        public TerritoryController(TerritoryRepository repo)
        {
            _repo = repo;
        }

        // GET: Territory
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetList());
        }

        // GET: Territory/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var territory = await _repo.Get(id);
            if (territory == null)
            {
                return NotFound();
            }

            return View(territory);
        }

        // GET: Territory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Territory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TerritoryId,TerritoryDescription,TerritoryId")] Territory territory)
        {
            if (ModelState.IsValid)
            {
                territory.State = Model.ModelState.Added;
                await _repo.Save(territory);
                return RedirectToAction(nameof(Index));
            }
            return View(territory);
        }

        // GET: Territory/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var territory = await _repo.Get(id);
            if (territory == null)
            {
                return NotFound();
            }
            return View(territory);
        }

        // POST: Territory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TerritoryId,TerritoryDescription,TerritoryId,RowVersion,ModifiedProperties")] Territory territory)
        {
            if (id != territory.TerritoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                territory.State = Model.ModelState.Modified;
                await _repo.Save(territory);

                return RedirectToAction(nameof(Index));
            }
            return View(territory);
        }

        // GET: Territory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var territory = await _repo.Get(id);

            if (territory == null)
            {
                return NotFound();
            }

            return View(territory);
        }

        // POST: Territory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
