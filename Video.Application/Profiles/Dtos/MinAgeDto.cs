using System.ComponentModel.DataAnnotations;
using Video.Domain.Enums;

namespace Video.Application.Profiles.Dtos
{
    public class MinAgeDto : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var customerDto = (CustomerDto)validationContext.ObjectInstance;

            if (customerDto.MembershipTypeId == 0 || customerDto.MembershipTypeId == MembershipTypes.PAYASYOUGO.Value)
            {
                return ValidationResult.Success;
            }

            if (customerDto.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required");
            }

            int age = GetAge(customerDto.Birthdate);
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 year old!");
        }

        private static int GetAge(DateTime? Birthdate)
        {
            int year = 0;
            if (Birthdate != null)
            {
                year = DateTime.Now.Year - Birthdate.Value.Year;
                int month = DateTime.Now.Month - Birthdate.Value.Month;
                if (month < 0)
                {
                    year--;
                }
            }

            return year;
        }
    }
}
