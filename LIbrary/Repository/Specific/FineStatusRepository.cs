using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Generic;

namespace LIbrary.Repository.Specific
{
    public class FineStatusRepository : EntityBaseRepository<FineStatus>, IFineStatusRepository
    {
        public FineStatusRepository(AppDbContext context) : base(context)
        {

        }
    }
}
