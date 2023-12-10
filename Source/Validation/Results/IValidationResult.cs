using System.Collections.Generic;
using GUtils.Validation.Data;
using GUtils.Validation.Enums;

namespace GUtils.Validation.Results
{
    public interface IValidationResult
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<IValidationLog> ValidationLogs { get; }
    }
}
