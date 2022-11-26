using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(RegionMetadata))]
    public partial class Region : ModelBase
    {
        public class RegionMetadata
        {
        }
    }
}
