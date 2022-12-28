using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Services.Interface;
using Domain.Entities;
using MongoDB.Driver;
using Persistence.Context.Interface;

namespace Application.Services.Behaviour
{
    public class BookService : IBookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IDbClient dbClient)
        {
            _books = dbClient.GetAllBooks();
        }

        public async Task<BookModel> CreateBook(BookModel book)
        {
            var bookToAdd = new Book
            {
                Authors = book.Authors,
                Category = (Category)(Int32.Parse(book.Category)),
                Description = book.Description,
                Price = book.Price,
                IsFeatured = book.IsFeatured,
                Title = book.Title,
                BookUrl = book.BookUrl,
                ShortDescription = book.ShortDescription
            };

            await _books.InsertOneAsync(bookToAdd);
            book.Id = bookToAdd.Id;

            return book;
        }

        public async Task<bool> DeleteBook(string id)
        {
            var result = await _books.DeleteOneAsync(x => x.Id == id);

            return result.IsAcknowledged;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();

            var booksFromDb = await _books.Find(book => true).ToListAsync();

            foreach (var book in booksFromDb)
            {
                books.Add(new BookModel
                {
                    Authors = book.Authors,
                    Id = book.Id,
                    Category = book.Category.ToString(),
                    Description = book.Description,
                    Price = book.Price,
                    Title = book.Title,
                    BookUrl = book.BookUrl,
                    IsFeatured = book.IsFeatured,
                    ShortDescription = book.ShortDescription
                });
            }

            return books;
        }

        public async Task<List<BookModel>> GetAllFeaturedBooks()
        {
            List<BookModel> books = new List<BookModel>();

            var booksFromDb = await _books.Find(book => book.IsFeatured).ToListAsync();

            foreach (var book in booksFromDb)
            {
                books.Add(new BookModel
                {
                    Authors = book.Authors,
                    Id = book.Id,
                    Category = book.Category.ToString(),
                    Description = book.Description,
                    Price = book.Price,
                    Title = book.Title,
                    BookUrl = book.BookUrl,
                    IsFeatured = book.IsFeatured,
                    ShortDescription = book.ShortDescription
                });
            }

            return books;
        }

        public async Task<BookModel> GetBook(string id)
        {
            var bookFromDb = await _books.Find(book => book.Id == id).FirstOrDefaultAsync();

            if (bookFromDb == null) return new BookModel();

            return new BookModel
            {
                Id = bookFromDb.Id,
                Authors = bookFromDb.Authors,
                Category = bookFromDb.Category.ToString(),
                CategoryInt = (int)bookFromDb.Category,
                Description = bookFromDb.Description,
                Price = bookFromDb.Price,
                Title = bookFromDb.Title,
                BookUrl = bookFromDb.BookUrl,
                IsFeatured = bookFromDb.IsFeatured,
                ShortDescription = bookFromDb.ShortDescription
            };
        }

        public async Task<List<BookModel>> GetBooksByCategory(string category)
        {
            List<BookModel> books = new List<BookModel>();

            var booksFromDb = await _books.Find(book => book.Category == GetCategory(category)).ToListAsync();

            foreach (var book in booksFromDb)
            {
                books.Add(new BookModel
                {
                    Authors = book.Authors,
                    Id = book.Id,
                    Category = book.Category.ToString(),
                    Description = book.Description,
                    Price = book.Price,
                    Title = book.Title,
                    BookUrl = book.BookUrl,
                    IsFeatured = book.IsFeatured,
                    ShortDescription = book.ShortDescription
                });
            }

            return books;
        }

        public List<BookModel> GetBooksByIds(List<string> booksIds)
        {
            List<BookModel> books = new List<BookModel>();

            foreach (var id in booksIds)
            {
                books.Add(GetBook(id).Result);
            }

            return books;
        }

        public async Task<BookModel> UpdateBook(BookModel book)
        {
            await _books.FindOneAndReplaceAsync(x => x.Id == book.Id, new Book
            {
                Id = book.Id,
                Authors = book.Authors,
                Category = (Category)(Int32.Parse(book.Category)),
                Description = book.Description,
                Price = book.Price,
                IsFeatured= book.IsFeatured,
                Title = book.Title,
                BookUrl = book.BookUrl,
                ShortDescription = book.ShortDescription
            });

            return book;
        }

        private Category GetCategory(string categoryName)
        {
            string categoryNormalized = categoryName.ToUpperInvariant();
            switch (categoryNormalized)
            {
                case "CLASSICS":
                    return Category.Classics;
                case "CRIME":
                    return Category.Crime;
                case "COMEDY":
                    return Category.Comedy;
                case "FANTASY":
                    return Category.Fantasy;
                case "HORROR":
                    return Category.Horror;
                default:
                    return Category.None;
            }
        }
    }
}