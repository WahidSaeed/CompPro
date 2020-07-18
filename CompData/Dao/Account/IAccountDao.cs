using CRMData.Configurations.Generics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompData.Dao.Account
{
    public interface IAccountDao
    {
        public Task<Result> UpdateUserProfileData(string userEmail, string fullName, string phone, string designation, string about, string websiteURL, bool isActive);
    }
}
