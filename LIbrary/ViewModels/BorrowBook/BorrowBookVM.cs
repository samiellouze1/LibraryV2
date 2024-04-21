using LIbrary.ViewModels.BookCatalogue;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.ViewModels.BorrowBook
{
    public class BorrowBookVM
    {
        [Required(ErrorMessage = "The BookReadVM field is required.")]
        public BookReadVM bookReadVM { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The End Date field is required.")]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End Date must be greater than Start Date.")]
        public DateTime EndDate { get; set; }
    }
}

public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
        {
            throw new ArgumentException("Property with this name not found");
        }

        var startDateValue = property.GetValue(validationContext.ObjectInstance) as DateTime?;
        var endDateValue = value as DateTime?;

        if (endDateValue.HasValue && startDateValue.HasValue && endDateValue <= startDateValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
