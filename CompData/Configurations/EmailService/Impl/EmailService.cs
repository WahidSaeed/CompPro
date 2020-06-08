using System;
using System.Collections.Generic;
using System.Text;

namespace CRMData.Configurations.EmailService.Impl
{
    class EmailService : IEmailService
    {
        public EmailService() { 
        
        }
        
        public void SendEmailToOwner(Guid OwnerID, string FK_Insured_PolicyID)
        {
            throw new NotImplementedException();
        }
    }
}
