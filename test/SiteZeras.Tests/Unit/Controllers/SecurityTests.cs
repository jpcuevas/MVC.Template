using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Controllers
{
    [TestFixture]
    public class SecurityTests
    {
        #region ValidateAntiForgeryToken

        [Test]
        public void AllControllerPostMethods_HasValidateAntiForgeryToken()
        {
            IEnumerable<MethodInfo> postMethods = Assembly
                .Load("SiteZeras.Controllers")
                .GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.GetCustomAttribute<HttpPostAttribute>() != null);

            foreach (MethodInfo method in postMethods)
                Assert.IsNotNull(method.GetCustomAttribute<ValidateAntiForgeryTokenAttribute>(),
                    String.Format("{0}.{1} does not have ValidateAntiForgeryToken attribute specified.",
                        method.ReflectedType.Name,
                        method.Name));
        }

        #endregion
    }
}
