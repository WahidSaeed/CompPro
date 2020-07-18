using CRMData.Configurations.Constants.Enums;
using CRMData.Configurations.Generics;
using CRMData.Data;
using CRMData.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace CompData.Dao.Account.Impl
{
    public class AccountDao : IAccountDao
    {
        public ApplicationDbContext _dbContext;
        public UserManager<ApplicationUser> _userManager;
        public AccountDao(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        public async Task<Result> UpdateUserProfileData(string userEmail, string fullName, string phone, string designation, string about, string websiteURL, bool isActive)
        {
			try
			{

                var user = await this._userManager.FindByEmailAsync(userEmail);
                user.FullName = fullName;
                user.PhoneNumber = phone;
                user.IsActive = isActive;
                user.Designation = designation;
                user.About = about;
                user.WebsiteURL = websiteURL;


                var result = await this._userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new Result
                    {
                        Status = ResultStatus.Success,
                        Message = "Profile has been updated."
                    };
                }
                else
                {
                    throw new Exception("Error Occured. Please try again.");
                }

                
            }
			catch (Exception ex)
			{

                return new Result
                {
                    Status = ResultStatus.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
