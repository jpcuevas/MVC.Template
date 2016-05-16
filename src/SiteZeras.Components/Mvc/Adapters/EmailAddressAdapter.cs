using SiteZeras.Resources.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SiteZeras.Components.Mvc
{
    public class EmailAddressAdapter : DataAnnotationsModelValidator<EmailAddressAttribute>
    {
        public EmailAddressAdapter(ModelMetadata metadata, ControllerContext context, EmailAddressAttribute attribute)
            : base(metadata, context, attribute)
        {
            Attribute.ErrorMessage = Validations.FieldIsNotValidEmail;
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[]
            {
                new ModelClientValidationRule
                {
                    ErrorMessage = ErrorMessage,
                    ValidationType = "email"
                }
            };
        }
    }
}
