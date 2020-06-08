using CRMData.Configurations.Attributes.Identity;
using CRMData.Data;
using CRMData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CRMData.Dao.Account.ProfileClaimConfiguration.Implementation
{
    public class ProfileClaimConfigurationDao : IProfileClaimConfigurationDao
    {
        ApplicationDbContext applicationDbContext;
        public ProfileClaimConfigurationDao(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public List<ProfileClaimConfigurationViewModel> GetProfileClaimConfiguration()
        {
            List<ProfileClaimConfigurationViewModel> profileClaimConfgurations = new List<ProfileClaimConfigurationViewModel>();

            List<Type> types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.Namespace == "CRMData.Configurations.Constants.Enums.Identity" && x.IsEnum && x.GetCustomAttribute<ModuleClaims>() != null).ToList();

            foreach (Type type in types)
            {
                ModuleClaims moduleClaims = type.GetCustomAttribute<ModuleClaims>();
                ProfileClaimConfigurationViewModel profileClaim = profileClaimConfgurations
                    .Where(x => x.ApplicationModule.Equals(moduleClaims.ApplicationModule)).FirstOrDefault();

                if (profileClaim == null)
                {
                    ProfileClaimConfigurationViewModel model = new ProfileClaimConfigurationViewModel();
                    model.ApplicationModule = moduleClaims.ApplicationModule;

                    var fields = type.GetFields();
                    if (fields.Count() > 0)
                    {
                        List<Claim> claims = new List<Claim>();
                        foreach (var field in fields.Skip(1))
                        {
                            claims.Add(new Claim() { Name = field.Name, Value = field.GetRawConstantValue().ToString(), Checked = false });
                        }
                        model.ApplicationModuleSectionViewModels.Add(new ApplicationModuleSectionViewModel() { ApplicationModuleSection = moduleClaims.ApplicationModuleSection, Claims = claims });
                        profileClaimConfgurations.Add(model);
                    }
                }
                else
                {
                    var fields = type.GetFields();
                    if (fields.Count() > 0)
                    {
                        List<Claim> claims = new List<Claim>();
                        foreach (var field in fields.Skip(1))
                        {
                            claims.Add(new Claim() { Name = field.Name, Value = field.GetRawConstantValue().ToString(), Checked = false });
                        }
                        profileClaim.ApplicationModuleSectionViewModels.Add(new ApplicationModuleSectionViewModel() { ApplicationModuleSection = moduleClaims.ApplicationModuleSection, Claims = claims });
                    }
                }
            }

            return profileClaimConfgurations;
        }

        public List<ProfileClaimConfigurationViewModel> UpdateProfileClaimConfiguration(List<ProfileClaimConfigurationViewModel> profileClaims)
        {
            //applicationDbContext.ApplicationUserRoles.
            return GetProfileClaimConfiguration();
        }
    }
}
