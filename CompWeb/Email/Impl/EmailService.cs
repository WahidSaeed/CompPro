using CRMData.Configurations.GlobalConfigurationVariables;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Options;
using CRMData.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace CompWeb.Configurations.Email.Impl
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceConfiguration emailService;
        private readonly IOptions<AppSettingModel> _appSettings;
        private readonly IHttpContextAccessor _httpContext;

        public EmailService(IConfiguration configuration, IOptions<AppSettingModel> appsettings, IHttpContextAccessor httpContext)
        {
            emailService = configuration.Get<EmailServiceConfiguration>();
            _appSettings = appsettings;
            _httpContext = httpContext;
        }

        public void SendEmailConfirmationToken(string userEmail, string code)
        {
            try
            {
                if (string.IsNullOrEmpty(userEmail))
                    new ArgumentNullException("userEmail");

                string htmlBody = GetHTMLFromEndPoint("EmailConfirmationToken", code);
                sendEmail(userEmail, subject: "Email Confirmation for The Account", body: htmlBody);
            }
            catch (Exception ex)
            {

            }
        }

        public void SendForgetPasswordToken(string userEmail, string code)
        {
            try
            {
                if (string.IsNullOrEmpty(userEmail))
                    new ArgumentNullException("userEmail");

                string htmlBody = GetHTMLFromEndPoint("ForgetPasswordToken", code);
                sendEmail(userEmail, subject: "Reset Password Access Token", body: htmlBody);
            }
            catch (Exception ex)
            {

            }
        }

        private string GetHTMLFromEndPoint(string view, dynamic content = null)
        {
            string scheme = _httpContext.HttpContext.Request.Scheme;
            string host = _httpContext.HttpContext.Request.Host.Value;
            var requestURI = $"{scheme}://{host}/Mail/{view}/"; // string.Format("{0}{1}", _appSettings.Value.ViewEngineEndPoint, $"{view}/");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(requestURI, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var results = response.Content.ReadAsStringAsync().Result;
                    var _response = results;
                    return _response;
                }
                else
                {
                    var results = response.Content.ReadAsStringAsync().Result;
                    var _response = results;
                    return _response;
                }
            }
        }

        #region Core Email Functions
        private void sendEmail(string emailsToSend, string subject, string body)
        {
            try
            {
                sendEmail(new List<string>() { emailsToSend }, subject, body);
            }
            catch (Exception ex)
            {

            }
        }
        private async void sendEmail(List<string> emailsToSend, string subject, string body)
        {

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(emailService.FromFullName, emailService.FromEmail));
                message.To.AddRange(emailsToSend.Select(x => new MailboxAddress(x)));
                message.Subject = subject;
                message.Body = new BodyBuilder()
                {
                    HtmlBody = body
                }.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Connect(emailService.SmtpHost, emailService.SmtpPort, MailKit.Security.SecureSocketOptions.None);
                    client.Authenticate(emailService.SmtpUserName, emailService.SmtpPassword);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }

    //public static class ControllerExtensions
    //{
    //    public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, ICompositeViewEngine compositeViewEngine, bool partial = false)
    //    {
    //        if (string.IsNullOrEmpty(viewName))
    //        {
    //            viewName = controller.ControllerContext.ActionDescriptor.ActionName;
    //        }

    //        controller.ViewData.Model = model;

    //        using (var writer = new StringWriter())
    //        {
    //            IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(compositeViewEngine)) as ICompositeViewEngine;
    //            ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

    //            if (viewResult.Success == false)
    //            {
    //                return $"A view with the name {viewName} could not be found";
    //            }

    //            ViewContext viewContext = new ViewContext(
    //                controller.ControllerContext,
    //                viewResult.View,
    //                controller.ViewData,
    //                controller.TempData,
    //                writer,
    //                new HtmlHelperOptions()
    //            );

    //            await viewResult.View.RenderAsync(viewContext);

    //            return writer.GetStringBuilder().ToString();
    //        }
    //    }
    //}
}
