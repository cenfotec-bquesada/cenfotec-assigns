using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product : ModelBase
    {
        public class ProductMetadata
        {
            [Display(Name = "Nombre de Producto")]
            [Required(ErrorMessage = "El {0} es requerido.")]
            [StringLength(40, MinimumLength = 4, ErrorMessage = "Se requiere entre {2} y {1} caracteres.")]
            public string ProductName { get; set; } = null!;

            [Display(Name = "Cantidad por unidad")]
            public string? QuantityPerUnit { get; set; }

            [Display(Name = "Precio Unitario")]
            [Required(ErrorMessage = "El {0} es requerido.")]
            public decimal? UnitPrice { get; set; }


            [Display(Name = "Categoria del producto")]
            public virtual Category? Category { get; set; }

            [Display(Name = "Proveedor del producto")]
            public virtual Supplier? Supplier { get; set; }
        }

        [NotMapped]
        public string PictureBase64
        {
            get
            {
                var result = "";
                if (Picture != null)
                {
                    var base64 = Convert.ToBase64String(Picture);
                    result = $"data:image/jpg;base64,{base64}";
                }
                return result;
            }
        }

        // ALTER TABLE [Northwind].[dbo].products ADD [Picture] [image]
        public byte[]? Picture { get; set; }
    }
}
