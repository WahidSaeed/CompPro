using System;
using System.Collections.Generic;
using System.Text;

namespace CRMData.Configurations.GlobalConfigurationVariables
{
    public class EmailServiceConfiguration
    {
        public string SmtpUserName { get; set; } = "trakker\\noreply";
        public string SmtpPassword { get; set; } = "Trakker123";
        public string SmtpHost { get; set; } = "webmail.trakker.com.pk"; //"exchange1.trakker.com.pk"; 
        public int SmtpPort { get; set; } = 25;
        public bool IsSmtpSSL { get; set; } = true;
        public string FromEmail { get; set; } = "noreply@tpllife.com";
        public string FromFullName { get; set; } = "TPL Life Autonomous Email";
        public bool IsDefault { get; set; } = true;
    }
}
