using System.ComponentModel.DataAnnotations;

namespace Masa.Blazor
{
    [Obsolete]
    public class EnumerableValidationResult : ValidationResult
    {
        public EnumerableValidationResult()
            : base("Invalid enumerable.")
        {

        }

        public List<ValidationResultDescriptor> Descriptors { get; } = new();
    }

    [Obsolete]
    public class ValidationResultDescriptor
    {
        public ValidationResultDescriptor(object objectInstance, List<ValidationResult> results)
        {
            ObjectInstance = objectInstance;
            Results = results ?? throw new ArgumentNullException(nameof(results));
        }

        public object ObjectInstance { get; }

        public List<ValidationResult> Results { get; }
    }
}
