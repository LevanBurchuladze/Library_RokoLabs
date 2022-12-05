
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Repository
{
    public interface IBookRepository
    {
        int InsertBook(BookDto bookDto);
        List<BookDto> Getbooks();
        List<BookDto> GetbooksByName(string title);
        List<BookDto> GetBooksByPubHouse(string pubHouse);
        List<BookDto> GetBooksByAuthor(Author newAuthor);
        int GetEqualsBook(BookDto bookDto);
        BookDto GetBookById(int EditionId);
        int UpdateBook(BookDto bookDto);
    }
}
