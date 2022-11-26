using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(ShipperMetadata))]
    public partial class Shipper : ModelBase
    {
    public class ShipperMetadata
    {
    }
    }

}
