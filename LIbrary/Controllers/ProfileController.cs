using AutoMapper;
using LIbrary.Services.BookCatalogue;
using LIbrary.Services.Profile;
using LIbrary.ViewModels.BookCatalogue;
using LIbrary.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LIbrary.Controllers
{
    public class ProfileController:Controller
    {
        private readonly IProfileService _profileService;
        private readonly IBookCatalogueService _bookCatalogueService;
        private readonly IMapper _mapper;

        public ProfileController(IProfileService profileService, IMapper mapper, IBookCatalogueService bookCatalogueService)
        {
            _profileService = profileService;
            _mapper = mapper;
            _bookCatalogueService = bookCatalogueService;
        }

        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> Index()
        {
            var Id = User.FindFirstValue("Id");
            var reader = await _profileService.GetReaderByIdAsync(Id);
            var profile = _mapper.Map<ProfileReadVM>(reader);
            var returnedbooks = await _bookCatalogueService.GetReturnedBooksByReaderIdAsync(Id);
            profile.books = _mapper.Map<List<BookCoverVM>>(returnedbooks.Take(5).ToList());
            var currentlyBorrowedBooks = await _bookCatalogueService.GetBorrowedBooksByReaderIdAsync(Id);
            profile.numberOfBooksInposession=currentlyBorrowedBooks.Count;
            ViewBag.Title = "My Profile";
            return View(profile);
        }
    }
}
