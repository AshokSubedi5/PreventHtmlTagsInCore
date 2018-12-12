using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PreventHtmlCore
{
    public class AllowHtmlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }

    public class PreventHtmlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var tagWithoutClosingRegex = new Regex(@"<[^>]+>");
            var hasTags = false;
            if (value.GetType() == typeof(string))
            {

                hasTags = tagWithoutClosingRegex.IsMatch(value.ToString());
                if (!hasTags) return ValidationResult.Success;
                return new ValidationResult
                (String.Format("{0} cannot contain html tags", validationContext.DisplayName));
            }
            else
            {
                foreach (PropertyInfo propertyInfo in value.GetType().GetProperties())
                {
                    var hasAllowHtml = Attribute.IsDefined(propertyInfo, typeof(AllowHtmlAttribute));
                    if (!hasAllowHtml)
                    {
                        hasTags = tagWithoutClosingRegex.IsMatch(propertyInfo.GetValue(value, null)?.ToString() ?? "");
                        if (hasTags)
                            return new ValidationResult
                            (String.Format("{0} cannot contain html tags", propertyInfo.Name));
                    }
                }
                return ValidationResult.Success;
            }

        }
    }
}
