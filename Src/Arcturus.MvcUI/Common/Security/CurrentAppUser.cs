using Arcturus.Application;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Arcturus.MvcUI.Common.Security
{
    public class CurrentAppUser : ICurrentAppUser
    {
        //private readonly UserBO user;
        //private readonly Guid sessionUID;


        //public int ID
        //{
        //    get
        //    {
        //        return user.UserID;
        //    }
        //}



        //public IEnumerable<string> Roles
        //{
        //    get
        //    {
        //        return user.Roles;
        //    }
        //}

        //public UserBO Details { get { return user; } }

        //public Guid SessionUID { get { return sessionUID; } }

        //public string Name { get { return user.FullName;  } }

        //public CurrentAppUser(IHttpContextAccessor contextAccessor)
        //{
        //    if (contextAccessor.HttpContext == null)
        //    {
        //        user = getEmptyUser();
        //        return;
        //    }

        //    var _currentUser = contextAccessor.HttpContext.User;

        //    if (_currentUser == null)
        //    {
        //        user = getEmptyUser();
        //        return;
        //    }

        //    ClaimsIdentity _claimsIdentity = _currentUser.Identity as ClaimsIdentity;
        //    Claim _claim = _claimsIdentity?.FindFirst(ClaimTypes.UserData);
        //    string _userData = _claim?.Value;

        //    if (string.IsNullOrWhiteSpace(_userData))
        //    {
        //        user = getEmptyUser();
        //        return;
        //    }

        //    try
        //    {
        //        user = JsonConvert.DeserializeObject<UserBO>(_userData);
        //    }
        //    catch
        //    {
        //        user = getEmptyUser();
        //    }

        //    Claim _sid = _claimsIdentity?.FindFirst(ClaimTypes.Sid);
        //    string _sidData = _sid?.Value;

        //    if (Guid.TryParse(_sidData, out Guid _sessionUID))
        //    {
        //        sessionUID = _sessionUID;
        //    }
        //}

        //private static UserBO getEmptyUser()
        //{
        //    return new UserBO();
        //}
        public int ID => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public Guid SessionUID => throw new NotImplementedException();

        public IEnumerable<string> Roles => throw new NotImplementedException();
    }
}
