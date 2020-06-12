using System;
using System.Collections.Generic;
using System.Text;

namespace CompWeb.Configurations.Email
{
    public interface IEmailService
    {
        public void SendForgetPasswordToken(string userEmail, string code);
        public void SendEmailConfirmationToken(string userEmail, string code);
    }
}
