using AutoMapper;
using LIbrary.Models;
using LIbrary.Repository.Specific;
using LIbrary.Services.BookCatalogue;
using LIbrary.Services.EmailSender;
using LIbrary.Services.ReturnBook;
using LIbrary.ViewModels.BookCatalogue;
using LIbrary.ViewModels.BorrowBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LIbrary.Controllers
{
    public class BookCatalogueController : Controller
    {
        private readonly IBookCatalogueService _bookCatalogueService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        public BookCatalogueController(IBookCatalogueService bookCatalogueService, IMapper mapper,IEmailSender emailSender)
        {
            _bookCatalogueService = bookCatalogueService;
            _mapper = mapper;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Books(string searchString, string author,string genre,bool? available)
        {
            //await _emailSender.SendEmailAsync("samiellouze@hotmail.com", "testsubject", "testmessage");
            string Id = User.FindFirstValue("Id");
            var books = await _bookCatalogueService.GetAllBooksAsync();
            var bookReadVms = new List<BookReadVM>();
            if (Id == null)
            {
                bookReadVms=_mapper.Map<List<BookReadVM>>(books);
            }
            else
            {
                foreach (Book book in books)
                {
                    var bookReadVm = _mapper.Map<BookReadVM>(book);
                    bookReadVm.isAlreadyBorrowed = _bookCatalogueService.IsAlreadyBorrowed(book, Id);
                    bookReadVm.isCurrentlyBorrowed = _bookCatalogueService.IsCurrentlyBorrowed(book, Id);
                    bookReadVms.Add(bookReadVm);
                }
            }
            ViewBag.Title = "Browse Books";
            ViewBag.Redirect = "Books";
            ViewBag.Genres = bookReadVms.Select(b => b.genreName).Distinct().ToList();
            ViewBag.Authors = bookReadVms.Select(b => b.authorName).Distinct().ToList();
            ViewBag.GenreValue = genre;
            ViewBag.AuthorValue = author;
            ViewBag.AvailabilityValue = available;
            bookReadVms = Filter(bookReadVms, searchString, author, genre, available);
            return View(bookReadVms);
        }
        public async Task<IActionResult> Book(string bookId)
        {
            var book = await _bookCatalogueService.GetBookByIdAsync(bookId);
            var bookVM = _mapper.Map<BookReadVM>(book);
            string Id = User.FindFirstValue("Id");
            if (Id == null)
            {

            }
            else
            {
                bookVM.isAlreadyBorrowed = _bookCatalogueService.IsAlreadyBorrowed(book, Id);
                bookVM.isCurrentlyBorrowed = _bookCatalogueService.IsCurrentlyBorrowed(book, Id);
            }
            ViewBag.Title = "Book " + bookVM.title;
            return View(bookVM);
        }
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> BorrowedBooks(string searchString, string author, string genre, bool? available)
        {
            string Id = User.FindFirstValue("Id");
            var books = await _bookCatalogueService.GetBorrowedBooksByReaderIdAsync(Id);
            var bookReadVms = new List<BookReadVM>();
            if (Id == null)
            {
                bookReadVms = _mapper.Map<List<BookReadVM>>(books);
            }
            else
            {
                foreach (Book book in books)
                {
                    var bookReadVm = _mapper.Map<BookReadVM>(book);
                    bookReadVm.isAlreadyBorrowed = _bookCatalogueService.IsAlreadyBorrowed(book, Id);
                    bookReadVm.isCurrentlyBorrowed = _bookCatalogueService.IsCurrentlyBorrowed(book, Id);
                    bookReadVms.Add(bookReadVm);
                }
            }
            ViewBag.Title = "Borrowed Books";
            ViewBag.Redirect = "BorrowedBooks";
            ViewBag.Genres = bookReadVms.Select(b => b.genreName).Distinct().ToList();
            ViewBag.Authors = bookReadVms.Select(b => b.authorName).Distinct().ToList();
            ViewBag.GenreValue = genre;
            ViewBag.AuthorValue = author;
            ViewBag.AvailabilityValue = available;
            bookReadVms = Filter(bookReadVms, searchString, author, genre, available);
            return View("Books",bookReadVms);
        }
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> ReturnedBooks(string searchString, string author, string genre, bool? available)
        {
            string Id = User.FindFirstValue("Id");
            var books = await _bookCatalogueService.GetReturnedBooksByReaderIdAsync(Id);
            var bookReadVms = new List<BookReadVM>();
            if (Id == null)
            {
                bookReadVms = _mapper.Map<List<BookReadVM>>(books);
            }
            else
            {
                foreach (Book book in books)
                {
                    var bookReadVm = _mapper.Map<BookReadVM>(book);
                    bookReadVm.isAlreadyBorrowed = _bookCatalogueService.IsAlreadyBorrowed(book, Id);
                    bookReadVm.isCurrentlyBorrowed = _bookCatalogueService.IsCurrentlyBorrowed(book, Id);
                    bookReadVms.Add(bookReadVm);
                }
            }
            ViewBag.Title = "Returned Books";
            ViewBag.Redirect = "ReturnedBooks";
            ViewBag.Genres = bookReadVms.Select(b=>b.genreName).Distinct().ToList();
            ViewBag.Authors = bookReadVms.Select(b=>b.authorName).Distinct().ToList();
            ViewBag.GenreValue = genre;
            ViewBag.AuthorValue = author;
            ViewBag.AvailabilityValue = available;
            bookReadVms = Filter(bookReadVms, searchString, author, genre, available);
            return View("Books",bookReadVms);
        }
        public List<BookReadVM> Filter(List<BookReadVM> bookReadVMs, string searchString, string author, string genre, bool? available)
        {
            if (!searchString.IsNullOrEmpty())
            {
                bookReadVMs = bookReadVMs.Where(br => br.title.Contains(searchString)).ToList();
            }
            if (!author.IsNullOrEmpty())
            {
                bookReadVMs = bookReadVMs.Where(br => br.authorName == author).ToList();
            }
            if (!genre.IsNullOrEmpty())
            {
                bookReadVMs = bookReadVMs.Where(br => br.genreName == genre).ToList();
            }
            if (available.HasValue)
            {
                if (available.Value)
                {
                    bookReadVMs = bookReadVMs.Where(br => br.numberOfCopies > 0).ToList();
                }
                else
                {
                    bookReadVMs = bookReadVMs.Where(br=>br.numberOfCopies == 0).ToList();
                }
            }
            return bookReadVMs;
        }
    }
}
