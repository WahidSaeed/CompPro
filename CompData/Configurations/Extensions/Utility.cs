using CRMData.Configurations.GlobalConfigurationVariables;
using CRMData.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRMData.Configurations.Extensions
{
    public class Utility
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly SessionKeys _sessionKeys;

        public Utility(IHttpContextAccessor httpContext) 
        {
            _httpContext = httpContext;
            _sessionKeys = new SessionKeys();
        }

        public Guid GetLoggedUserId() 
        {
            string UserId = _httpContext.HttpContext.Session.Get<string>(_sessionKeys.UserId);
            Guid _loggedUserId = Guid.Parse(UserId);
            return _loggedUserId;
        }

        public string GetLoggedUserName()
        {
            string UserName = _httpContext.HttpContext.User.Identity.Name;
            return UserName;
        }

        public List<UserAccessibleClaims> GetSelectedMenuItems() {
            
            string area = (_httpContext.HttpContext.GetRouteValue("area") ?? string.Empty).ToString();
            string controller = (_httpContext.HttpContext.GetRouteValue("controller") ?? string.Empty).ToString();
            string action = (_httpContext.HttpContext.GetRouteValue("action") ?? string.Empty).ToString();

            List<UserAccessibleClaims> selectedMenuItems = new List<UserAccessibleClaims>();
            var userAccessibleClaims = _httpContext.HttpContext.Session.Get<List<UserAccessibleClaims>>(_sessionKeys.UserAccessibleClaims);

            if (userAccessibleClaims != null)
            {
                List<UserAccessibleClaims> userAccessibleClaim = userAccessibleClaims.ToList();
                Func<UserAccessibleClaims, bool> filter = (x) =>
                {
                    string _area = x.Area;
                    string _controller = x.Controller;
                    string _action = (string.IsNullOrEmpty(x.Action) ? "Index" : x.Action);

                    bool a = _area == area;
                    bool b = _controller == controller;
                    bool c = _action == action;

                    return a && b && (string.IsNullOrEmpty(_area) || c);
                };
                var _userAccessibleClaim = userAccessibleClaim.Where(filter).ToList();

                if (_userAccessibleClaim.Count() > 0)
                {
                    var menuItem = _userAccessibleClaim.FirstOrDefault();
                    selectedMenuItems.Add(menuItem);
                    while (!menuItem.ParentID.Equals(null))
                    {
                        menuItem = userAccessibleClaim.Where(x => x.MenuID.Equals(menuItem.ParentID)).FirstOrDefault();
                        selectedMenuItems.Add(menuItem);
                    }
                } 
            }

            return selectedMenuItems;
        }
    }
}
