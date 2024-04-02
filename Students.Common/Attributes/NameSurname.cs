﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.Attributes
{
    public class NameSurname : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var resultMessage = new ValidationResult("First and last name should begin with a capital letter and have only one space");
            if (value is string strValue)
            {
                if (Regex.IsMatch(strValue, @"^[A-Z][a-z]*\s[A-Z][a-z]*$"))
                {
                    resultMessage = ValidationResult.Success;
                }
            }
            return resultMessage;
        }
    }
}
