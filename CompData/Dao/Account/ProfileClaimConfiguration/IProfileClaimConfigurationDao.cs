using CRMData.ViewModels;
using System.Collections.Generic;

namespace CRMData.Dao.Account.ProfileClaimConfiguration
{
    public interface IProfileClaimConfigurationDao
    {
        public List<ProfileClaimConfigurationViewModel> GetProfileClaimConfiguration();
        public List<ProfileClaimConfigurationViewModel> UpdateProfileClaimConfiguration(List<ProfileClaimConfigurationViewModel> profileClaims);

    }
}
