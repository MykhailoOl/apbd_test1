using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class books_genres
{
    [Required] public int FK_book { get; set; }
    [Required] public int FK_genre { get; set; }
}