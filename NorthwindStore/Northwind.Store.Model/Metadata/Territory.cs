using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(TerritoryMetadata))]
    public partial class Territory : ModelBase
    {
    public class TerritoryMetadata
    {
    }
    }

}
