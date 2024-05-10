using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class books
{
    [Required] public int PK { get; set; }
    [Required] [MaxLength(100)]  public string title { get; set; }
}