using SiteZeras.Resources.Shared;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SiteZeras.Components.Mvc
{
    public class StringLengthAdapter : StringLengthAttributeAdapter
    {
        public StringLengthAdapter(ModelMetadata metadata, ControllerContext context, StringLengthAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (Attribute.MinimumLength == 0)
                Attribute.ErrorMessage = Validations.FieldMustNotExceedLength;
            else
                Attribute.ErrorMessage = Validations.FieldMustBeInRangeOfLength;
        }
    }
}