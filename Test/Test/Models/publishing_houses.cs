using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class publishing_houses
{
    [Required] public int PK { get; set; }
    [Required] [MaxLength(100)]  public string name { get; set; }
    [MaxLength(50)] public string owner_first_na { get; set; }
    [Required] [MaxLength(100)] public string owner_last_na { get; set; }
}