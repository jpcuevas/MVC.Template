using SiteZeras.Resources.Shared;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SiteZeras.Components.Mvc
{
    public class MinLengthAdapter : MinLengthAttributeAdapter
    {
        public MinLengthAdapter(ModelMetadata metadata, ControllerContext context, MinLengthAttribute attribute)
            : base(metadata, context, attribute)
        {
            Attribute.ErrorMessage = Validations.FieldMustBeWithMinLengthOf;
        }
    }
}
