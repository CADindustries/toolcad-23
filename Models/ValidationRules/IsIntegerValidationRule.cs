using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace toolcad23.Models.ValidationRules
{
    internal class IsIntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return int.TryParse((value ?? "").ToString(), out int _)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "Неверный формат.");
        }
    }
}
