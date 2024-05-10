namespace Test.Models;

public class BooksEditionDTO
{
    public class BookEditionsDto
    {
        public List<BookEditionDto> BookEditions { get; set; } = new();
    }

    public class BookEditionDto
    {
        public int PK { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string EditionTitle { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
    }
}