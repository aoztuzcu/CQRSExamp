using System.ComponentModel.DataAnnotations;

namespace CQRSExamp.Domain.Common;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
