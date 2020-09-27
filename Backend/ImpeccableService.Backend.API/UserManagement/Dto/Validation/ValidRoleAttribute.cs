using System.ComponentModel.DataAnnotations;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.API.UserManagement.Dto.Validation
{
    public class ValidRoleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var role = (string) value;
            return role == UserRole.Consumer || role == UserRole.Provider || role == UserRole.ProviderAdmin
                ? ValidationResult.Success
                : new ValidationResult("Invalid role.");
    }
    }
}