﻿using SiteZeras.Components.Alerts;
using SiteZeras.Objects;
using SiteZeras.Resources.Views.AccountView;
using SiteZeras.Services;
using SiteZeras.Validators;
using System;
using System.Web.Mvc;

namespace SiteZeras.Controllers
{
    [AllowAnonymous]
    public class AuthController : ValidatedController<IAccountValidator, IAccountService>
    {
        public AuthController(IAccountValidator validator, IAccountService service)
            : base(validator, service)
        {
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Service.IsLoggedIn())
                return RedirectToDefault();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "Id")] AccountView account)
        {
            if (Service.IsLoggedIn())
                return RedirectToDefault();

            if (!Validator.CanRegister(account))
                return View(account);

            Service.Register(account);
            Alerts.Add(AlertTypes.Success, Messages.SuccesfulRegistration);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Recover()
        {
            if (Service.IsLoggedIn())
                return RedirectToDefault();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Recover(AccountRecoveryView account)
        {
            if (Service.IsLoggedIn())
                return RedirectToDefault();

            if (!Validator.CanRecover(account))
                return View(account);

            Service.Recover(account);
            Alerts.Add(AlertTypes.Info, Messages.RecoveryInformation, 0);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Reset(String token)
        {
            if (Service.IsLoggedIn())
                return RedirectToDefault();

            AccountResetView account = new AccountResetView();
            account.Token = token;

            if (!Validator.CanReset(account))
                return RedirectToAction("Recover");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reset(AccountResetView account)
        {
            if (Service.IsLoggedIn())
                return RedirectToDefault();

            if (!Validator.CanReset(account))
                return RedirectToAction("Recover");

            Service.Reset(account);
            Alerts.Add(AlertTypes.Success, Messages.SuccesfulReset);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login(String returnUrl)
        {
            if (Service.IsLoggedIn())
                return RedirectToLocal(returnUrl);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountLoginView account, String returnUrl)
        {
            if (Service.IsLoggedIn())
                return RedirectToLocal(returnUrl);

            if (!Validator.CanLogin(account))
                return View(account);

            Service.Login(account.Username);

            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        public RedirectToRouteResult Logout()
        {
            Service.Logout();

            return RedirectToAction("Login");
        }
    }
}
