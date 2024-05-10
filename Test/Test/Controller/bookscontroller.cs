using Microsoft.AspNetCore.Mvc;
using Test.Repository;

namespace Test.Controller;

[ApiController]
[Route("api/"+"[controller]")]
public class bookscontroller : ControllerBase
{
    private readonly books_repository _booksRepository;

    public bookscontroller(books_repository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int PK)
    {
        if (!await _booksRepository.DoesBookExist(PK))
            return NotFound();
        var res = await _booksRepository.GetBook(PK);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Post(string bookTitle,string editionTitle,int publishingHouseId,DateTime releaseDate)
    {
        _booksRepository.AddBook(bookTitle, editionTitle, publishingHouseId, releaseDate);
        var res = await _booksRepository.GetBook(bookTitle);
        return Created("",res);
    }
}