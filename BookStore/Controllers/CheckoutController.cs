using Application.Models;
using Application.Services.Interface;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
	public class CheckoutController : Controller
	{
        private readonly IBookService _bookService;

        public CheckoutController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
		{
            var booksIds = GetBooksIds(HttpContext.Session, SessionKey);

            List<BookModel> books = _bookService.GetBooksByIds(booksIds.Select(x => x.Id).ToList());

			return View(books);
        }

        public bool FinishCheckout()
        {
            var currentSession = HttpContext.Session;

            currentSession.Remove(SessionKey);

            return true;
        }

        private static string SessionKey = "BooksToCheckoutIds";

        private static List<BookInCartModel> GetBooksIds(ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? new List<BookInCartModel>() : JsonConvert.DeserializeObject<List<BookInCartModel>>(value);
        }
    }
}
