using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace BookStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "test@gmail.com";
        public string MailFromAddress = "test@gmail.com";
        public bool UseSSL = true;
        public string UserName = "test@gmail.com";
        public string Password = "test";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\BookStore\Order";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings emailSettingsParam)
        {
            emailSettings = emailSettingsParam;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using(var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSSL;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder message = new StringBuilder();
                message.AppendLine("A new order has been submitted")
                       .AppendLine("----------")
                       .AppendLine("Books:");
                foreach (var item in cart.Lines)
                {
                    var subtotal = item.Book.Price * item.Quantity;
                    message.AppendFormat("{0} x {1} (subtotal: {2:c})", item.Quantity, item.Book.Price, subtotal)
                           .AppendLine("");
                }
                message.AppendLine("")
                       .AppendLine("----------")
                       .AppendFormat("Total Order Value {0:c}", cart.ComputeTotalLine())
                       .AppendLine("")
                       .AppendLine("----------")
                       .AppendLine("")
                       .AppendLine("Shiped To :")
                       .AppendLine(shippingDetails.Name)
                       .AppendLine(shippingDetails.Line1);
                if (shippingDetails.Line2 != null)
                        {
                            message.AppendLine(shippingDetails.Line2);
                        }
                message.AppendLine(shippingDetails.City)
                       .AppendLine(shippingDetails.State)
                       .AppendLine(shippingDetails.Country)
                       .AppendLine("----------")
                       .AppendFormat("Gift Wrap : {0}" , shippingDetails.GiftWrap ? "Yes" : "No");
                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress, "New Order Submited", message.ToString());
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                try {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                
            }
        }
    }
}
