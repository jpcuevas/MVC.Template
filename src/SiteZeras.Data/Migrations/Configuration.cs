﻿using SiteZeras.Data.Core;
using SiteZeras.Objects;
using System;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SiteZeras.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<Context>, IDisposable
    {
        private Context context;

        public Configuration()
        {
            ContextKey = "SiteZeras.Data";
        }

        protected override void Seed(Context context)
        {
            this.context = context;

            SeedAllPrivileges();
            SeedAdministratorRole();
            SeedAccounts();
        }
        private void SeedAllPrivileges()
        {
            Privilege[] allPrivileges =
            {
                new Privilege { Area = "Administration", Controller = "Accounts", Action = "Index" },
                new Privilege { Area = "Administration", Controller = "Accounts", Action = "Details" },
                new Privilege { Area = "Administration", Controller = "Accounts", Action = "Edit" },

                new Privilege { Area = "Administration", Controller = "Roles", Action = "Index" },
                new Privilege { Area = "Administration", Controller = "Roles", Action = "Create" },
                new Privilege { Area = "Administration", Controller = "Roles", Action = "Details" },
                new Privilege { Area = "Administration", Controller = "Roles", Action = "Edit" },
                new Privilege { Area = "Administration", Controller = "Roles", Action = "Delete" }
            };

            Privilege[] privileges = context.Set<Privilege>().ToArray();
            foreach (Privilege privilege in allPrivileges)
                if (!privileges.Any(priv =>
                        privilege.Area == priv.Area &&
                        privilege.Action == priv.Action &&
                        privilege.Controller == priv.Controller))
                    context.Set<Privilege>().Add(privilege);

            context.SaveChanges();
        }
        private void SeedAdministratorRole()
        {
            if (!context.Set<Role>().Any(role => role.Name == "Sys_Admin"))
            {
                context.Set<Role>().Add(new Role { Name = "Sys_Admin" });
                context.SaveChanges();
            }

            String adminRoleId = context.Set<Role>().Single(role => role.Name == "Sys_Admin").Id;
            RolePrivilege[] adminPrivileges = context
                .Set<RolePrivilege>()
                .Where(rolePrivilege => rolePrivilege.RoleId == adminRoleId)
                .ToArray();

            foreach (Privilege privilege in context.Set<Privilege>())
                if (!adminPrivileges.Any(rolePrivilege => rolePrivilege.PrivilegeId == privilege.Id))
                    context.Set<RolePrivilege>().Add(new RolePrivilege
                    {
                        RoleId = adminRoleId,
                        PrivilegeId = privilege.Id
                    });

            context.SaveChanges();
        }
        private void SeedAccounts()
        {
            Account[] accounts =
            {
                new Account
                {
                    Username = "admin",
                    Passhash = "$2a$13$yTgLCqGqgH.oHmfboFCjyuVUy5SJ2nlyckPFEZRJQrMTZWN.f1Afq", // Admin123?
                    Email = "admin@admins.com",
                    RoleId = context.Set<Role>().Single(role => role.Name == "Sys_Admin").Id
                },
                new Account
                {
                    Username = "test",
                    Passhash = "$2a$13$VLUUfSyotu8Ec.D4mZRCE.YuQ5i7CbTi84LGQp1aFb7xvVksPVLdm", // test
                    Email = "test@tests.com"
                }
            };

            foreach (Account account in accounts)
                if (!context.Set<Account>().Any(acc => acc.Username == account.Username))
                    context.Set<Account>().Add(account);

            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
