using GUtils.Validation.Enums;

namespace GUtils.Validation.Data
{
    public interface IValidationLog
    {
        public ValidationLogType ValidationLogType { get; }
        public string LogMessage { get; }
    }
}
