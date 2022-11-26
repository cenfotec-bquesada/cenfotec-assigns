using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(SupplierMetadata))]
    public partial class Supplier : ModelBase
    {
    public class SupplierMetadata
    {
    }
    }

}
