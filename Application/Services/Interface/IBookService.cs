using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Services.Interface
{
    public interface IBookService
    {
        Task<BookModel> CreateBook(BookModel book);
        Task<bool> DeleteBook(string id);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBook(string id);
        Task<List<BookModel>> GetBooksByCategory(string category);
        Task<BookModel> UpdateBook(BookModel book);
        Task<List<BookModel>> GetAllFeaturedBooks();
        List<BookModel> GetBooksByIds(List<string> booksIds);
    }
}
