using System.ComponentModel.DataAnnotations;

namespace WebbApp.Models.Entities.Extra;

public class InvoiceEntity
{
    [Key]
    public int InvoiceId { get; set; }

}
