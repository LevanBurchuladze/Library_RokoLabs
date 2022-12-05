
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Service
{
    public interface IBookService
    {
        Book GetBookById(int id);
        List<Book> Getbooks();
        int InsertBook(BookDto bookDto);
        List<BookDto> GetbooksByName(string title);
        List<BookDto> GetBooksByPubHouse(string pubHouse);
        List<BookDto> GetBooksByAuthor(Author newAuthor);
        int GetEqualsBook(BookDto bookDto);
        int UpdateBook(BookDto bookDto);
    }
}
