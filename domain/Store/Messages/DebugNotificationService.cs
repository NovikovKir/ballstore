﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Store.Messages
{
    public class DebugNotificationService : INotificationService
    {
        public void SendConfirmationCode(string cellPhone, int code)
        {
            Debug.WriteLine("Cell phone: {0}, code: {1:0000}.", cellPhone, code);
        }

        public void StartProcess(Order order)
        {
            using (var client = new SmtpClient("smtp.your-email-provider.com", 587))
            {
                client.Credentials = new NetworkCredential("your-email@example.com", "your-email-password");
                var message = new MailMessage("from@at.my.domain", "to@at.my.domain");
                message.Subject = "Заказ #" + order.Id;

                var builder = new StringBuilder();
                foreach (var item in order.Items)
                {
                    builder.Append("{0}, {1}", item.BallId, item.Count);
                    builder.AppendLine();
                }
                message.Body = builder.ToString();
                client.Send(message);
            }
        }
    }
}
