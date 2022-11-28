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
    public class RegionController : Controller
    {
        private readonly BaseRepository<Region, int> _repo;

        public RegionController(BaseRepository<Region, int> repo)
        {
            _repo = repo;
        }

        // GET: Region
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetList());
        }

        // GET: Region/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _repo.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Region/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Region/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionId,RegionDescription")] Region region)
        {
            if (ModelState.IsValid)
            {
                region.State = Model.ModelState.Added;
                await _repo.Save(region);
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Region/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _repo.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // POST: Region/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,RegionDescription,RowVersion,ModifiedProperties")] Region region)
        {
            if (id != region.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                region.State = Model.ModelState.Modified;
                await _repo.Save(region);

                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Region/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _repo.Get(id.Value);

            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
