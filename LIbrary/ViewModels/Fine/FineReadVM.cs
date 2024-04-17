using LIbrary.Models;

namespace LIbrary.ViewModels.Fine
{
    public class FineReadVM
    {
        public string Id { get; set; }
        public string? borrowItemId { get; set; }
        public virtual BorrowItem borrowItem { get; set; }
        public string? fineStatusId { get; set; }
        public virtual FineStatus fineStatus { get; set; }
    }
}
