using Microsoft.Data.SqlClient;
using Test.Models;

namespace Test.Repository;

public class books_repository
{
    private readonly IConfiguration _configuration;

    public books_repository (IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<bool> DoesBookExist(int PK)
    {
        var query = "SELECT 1 FROM books WHERE PK = @PK";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@PK", PK);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();
        
        return res is not null;
    }

    public async Task<object> GetBook(int PK)
    {
        var query = "SELECT books_editions.PK as id,books.title as bookTitle,books_editions.edition_title as editionTitle,publishing_houses.name as publishingHouseName, books_editions.release_date as releaseDate FROM books JOIN books_editions ON books.PK = books_editions.FK_book JOIN publishing_houses ON publishing_houses.PK =  books_editions.FK_publishing_house WHERE books.PK = @PK";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@PK", PK);
        
        await connection.OpenAsync();
        
        await using var reader = await command.ExecuteReaderAsync();

        var bookEditions = new BooksEditionDTO.BookEditionsDto();

        var idOrdinal = reader.GetOrdinal("id");
        var bookTitleOrdinal = reader.GetOrdinal("bookTitle");
        var editionTitleOrdinal = reader.GetOrdinal("editionTitle");
        var publisherOrdinal = reader.GetOrdinal("publishingHouseName");
        var releaseDateOrdinal = reader.GetOrdinal("releaseDate");

        while (await reader.ReadAsync())
        {
            bookEditions.BookEditions.Add(new BooksEditionDTO.BookEditionDto
            {
                PK = reader.GetInt32(idOrdinal),
                BookTitle = reader.GetString(bookTitleOrdinal),
                EditionTitle = reader.GetString(editionTitleOrdinal),
                Publisher = reader.GetString(publisherOrdinal),
                ReleaseDate = reader.GetDateTime(releaseDateOrdinal)
            });
        }

        if (bookEditions.BookEditions.Count == 0)
            throw new Exception("No book found");

        return bookEditions;
    }

public async void AddBook(string bookTitle,string editionTitle,int publishingHouseId,DateTime releaseDate)
    {
        var query = "INSERT INTO books (PK,title) VALUES ((SELECT MAX( customer_id ) FROM customers) + 1,@bookTitle)";
        var query2 = "INSERT INTO books_editions (PK,FK_publishing_house,FK_book,edition_title,release_date) VALUES ((SELECT MAX( customer_id ) FROM customers) + 1,@publishingHouseId,(SELECT PK FROM books WHERE title = @bookTitle2),@editionTitle,@releaseDate)";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();
        using SqlCommand command2 = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@bookTitle", bookTitle);

        command2.Connection = connection;
        command2.CommandText = query2;
        command.Parameters.AddWithValue("@bookTitle2", bookTitle);
        command.Parameters.AddWithValue("@editionTitle", editionTitle);
        command.Parameters.AddWithValue("@publishingHouseId", publishingHouseId);
        command.Parameters.AddWithValue("@releaseDate", releaseDate);
        
        await connection.OpenAsync();
        await command.ExecuteScalarAsync();
        await command2.ExecuteScalarAsync();
    
    }

    public async Task<object> GetBook(string title)
    {
        var query = "SELECT books_editions.PK as id,books.title as bookTitle,books_editions.edition_title as editionTitle,publishing_houses.name as publishingHouseName, books_editions.release_date as releaseDate FROM books JOIN books_editions ON books.PK = books_editions.FK_book JOIN publishing_houses ON publishing_houses.PK =  books_editions.FK_publishing_house WHERE books.title = @title";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@title", title);
        
        await connection.OpenAsync();
        
        var bookEditions = new BooksEditionDTO.BookEditionsDto();

        await using var reader = await command.ExecuteReaderAsync();
        
        var idOrdinal = reader.GetOrdinal("id");
        var bookTitleOrdinal = reader.GetOrdinal("bookTitle");
        var editionTitleOrdinal = reader.GetOrdinal("editionTitle");
        var publisherOrdinal = reader.GetOrdinal("publishingHouseName");
        var releaseDateOrdinal = reader.GetOrdinal("releaseDate");

        while (await reader.ReadAsync())
        {
            bookEditions.BookEditions.Add(new BooksEditionDTO.BookEditionDto
            {
                PK = reader.GetInt32(idOrdinal),
                BookTitle = reader.GetString(bookTitleOrdinal),
                EditionTitle = reader.GetString(editionTitleOrdinal),
                Publisher = reader.GetString(publisherOrdinal),
                ReleaseDate = reader.GetDateTime(releaseDateOrdinal)
            });
        }

        if (bookEditions.BookEditions.Count == 0)
            throw new Exception("No book found");

        return bookEditions;
    }
}