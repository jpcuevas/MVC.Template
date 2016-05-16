﻿using SiteZeras.Data.Logging;
using SiteZeras.Objects;
using SiteZeras.Tests.Data;
using SiteZeras.Tests.Objects;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SiteZeras.Tests.Unit.Data.Logging
{
    [TestFixture]
    public class AuditLoggerTests
    {
        private TestingContext dataContext;
        private DbContext context;

        private DbEntityEntry<BaseModel> entry;
        private AuditLogger logger;

        [SetUp]
        public void SetUp()
        {
            context = new TestingContext();
            dataContext = new TestingContext();
            TestModel model = ObjectFactory.CreateTestModel();
            logger = Substitute.ForPartsOf<AuditLogger>(context);
            HttpContext.Current = HttpContextFactory.CreateHttpContext();

            entry = dataContext.Entry<BaseModel>(dataContext.Set<TestModel>().Add(model));
            dataContext.Set<TestModel>().RemoveRange(dataContext.Set<TestModel>());
            dataContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            HttpContext.Current = null;
            dataContext.Dispose();
            context.Dispose();
        }

        #region Method: Log(IEnumerable<DbEntityEntry<BaseModel>> entries)

        [Test]
        public void Log_LogsAddedEntities()
        {
            entry.State = EntityState.Added;

            Logs(entry);
        }

        [Test]
        public void Log_LogsModifiedEntities()
        {
            (entry.Entity as TestModel).Text += "?";
            entry.State = EntityState.Modified;

            Logs(entry);
        }

        [Test]
        public void Log_DoesNotLogModifiedEntitiesWithoutChanges()
        {
            entry.State = EntityState.Modified;

            logger.Log(new[] { entry });

            logger.DidNotReceiveWithAnyArgs().Log((LoggableEntity)null);
        }

        [Test]
        public void Log_LogsDeletedEntities()
        {
            entry.State = EntityState.Deleted;

            Logs(entry);
        }

        [Test]
        public void Log_DoesNotLogUnsupportedEntityStates()
        {
            IEnumerable<EntityState> unsupportedStates = Enum
                .GetValues(typeof(EntityState))
                .Cast<EntityState>()
                .Where(state =>
                    state != EntityState.Added &&
                    state != EntityState.Modified &&
                    state != EntityState.Deleted);

            foreach (EntityState usupportedState in unsupportedStates)
            {
                entry.State = usupportedState;
                logger.Log(new[] { entry });
            }

            logger.DidNotReceiveWithAnyArgs().Log((LoggableEntity)null);
        }

        [Test]
        public void Log_DoesNotSaveLogs()
        {
            entry.State = EntityState.Added;
            DbContext context = Substitute.For<DbContext>();
            logger = Substitute.ForPartsOf<AuditLogger>(context);
            logger.When(sub => sub.Log(Arg.Any<LoggableEntity>())).DoNotCallBase();

            logger.Log(new[] { entry });

            logger.DidNotReceive().Save();
            context.DidNotReceive().SaveChanges();
        }

        #endregion

        #region Method: Log(LoggableEntity entity)

        [Test]
        public void Log_AddsLogToTheSet()
        {
            LoggableEntity entity = new LoggableEntity(entry);

            logger.Log(entity);

            AuditLog actual = context.ChangeTracker.Entries<AuditLog>().First().Entity;
            LoggableEntity expected = entity;

            Assert.AreEqual(HttpContext.Current.User.Identity.Name, actual.AccountId);
            Assert.AreEqual(expected.ToString(), actual.Changes);
            Assert.AreEqual(expected.Name, actual.EntityName);
            Assert.AreEqual(expected.Action, actual.Action);
            Assert.AreEqual(expected.Id, actual.EntityId);
        }

        [Test]
        public void Log_DoesNotSaveLog()
        {
            entry.State = EntityState.Added;
            DbContext context = Substitute.For<DbContext>();
            LoggableEntity entity = new LoggableEntity(entry);

            logger = Substitute.ForPartsOf<AuditLogger>(context);
            logger.When(sub => sub.Log(Arg.Any<LoggableEntity>())).DoNotCallBase();

            logger.Log(entity);

            logger.DidNotReceive().Save();
            context.DidNotReceive().SaveChanges();
        }

        #endregion

        #region Method: Save()

        [Test]
        public void Save_SavesLogs()
        {
            DbContext context = Substitute.For<DbContext>();
            logger = Substitute.ForPartsOf<AuditLogger>(context);

            logger.Save();

            context.Received().SaveChanges();
        }

        #endregion

        #region Method: Dispose()

        [Test]
        public void Dispose_DisposesContext()
        {
            DbContext context = Substitute.For<DbContext>();
            logger = Substitute.ForPartsOf<AuditLogger>(context);

            logger.Dispose();

            context.Received().Dispose();
        }

        [Test]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            logger.Dispose();
            logger.Dispose();
        }

        #endregion

        #region Test helpers

        private void Logs(DbEntityEntry<BaseModel> entry)
        {
            LoggableEntity expected = new LoggableEntity(entry);
            logger.When(sub => sub.Log(Arg.Any<LoggableEntity>())).DoNotCallBase();
            logger.When(sub => sub.Log(Arg.Any<LoggableEntity>())).Do(info =>
            {
                LoggableEntity actual = info.Arg<LoggableEntity>();

                Assert.AreEqual(expected.ToString(), actual.ToString());
                Assert.AreEqual(expected.Action, actual.Action);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Id, actual.Id);
            });

            logger.Log(new[] { entry });

            logger.ReceivedWithAnyArgs().Log(expected);
        }

        #endregion
    }
}
