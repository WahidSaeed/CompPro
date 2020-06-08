using CRMData.Dao.Account.ProfileClaimConfiguration;
using CRMData.ViewModels;
using System.Collections.Generic;

namespace CRMData.Services.Account.ProfileClaimConfiguration.Implementation
{
    public class ProfileClaimConfiguration : IProfileClaimConfiguration
    {
        private readonly IProfileClaimConfigurationDao profileClaimConfigurationDao;

        public ProfileClaimConfiguration(IProfileClaimConfigurationDao profileClaimConfigurationDao)
        {
            this.profileClaimConfigurationDao = profileClaimConfigurationDao;
        }

        public List<ProfileClaimConfigurationViewModel> GetProfileClaimConfigurations()
        {
            return profileClaimConfigurationDao.GetProfileClaimConfiguration();
        }

        public List<ProfileClaimConfigurationViewModel> UpdateProfileClaimConfigurations(List<ProfileClaimConfigurationViewModel> profileClaims)
        {
            return profileClaimConfigurationDao.UpdateProfileClaimConfiguration(profileClaims);
        }
    }
}
