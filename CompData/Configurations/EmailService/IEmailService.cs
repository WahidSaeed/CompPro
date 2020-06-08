using System;
using System.Collections.Generic;
using System.Text;

namespace CRMData.Configurations.EmailService
{
    interface IEmailService
    {
        public void SendEmailToOwner(Guid OwnerID, string FK_Insured_PolicyID);
    }
}
