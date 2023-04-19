using System.ComponentModel.DataAnnotations;

namespace WebbApp.Models.Entities;

public class InvoiceEntity
{
    [Key]
    public int InvoiceId { get; set; }

}
