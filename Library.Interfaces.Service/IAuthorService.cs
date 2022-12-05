
using Library.Dto;
using Library.Entities;
using System.Collections.Generic;

namespace Library.Interfaces.Service
{
    public interface IAuthorService
    {
        List<AuthorDto> GetAuthors();
        int InsertAuthor(Author authorDto);
        void UpdateAuthorsBook(int bookId, List<Author> newAuthors, List<Author> deleteAuthors);
        void UpdateAuthorsPatent(int patentId, List<Author> newAuthors, List<Author> deleteAuthors);
    }
}
