using CRMData.Configurations.GlobalConfigurationVariables;
using CRMData.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CRMData.Data;

namespace CRMData.Configurations.Extensions
{
    public class Utility
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly SessionKeys _sessionKeys;
        private readonly ApplicationDbContext _context;

        public Utility(IHttpContextAccessor httpContext, ApplicationDbContext context) 
        {
            _httpContext = httpContext;
            _sessionKeys = new SessionKeys();
            _context = context;
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

        public bool IsDomain(string domain) 
        {
            var isDomained = _context.OrganizationDomains.Any(x => x.Domain.ToLower().Equals(domain.ToLower()));
            return isDomained;
        }

        public bool IsCountry(string countryCode) 
        { 
            string countryCodeL = countryCode.ToLower();
            bool isCountry = _context.Countries.Any(x => x.CountryId.Equals(countryCodeL));
            return isCountry;
        }

        public string GetAppSetting(string key)
        {
            var appSetting = _context.AppSettings.Where(x => x.Key.ToLower().Equals(key.ToLower())).FirstOrDefault();
            return appSetting.Value;
        }


        public async Task<T> PostRequestAsync<T>(string requestURI, dynamic content = null)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(requestURI, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsStringAsync();
                    var _response = JsonConvert.DeserializeObject<T>(results);
                    return _response;
                }
                return default(T);
            }
        }

        public async Task<T> GetRequestAsync<T>(string requestURI)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(requestURI);
                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsStringAsync();
                    var _response = JsonConvert.DeserializeObject<T>(results);
                    return _response;
                }
                return default(T);
            }
        }

        public async Task<string> GetRequestAsync(string requestURI)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(requestURI);
                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsStringAsync();
                    return results;
                }
                return null;
            }
        }
    }
}
