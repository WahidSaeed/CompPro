using CRMData.Configurations.Constants.Enums.Application;
using System.Collections.Generic;

namespace CRMData.ViewModels
{
    #region For Page Rendering
    public class Claim
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; } = false;
    }

    public class ApplicationModuleSectionViewModel
    {
        public ApplicationModuleSection ApplicationModuleSection { get; set; }
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }

    public class ProfileClaimConfigurationViewModel
    {
        public ApplicationModule ApplicationModule { get; set; }
        public List<ApplicationModuleSectionViewModel> ApplicationModuleSectionViewModels { get; set; } = new List<ApplicationModuleSectionViewModel>();
    }
    #endregion
}