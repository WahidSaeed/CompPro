using CompData.Dao.Account;
using CRMData.Configurations.Generics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompData.Services.Account.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDao _accountDao;
        public AccountService(IAccountDao accountDao) {
            this._accountDao = accountDao;
        }

        public Task<Result> UpdateUserProfileData(string userEmail, string fullName, string phone, string designation, string about, string websiteURL, bool isActive)
        {
            return _accountDao.UpdateUserProfileData(userEmail, fullName, phone, designation, about, websiteURL, isActive);
        }
    }
}
