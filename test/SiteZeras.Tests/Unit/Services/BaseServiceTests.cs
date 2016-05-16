using SiteZeras.Data.Core;
using SiteZeras.Services;
using NSubstitute;
using NUnit.Framework;

namespace SiteZeras.Tests.Unit.Services
{
    [TestFixture]
    public class BaseServiceTests
    {
        private IUnitOfWork unitOfWork;
        private BaseService service;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            service = Substitute.ForPartsOf<BaseService>(unitOfWork);
        }

        [TearDown]
        public void TearDown()
        {
            service.Dispose();
        }

        #region Method: Dispose()

        [Test]
        public void Dispose_DisposesUnitOfWork()
        {
            service.Dispose();

            unitOfWork.Received().Dispose();
        }

        [Test]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            service.Dispose();
            service.Dispose();
        }

        #endregion
    }
}
