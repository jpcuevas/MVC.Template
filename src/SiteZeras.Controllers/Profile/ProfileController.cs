using SiteZeras.Components.Alerts;
using SiteZeras.Components.Security;
using SiteZeras.Objects;
using SiteZeras.Resources.Views.AccountView;
using SiteZeras.Services;
using SiteZeras.Validators;
using System.Web.Mvc;

namespace SiteZeras.Controllers
{
    [AllowUnauthorized]
    public class ProfileController : ValidatedController<IAccountValidator, IAccountService>
    {
        public ProfileController(IAccountValidator validator, IAccountService service)
            : base(validator, service)
        {
        }

        [HttpGet]
        public ActionResult Edit()
        {
            if (!Service.AccountExists(CurrentAccountId))
                return LogOut();

            return View(Service.Get<ProfileEditView>(CurrentAccountId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Id")] ProfileEditView profile)
        {
            if (!Service.AccountExists(CurrentAccountId))
                return LogOut();

            profile.Id = CurrentAccountId;
            if (Validator.CanEdit(profile))
            {
                Service.Edit(profile);
                Alerts.Add(AlertTypes.Success, Messages.ProfileUpdated);
            }

            return View(profile);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            if (!Service.AccountExists(CurrentAccountId))
                return LogOut();

            Alerts.Add(AlertTypes.Danger, Messages.ProfileDeleteDisclaimer, 0);

            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Exclude = "Id")] ProfileDeleteView profile)
        {
            if (!Service.AccountExists(CurrentAccountId))
                return LogOut();

            profile.Id = CurrentAccountId;
            if (!Validator.CanDelete(profile))
            {
                Alerts.Add(AlertTypes.Danger, Messages.ProfileDeleteDisclaimer, 0);
                return View();
            }

            Service.Delete(CurrentAccountId);

            return LogOut();
        }

        private RedirectToRouteResult LogOut()
        {
            return RedirectToAction("Logout", "Auth");
        }
    }
}
