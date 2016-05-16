using AutoMapper;
using SiteZeras.Objects;

namespace SiteZeras.Data.Mapping
{
    public static class ObjectMapper
    {
        public static void MapObjects()
        {
            MapAccounts();
            MapRoles();
        }

        #region Administration

        private static void MapAccounts()
        {
            Mapper.CreateMap<Account, AccountView>();
            Mapper.CreateMap<AccountView, Account>();

            Mapper.CreateMap<Account, AccountEditView>();

            Mapper.CreateMap<Account, ProfileEditView>();

            Mapper.CreateMap<PersonalInformation, ProfileEditView>();
        }

        private static void MapRoles()
        {
            Mapper.CreateMap<Role, RoleView>();
            Mapper.CreateMap<RoleView, Role>();
        }

        #endregion
    }
}
