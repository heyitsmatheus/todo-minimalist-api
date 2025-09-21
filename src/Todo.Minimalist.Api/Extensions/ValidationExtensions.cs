using System.ComponentModel.DataAnnotations;
using Todo.Minimalist.Api.Models.Errors;

namespace Todo.Minimalist.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static bool TryValidate<T>(this T obj, out List<FieldError> errors)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(obj!);
            errors = [];

            bool isValid = Validator.TryValidateObject(obj!, context, validationResults, true);

            if (!isValid)
            {
                errors = [.. validationResults.SelectMany(vr => vr.MemberNames.Select(m => new FieldError
                {
                    Field = m,
                    Error = vr.ErrorMessage ?? "Invalid field"
                }))];
            }

            return isValid;
        }
    }
}