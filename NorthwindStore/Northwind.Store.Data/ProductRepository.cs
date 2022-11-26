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
    public class ProductRepository : BaseRepository<Product, int>
    {
        public ProductRepository(NWContext context) : base(context) { }

        public override async Task<Product> Get(int key)
        {
            var result = await base.Get(key);

            return result;
        }

        public override async Task<IEnumerable<Product>> GetList(PageFilter pf = null)
        {
            var result = new List<Product>();

            if (pf == null)
            {
                result = await _db.Set<Product>()
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                pf.Count = await _db.Set<Product>().CountAsync();

                result = await _db.Set<Product>()
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .AsNoTracking()
                    .OrderBy(pf.Sorting)
                    .Skip((pf.Page - 1) * pf.PageSize)
                    .Take(pf.PageSize).ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Product>> Search(string filter, PageFilter pf)
        {
            var result = new List<Product>();

            return result;
        }

        /// <summary>
        /// Lee la imagen de base de datos como un MemoryStream.
        /// </summary>
        /// <example>
        /// Para utilizarse en una acción de un Controller de ASP.NET MVC
        /// public FileStreamResult ReadImage(int id)
        /// {
        ///    return File(pB.ReadImageStream(id), "image/jpg");
        /// } 
        /// </example>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MemoryStream> GetFileStream(int id)
        {
            MemoryStream result = null;

            var image = await _db.Products.Where(c => c.ProductId == id).
                Select(i => i.Picture).AsNoTracking().FirstOrDefaultAsync();

            if (image != null)
            {
                result = new MemoryStream(image);
            }

            return result;
        }

        /// <summary>
        /// Lee la imagen de base de datos como un string en Base64.
        /// </summary>
        /// <example>
        /// Para utilizarse directamente en una vista de razor. 
        /// </example>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFileBase64(int id)
        {
            string result = "";

            using (var ms = await GetFileStream(id))
            {
                if (ms != null)
                {
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    result = $"data:image/jpg;base64,{base64}";
                }
            }

            return result;
        }
    }
}
