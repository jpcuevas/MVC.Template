using SiteZeras.Data.Mapping;
using SiteZeras.Objects;
using System.Data.Entity;

namespace SiteZeras.Data.Core
{
    public class Context : DbContext
    {
        #region Administration

        protected DbSet<Account> Accounts { get; set; }

        protected DbSet<Role> Roles { get; set; }
        protected DbSet<Privilege> Privileges { get; set; }
        protected DbSet<RolePrivilege> RolePrivileges { get; set; }
        protected DbSet<PersonalInformation> PersonalInformations { get; set; }

        #endregion

        #region System

        protected DbSet<Log> Logs { get; set; }
        protected DbSet<AuditLog> AuditLogs { get; set; }

        #endregion

        static Context()
        {
            ObjectMapper.MapObjects();
        }
    }
}
