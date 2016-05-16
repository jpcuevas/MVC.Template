﻿using SiteZeras.Components.Extensions.Mvc;
using SiteZeras.Data.Core;
using SiteZeras.Objects;
using SiteZeras.Resources.Views.RoleView;
using System;
using System.Linq;

namespace SiteZeras.Validators
{
    public class RoleValidator : BaseValidator, IRoleValidator
    {
        public RoleValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean CanCreate(RoleView view)
        {
            Boolean isValid = ModelState.IsValid;
            isValid &= IsUniqueRole(view);

            return isValid;
        }
        public Boolean CanEdit(RoleView view)
        {
            Boolean isValid = ModelState.IsValid;
            isValid &= IsUniqueRole(view);

            return isValid;
        }

        private Boolean IsUniqueRole(RoleView view)
        {
            Boolean isUnique = !UnitOfWork
                .Select<Role>()
                .Any(role =>
                    role.Id != view.Id &&
                    role.Name.ToLower() == view.Name.ToLower());

            if (!isUnique)
                ModelState.AddModelError<RoleView>(model => model.Name, Validations.RoleNameIsAlreadyUsed);

            return isUnique;
        }
    }
}
