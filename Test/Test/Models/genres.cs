using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class genres
{
    [Required] public int PK { get; set; }
    [Required] [MaxLength(100)] public string nam { get; set; }
}