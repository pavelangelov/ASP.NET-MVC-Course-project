using System;

namespace Bg_Fishing.Utils
{
    public static class Validator
    {
        /// <summary>
        /// Validate if passed value is null.
        /// </summary>
        /// <param name="value">Value to validate</param>
        /// <param name="paramName">Parameter name.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateForNull(object value, string paramName = null, string errorMessage = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, errorMessage);
            }
        }
    }
}
