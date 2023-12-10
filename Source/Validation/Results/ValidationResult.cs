using System.Collections.Generic;
using GUtils.Validation.Data;
using GUtils.Validation.Enums;

namespace GUtils.Validation.Results
{
    public sealed class ValidationResult : IValidationResult
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<IValidationLog> ValidationLogs { get; }

        public ValidationResult(ValidationResultType validationResultType, IReadOnlyList<IValidationLog> validationLogs)
        {
            ValidationResultType = validationResultType;
            ValidationLogs = validationLogs;
        }
    }
}
