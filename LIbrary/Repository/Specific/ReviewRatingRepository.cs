using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public class ReviewRatingRepository : EntityBaseRepository<ReviewRating>, IReviewRatingRepository
    {
        public ReviewRatingRepository(AppDbContext context) : base(context)
        {

        }
    }
}
