using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class books_authors
{
    [Required] public int FK_book { get; set; }
    [Required] public int FK_author { get; set; }
}