using Microsoft.AspNetCore.Mvc;
using Northwind.Store.Data;
using Northwind.Store.Model;

namespace Northwind.Store.UI.Web.Internet.Controllers
{
    public class CartController : Controller
    {
        private readonly NWContext _db;
        private readonly SessionSettings _ss;
        private readonly RequestSettings _rs;

        public CartController(NWContext db, SessionSettings ss)
        {
            _db = db;
            _ss = ss;
            _rs = new RequestSettings(this);
        }

        public IActionResult Index()
        {
            var productId = TempData[nameof(Product.ProductId)];

            ViewBag.productAdded = _rs.ProductAdded;
            var cartItems = _ss.CartTotalItems ?? "0";
            ViewBag.cartTotalItems = cartItems;

            return View(_ss.Cart);
        }

        public ActionResult Add(int? id)
        {

            if (id.HasValue)
            {
                #region Session
                var cart = _ss.Cart;

                if (!cart.Items.Any(i => i.ProductId == id))
                {
                    var p = _db.Products.Find(id);
                    cart.Items.Add(p);
                    _ss.Cart = cart;

                    _ss.CartTotalItems = cart.Items.Count().ToString();
                    ViewBag.cartTotalItems = _ss.CartTotalItems;

                    #region TempData
                    TempData[nameof(Product.ProductId)] = p.ProductId;
                    TempData[nameof(Product.ProductName)] = p.ProductName;
                    TempData[nameof(Product.UnitPrice)] = 1;
                    TempData[nameof(Product.QuantityPerUnit)] = 1;

                    _rs.ProductAdded = p;
                    #endregion
                }
                #endregion
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Buy()
        {
            var order = new Order();

            if (ModelState.IsValid)
            {
                // Create an order
                _db.Orders.Add(order);
                await _db.SaveChangesAsync();

                // Create order details based on cart products
                var cart = _ss.Cart;
                foreach(var item in cart.Items)
                {
                    var product = new OrderDetail();

                    product.OrderId = order.OrderId;
                    product.ProductId = item.ProductId;
                    product.UnitPrice = (decimal)item.UnitPrice;
                    product.Quantity = 1;
                    product.Discount = 0;

                    _db.OrderDetails.Add(product);
                    await _db.SaveChangesAsync();
                }
            }

            HttpContext.Session.Remove("Cart");

            HttpContext.Session.Remove("CartTotalItems");
            ViewBag.cartTotalItems = _ss.CartTotalItems;

            return RedirectToAction("Index");
        }
    }
}