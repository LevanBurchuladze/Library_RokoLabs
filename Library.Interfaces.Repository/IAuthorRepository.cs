
using Library.Dto;
using System.Collections.Generic;

namespace Library.Interfaces.Repository
{
    public interface IAuthorRepository
    {
        int FindAuthor(AuthorDto authorDto);
        int InsertAuthor(AuthorDto authorDto);
        void InsertAuthorsBooks(int bookId, int tempAuthorid);
        void InsertAuthorsPatents(int bookId, int tempAuthorid);
        List<AuthorDto> GetAuthorByBookIds(IEnumerable<int> ids);
        List<AuthorDto> GetAuthorByPatentIds(IEnumerable<int> ids);
        List<AuthorDto> GetAuthors();
        void UpdateAuthorsBook(int bookId, int authorId);
        void DeleteAuthorsBook(int bookId, int authorId);
        void UpdateAuthorsPatent(int patentId, int authorId);
        void DeleteAuthorsPatent(int patentId, int authorId);
    }
}
