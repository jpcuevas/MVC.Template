using SiteZeras.Data.Core;
using SiteZeras.Tests.Data.Mapping;
using SiteZeras.Tests.Objects;
using System.Data.Entity;

namespace SiteZeras.Tests.Data
{
    public class TestingContext : Context
    {
        #region DbSets

        #region Test

        private DbSet<TestModel> TestModels { get; set; }

        #endregion

        #endregion

        static TestingContext()
        {
            TestObjectMapper.MapObjects();
        }
    }
}
