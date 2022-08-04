using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using SE.Core.DomainObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace SE.WebApp.MVC.Extensions
{
    public class SSNAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) =>
            SocialSecurityNumber.Validate(value?.ToString()) ? ValidationResult.Success : new ValidationResult("Invalid social security number");
    }

    public class SSNAttributeAdapter : AttributeAdapterBase<SSNAttribute>
    {
        public SSNAttributeAdapter(SSNAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer) { }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-ssn", GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) => "Social Security Number is invalid";
    }

    public class SSNValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is SSNAttribute ssnAttribute)
                return new SSNAttributeAdapter(ssnAttribute, stringLocalizer);

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
