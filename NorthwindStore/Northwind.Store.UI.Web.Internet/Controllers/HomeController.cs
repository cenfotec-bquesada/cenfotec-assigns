using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.UI.Web.Internet.Models;
using System.Diagnostics;
using X.PagedList;

namespace Northwind.Store.UI.Web.Internet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NWContext _context;
        private readonly SessionSettings _ss;

        public HomeController(ILogger<HomeController> logger, NWContext context, SessionSettings ss)
        {
            _logger = logger;
            _context = context;
            _ss = ss;
        }

        public IActionResult Index(int? page, string? filter)
        {
            var pageNumber = page ?? 1;
            var filterValue = filter ?? "";

            var products = _context.Products
                .Where(x => x.ProductName.Contains(filterValue))
                .ToPagedList(pageNumber, 4);

            var cartItems = _ss.CartTotalItems ?? "0";
            ViewBag.cartTotalItems = cartItems;
            ViewBag.filter = filterValue;

            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productList = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (productList == null)
            {
                return NotFound();
            }

            var cartItems = _ss.CartTotalItems ?? "0";
            ViewBag.cartTotalItems = cartItems;

            return View(productList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<FileStreamResult> ReadImage(int id)
        {
            FileStreamResult result = null;

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category != null)
            {
                var stream = new MemoryStream(category.Picture);

                if (stream != null)
                {
                    result = File(stream, "image/png");
                }
            }

            return result;
        }
    }
}