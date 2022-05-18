using System;

namespace Utilities.Validations
{
    public static class GuidValidationExtensions
    {
        public static bool IsValidGuid(this Guid id)
        {
            return id != Guid.Empty;
        }

        public static bool IsInvalidGuid(this Guid id)
        {
            return id == Guid.Empty;
        }
    }
}
