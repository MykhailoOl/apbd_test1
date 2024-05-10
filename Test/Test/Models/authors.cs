using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Test.Models;

public class authors
{
    [Required] public int PK { get; set; }
    [MaxLength(50)]  public string first_na { get; set; }
    [Required] [MaxLength(100)] public string last_na { get; set; }
}