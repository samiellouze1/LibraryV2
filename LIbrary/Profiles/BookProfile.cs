using AutoMapper;
using LIbrary.Models;
using LIbrary.ViewModels.BookCatalogue;


namespace LIbrary.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookReadVM>()
            .ForMember(dest => dest.numberOfCopies, opt => opt.MapFrom(src => AvailableCopies(src)))
            .ForMember(dest => dest.averageRating, opt => opt.MapFrom(src => AverageRating(src)))
            .ForMember(dest => dest.authorName, opt => opt.MapFrom(src => src.author.name))
            .ForMember(dest => dest.genreName, opt => opt.MapFrom(src => src.genre.name))
            .ForMember(dest => dest.reviewRatings, opt => opt.MapFrom(src =>
                src.bookCopies
                    .SelectMany(bc => bc.borrowItems
                        .Select(bi => bi.reviewRating))
                    .Where(rating => rating != null)  // Filter out null ratings
                    .ToList()));
        CreateMap<Book, BookCoverVM>();
    }
    private int AvailableCopies(Book book)
    {
        var num = 0;
        foreach (var bookCopy in book.bookCopies)
        {
            bool testAvailable = !bookCopy.borrowItems.Select(bi => bi.borrowItemStatusId).Any(bi => bi == "1" || bi==null);
            if (testAvailable)
            {
                num++;
            }
        }
        return num;
    }
    public int AverageRating(Book book)
    {
        // Check if the book has any copies with review ratings
        if (book.bookCopies.Any(bc => bc.borrowItems.Any(bi => bi.reviewRating != null)))
        {
            // Flatten the review ratings of all book copies
            var allReviewRatings = book.bookCopies
                .SelectMany(bc => bc.borrowItems)
                .Where(bi => bi.reviewRating != null)
                .Select(bi => bi.reviewRating.rating)
                .ToList();

            // Calculate the average rating if there are review ratings available
            if (allReviewRatings.Any())
            {
                var averageRating = allReviewRatings.Average();
                return Convert.ToInt32(averageRating);
            }
            else
            {
                // In case there are no review ratings but there are book copies
                return 5;
            }
        }
        else
        {
            // If there are no book copies with review ratings, return default rating
            return 5;
        }
    }

}

