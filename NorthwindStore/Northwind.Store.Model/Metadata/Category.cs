using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
        public class CategoryMetadata
        {
            [Display(Name = "Nombre de Categoria")]
            public string CategoryName { get; set; } = null!;
            [Display(Name = "Descripcion")]
            [Required(ErrorMessage = "La {0} es requerida.")]
            [StringLength(32, MinimumLength = 4, ErrorMessage = "Se requiere entre {2} y {1} caracteres.")]
            public string? Description { get; set; }
            [Display(Name = "Imagen")]
            public byte[]? Picture { get; set; }
        }
    }
}
