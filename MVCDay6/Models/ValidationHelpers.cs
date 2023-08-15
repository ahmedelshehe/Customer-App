using System.ComponentModel.DataAnnotations;

namespace MVCDay6.Models
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date > DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }

    }

    public class TotalPriceAttribute : ValidationAttribute
    {
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
            if(value == null) 
            {
                return new ValidationResult("Total Price Can not be Empty");
            }else
            {
                if ((double)value < 0)
                    return new ValidationResult("Total Price Can not be Negative");
                else
					return ValidationResult.Success;
			}
		}
	}
}
