using Application.Models;
using Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
	public class AdminController : Controller
	{
        private readonly IBookService _bookService;
        public AdminController(IBookService bookService)
        {
            _bookService = bookService;
        }   

        public async Task<IActionResult> Index()
		{
            List<BookModel> books = await _bookService.GetAllBooks();

            return View(books);
        }
	}
}
