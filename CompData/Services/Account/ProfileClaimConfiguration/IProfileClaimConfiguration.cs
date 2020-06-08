using CRMData.ViewModels;
using System.Collections.Generic;

namespace CRMData.Services.Account.ProfileClaimConfiguration
{
    public interface IProfileClaimConfiguration
    {
        public List<ProfileClaimConfigurationViewModel> GetProfileClaimConfigurations();

        public List<ProfileClaimConfigurationViewModel> UpdateProfileClaimConfigurations(List<ProfileClaimConfigurationViewModel> profileClaims);
    }
}
