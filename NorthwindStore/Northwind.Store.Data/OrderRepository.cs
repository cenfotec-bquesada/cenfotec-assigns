using System;
using System.Collections.Generic;
using Northwind.Store.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Northwind.Store.Data
{
    public class OrderRepository : BaseRepository<Order, int>
    {
        public OrderRepository(NWContext context) : base(context) { }

        public override async Task<Order> Get(int key)
        {
            var result = await _db.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == key);

            return result;
        }

        public override async Task<IEnumerable<Order>> GetList(PageFilter pf = null)
        {
            var result = new List<Order>();

            if (pf == null)
            {
                result = await _db.Set<Order>()
                    .Include(o => o.OrderDetails)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                pf.Count = await _db.Set<Order>().CountAsync();

                result = await _db.Set<Order>()
                    .Include(o => o.OrderDetails)
                    .AsNoTracking()
                    .OrderBy(pf.Sorting)
                    .Skip((pf.Page - 1) * pf.PageSize)
                    .Take(pf.PageSize).ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Order>> Search(string filter, PageFilter pf)
        {
            var result = new List<Order>();

            return result;
        }
    }
}
