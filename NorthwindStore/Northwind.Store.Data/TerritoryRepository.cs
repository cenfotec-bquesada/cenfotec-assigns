using Northwind.Store.Model;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Store.Data
{
    public class TerritoryRepository : BaseRepository<Territory, string>
    {
        public TerritoryRepository(NWContext context) : base(context) { }

        public override async Task<Territory> Get(string key)
        {
            return await _db.Territories
                .Include(t => t.Region)
                .FirstOrDefaultAsync(m => m.TerritoryId == key);
        }
    }
}
