using SiteZeras.Objects;
using System;

namespace SiteZeras.Validators
{
    public interface IAccountValidator : IValidator
    {
        Boolean CanRecover(AccountRecoveryView view);
        Boolean CanReset(AccountResetView view);
        Boolean CanLogin(AccountLoginView view);
        Boolean CanRegister(AccountView view);

        Boolean CanEdit(ProfileEditView view);
        Boolean CanEdit(AccountEditView view);
        Boolean CanDelete(ProfileDeleteView view);
    }
}
