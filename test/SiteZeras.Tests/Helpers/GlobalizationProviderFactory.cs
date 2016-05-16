using SiteZeras.Components.Mvc;
using NSubstitute;
using System.Globalization;
using System.Threading;

namespace SiteZeras.Tests
{
    public class GlobalizationProviderFactory
    {
        public static IGlobalizationProvider CreateProvider()
        {
            IGlobalizationProvider provider = Substitute.For<IGlobalizationProvider>();
            SetUpSubstitute(provider);

            return provider;
        }

        private static void SetUpSubstitute(IGlobalizationProvider provider)
        {
            Language[] languages =
            {
                new Language
                {
                    Culture = new CultureInfo("en-GB"),
                    Abbrevation = "en",
                    IsDefault = true,
                    Name = "English"
                },
                new Language
                {
                    Culture = new CultureInfo("lt-LT"),
                    Abbrevation = "lt",
                    IsDefault = false,
                    Name = "Lietuvių"
                },
                new Language
                {
                    Culture = new CultureInfo("es-PA"),
                    Abbrevation = "pa",
                    IsDefault = false,
                    Name = "Spanish"
                }
            };

            provider.When(language => language.CurrentLanguage = Arg.Any<Language>()).Do(value =>
            {
                Thread.CurrentThread.CurrentUICulture = value.Arg<Language>().Culture;
                Thread.CurrentThread.CurrentCulture = value.Arg<Language>().Culture;
            });
            provider.CurrentLanguage.Returns(languages[0]);
            provider.DefaultLanguage.Returns(languages[0]);
            provider.Languages.Returns(languages);
            provider["en"].Returns(languages[0]);
            provider["lt"].Returns(languages[1]);
            provider["pa"].Returns(languages[2]);
            
        }
    }
}
