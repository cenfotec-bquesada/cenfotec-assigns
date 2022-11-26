using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(CustomerMetadata))]
    public partial class Customer : ModelBase
    {
        public class CustomerMetadata
        {
        }
    }
}
