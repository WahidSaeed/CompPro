using CRMData.Configurations.Constants.Enums.Application;
using System;


namespace CRMData.Configurations.Attributes.Identity
{
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = true)]
    public class ModuleClaims : Attribute
    {
        public ApplicationModule ApplicationModule { get; private set; }
        public ApplicationModuleSection ApplicationModuleSection { get; private set; }

        public ModuleClaims(ApplicationModule applicationModule, ApplicationModuleSection applicationModuleSection)
        {
            this.ApplicationModule = applicationModule;
            this.ApplicationModuleSection = applicationModuleSection;
        }
    }
}
