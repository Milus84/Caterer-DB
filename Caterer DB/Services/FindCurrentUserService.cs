﻿using Caterer_DB.Interfaces;
using Caterer_DB.Models;
using System.Web;
using System;

namespace Caterer_DB.Services
{
    public class FindCurrentUserService : IFindCurrentUserService
    {
        public UserModel CurrentUser()
        {
            return HttpContext.Current.User as UserModel;
        }
    }
}
