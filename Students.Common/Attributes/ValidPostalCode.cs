using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Students.Common.Attributes;

public class ValidPostalCode : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var resultMessage = new ValidationResult("Please enter a postal code such as xx-xxx");
        if (value is string strValue)
        {
            if (Regex.IsMatch(strValue, @"\d{2}-\d{3}$"))
            {
                resultMessage = ValidationResult.Success;
            }
        }
        return resultMessage;
    }
}
