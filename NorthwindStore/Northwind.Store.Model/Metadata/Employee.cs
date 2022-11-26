using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(EmployeeMetadata))]
    public partial class Employee : ModelBase
    {
        public class EmployeeMetadata
        {
        }
    }

}
