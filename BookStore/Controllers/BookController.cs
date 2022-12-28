using Application.Models;
using Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            List<BookModel> books = await _bookService.GetAllBooks();
           
            return View(books);
        }

        public async Task<IActionResult> One(string id)
        {
            BookModel book = await _bookService.GetBook(id);

            return View(book);
        }

        [HttpGet]
        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _bookService.GetAllBooks();
        }

        [HttpGet]
        public async Task<List<BookModel>> GetBooksByCategory(string category)
        {
            return await _bookService.GetBooksByCategory( category);
        }

        [HttpGet]
        public async Task<BookModel> GetBook(string id)
        {
            return await _bookService.GetBook(id);
        }

        [HttpPost]
        public async Task<BookModel> CreateBook(BookModel book)
        {
            return await _bookService.CreateBook(book);
        }

        private static string SessionKey = "BooksToCheckoutIds";

        [HttpPost]
        public async Task<bool> AddToCart(BookModel book)
        {
            var currentSession = HttpContext.Session;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKey)))
            {
                List<BookInCartModel> existedBooksIds = GetBooksIds(currentSession, SessionKey);

                if (!existedBooksIds.Any(x => x.Id == book.Id))
                {
                    existedBooksIds.Add(new BookInCartModel
                    {
                        Id = book.Id,
                        Quantity = book.Quantity
                    });
                }

                SetBooksIds(currentSession, SessionKey, existedBooksIds);
            }
            else
            {
                List<BookInCartModel> newBooksIds = new List<BookInCartModel>();

                newBooksIds.Add(new BookInCartModel
                {
                    Id = book.Id,
                    Quantity = book.Quantity
                });

                SetBooksIds(currentSession, SessionKey, newBooksIds);
            }

            return true;
        }

        private static List<BookInCartModel> GetBooksIds(ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? new List<BookInCartModel>() : JsonConvert.DeserializeObject<List<BookInCartModel>>(value);
        }

        private static void SetBooksIds(ISession session, string key, List<BookInCartModel> booksIds)
        {
            session.SetString(key, JsonConvert.SerializeObject(booksIds));
        }

        [HttpPost]
        public async Task<BookModel> UpdateBook( BookModel book)
        {
            return await _bookService.UpdateBook(book);
        }

        [HttpDelete]
        public async Task<bool> DeleteBook(string id)
        {
            return await _bookService.DeleteBook(id);
        }
    }
}