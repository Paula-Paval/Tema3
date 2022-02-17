using System;
using System.ComponentModel.DataAnnotations;


namespace Cinerva.Web.Models.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string;
            var isValid = true;

            if (!string.IsNullOrEmpty(inputValue))
            {
                isValid = inputValue.Length > 3;
            }

            return isValid;
        }
    }
}
