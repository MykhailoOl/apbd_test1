using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class books_editions
{
    [Required] public int PK { get; set; }
    [Required] public int FK_publishing_house { get; set; }
    [Required] public int FK_book { get; set; }
    [Required] [MaxLength(100)] public string edition_title { get; set; }
    [Required] public DateTime release_date { get; set; }
}