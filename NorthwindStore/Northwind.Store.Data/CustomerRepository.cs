using Northwind.Store.Model;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Store.Data
{
    public class CustomerRepository : BaseRepository<Customer, string>
    {
        public CustomerRepository(NWContext context) : base(context) { }

        public override async Task<Customer> Get(string key)
        {
            var result = await _db.Customers
                //.Include(o => o.Region)
                .FirstOrDefaultAsync(m => m.CustomerId == key);

            return result;
        }
    }
}
