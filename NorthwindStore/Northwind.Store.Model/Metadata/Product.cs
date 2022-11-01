using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        public class ProductMetadata
        {
            [Display(Name = "Nombre de Categoria")]
            public string ProductName { get; set; } = null!;

            [Display(Name = "Cantidad por unidad")]
            public string? QuantityPerUnit { get; set; }

            [Display(Name = "Precio del producto")]
            public decimal? UnitPrice { get; set; }


            [Display(Name = "Categoria del producto")]
            public virtual Category? Category { get; set; }

            [Display(Name = "Proveedor del producto")]
            public virtual Supplier? Supplier { get; set; }
        }
    }
}
