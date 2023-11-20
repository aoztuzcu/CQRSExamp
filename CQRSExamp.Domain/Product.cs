using CQRSExamp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CQRSExamp.Domain;

public class Product : BaseEntity
{
    [Required]
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }

}
