using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Masa.Blazor
{
    [Obsolete("Use System.ComponentModel.DataAnnotations.ValidateComplexType instead.")]
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumerableValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var result = new EnumerableValidationResult();

            if (value is IEnumerable enumerable)
            {
                var hasError = false;

                foreach (var item in enumerable)
                {
                    var context = new ValidationContext(item);
                    var validationResults = new List<ValidationResult>();

                    var success = Validator.TryValidateObject(item, context, validationResults, true);
                    if (!success)
                    {
                        hasError = true;
                    }

                    var descriptor = new ValidationResultDescriptor(item, validationResults);
                    result.Descriptors.Add(descriptor);
                }

                if (hasError)
                {
                    return result;
                }
            }

            return ValidationResult.Success;
        }
    }
}
