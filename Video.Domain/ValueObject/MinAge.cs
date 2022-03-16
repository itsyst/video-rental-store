using System.ComponentModel.DataAnnotations;
using Video.Domain.Entities;
using Video.Domain.Enums;

namespace Video.Domain.ValueObject;

public class MinAge : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var customer = (Customer)validationContext.ObjectInstance;

        if (customer.MembershipTypeId == 0 || customer.MembershipTypeId == MembershipTypes.PAYASYOUGO.Value)
            return ValidationResult.Success;

        if (customer.Birthdate == null)
            return new ValidationResult("Birthdate is required");

        int age = GetAge(customer.Birthdate);
        return  (age >= 18) 
            ? ValidationResult.Success 
            : new ValidationResult("Customer should be at least 18 year old!");
    }
 
    private static int GetAge(DateTime? Birthdate) 
    {
        int year = 0;
        if(Birthdate != null)
        {
            year = DateTime.Now.Year - Birthdate.Value.Year; 
            int month = DateTime.Now.Month - Birthdate.Value.Month; 
            if (month < 0) 
                year--; 
        }
        
        return year; 
    }
}
