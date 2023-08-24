using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Net.PeerToPeer;
using System.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace WebSecureBookings
{
    public class EnvioDeCorreoController
    {
        
        string body = $"<body>\r\n    <div class=\"container\">\r\n        <div class=\"header\">\r\n            <h1>Bienvenido a Web Securete Booking</h1>\r\n        </div>\r\n        <div class=\"content\">\r\n            <p>Hola {{userName}},</p>\r\n            <p>¡Bienvenido a nuestro sistema! Agradecemos que te hayas registrado con nosotros.</p>\r\n            <p>Esperamos que disfrutes de todas las funcionalidades que ofrecemos.</p>\r\n            <p>Gracias por unirte a nuestra comunidad.</p>\r\n            <p>Saludos,</p>\r\n            <p>El equipo de Web Securete Booking</p>\r\n        </div>\r\n        <div class=\"footer\">\r\n            <p>Este correo es automático, por favor no lo respondas.</p>\r\n        </div>\r\n    </div>\r\n</body>";

        private static void Main()
        {
            Execute().Wait();
        }

        public static async Task Execute()
        {
            string sApiKey = ConfigurationManager.AppSettings["api_sendgrid"];
            var apiKey = Environment.GetEnvironmentVariable(sApiKey);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("1220100044@alumnos.utng.edu.mx", "WebSecureteBooking");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("jm4885459@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync());
        }

        public async Task<bool> EnviarCorreoBienvenidaAsync(string toAddress, string userName, string sToken)
        {
            try
            {
                //string sApiKey = ConfigurationManager.AppSettings["api_sendgrid"];
                //var client = new SendGridClient(sApiKey);

                //// Crear el contenido del correo
                //string body = $@"<body><b>El token es el siguiente: </b>{sToken}</body>";

                //// Agregar el resultado de la búsqueda de cuenta al cuerpo del correo
                //body += $@"<p>El idUsuario asociado a la cuenta es: {userName}</p>";

                //var from = new EmailAddress("1220100044@alumnos.utng.edu.mx", "WebSecureteBooking");
                //var subject = "Recuperación de Cuenta";
                //var to = new EmailAddress(toAddress, userName);
                //var plainTextContent = "Esperemos que recupere su cuenta";
                //var htmlContent = body;

                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);

                //// Retorna información sobre el resultado del envío del correo
                //return true;

                string sApiKey = "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";//ConfigurationManager.AppSettings["api_sendgrid"];
                var client = new SendGridClient(sApiKey);
                var from = new EmailAddress("1220100044@alumnos.utng.edu.mx", "WebSecureteBooking");
                var subject = "Recuperación de Cuenta";
                var to = new EmailAddress(toAddress, userName);
                var plainTextContent = "Esperemos que recupere su cuenta";
                var htmlContent = $@"<body><b>El token es el siguiente: </b>{sToken}</body>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Body.ReadAsStringAsync());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //public async Task EnviarCorreoBienvenidaAsync(string toAddress, string userName, string sToken)
        //{
        //    try
        //    {
        //        string sApiKey = ConfigurationManager.AppSettings["api_sendgrid"];
        //        //var apiKey = Environment.GetEnvironmentVariable("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");
        //        var client = new SendGridClient(sApiKey);

        //        // Crear el contenido del correo
        //        string body = $@"<body><b>El token es el siguiente: </b>{sToken}</body>";

        //        var from = new EmailAddress("1220100044@alumnos.utng.edu.mx", "WebSecureteBooking");
        //        var subject = "Recuperación de Cuenta";
        //        var to = new EmailAddress(toAddress, userName);
        //        var plainTextContent = "Esperemos que recupere su cuenta";
        //        var htmlContent = body;

        //        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //        var response = await client.SendEmailAsync(msg);

        //        Console.WriteLine(response.StatusCode);
        //        Console.WriteLine(response.Body.ReadAsStringAsync());
        //    }catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //public void EmailSender(string toAddress, string userName)
        //{
        //    try
        //    {
        //        var client = new SendGridClient(apiKey);
        //        var from = new EmailAddress("1220100044@alumnos.utng.edu.mx");
        //        var to = new EmailAddress(toAddress);
        //        var msg = MailHelper.CreateSingleEmail(from, to, userName, "", body);
        //        client.SendEmailAsync(msg);
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        //public void SendWelcomeEmail(string toAddress, string userName)
        //{
        //    string body = $"<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    " +
        //        $"<style>\r\n        body {{\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f4f4f4;\r\n        }}\r\n\r\n        .container {{\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            padding: 20px;\r\n            background-color: #ffffff;\r\n            border-radius: 5px;\r\n            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);\r\n        }}\r\n\r\n        .header {{\r\n            background-color: #3385d9;\r\n            color: #ffffff;\r\n            padding: 10px 0;\r\n            text-align: center;\r\n            border-radius: 5px 5px 0 0;\r\n        }}\r\n\r\n        .content {{\r\n            padding: 20px;\r\n        }}\r\n\r\n        .footer {{\r\n            background-color: #f4f4f4;\r\n            color: #333333;\r\n            padding: 10px 0;\r\n            text-align: center;\r\n            border-radius: 0 0 5px 5px;\r\n        }}\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <div class=\"header\">\r\n            <h1>Bienvenido a Web Securete Booking</h1>\r\n        </div>\r\n        <div class=\"content\">\r\n            <p>Hola {{userName}},</p>\r\n            <p>¡Bienvenido a nuestro sistema! Agradecemos que te hayas registrado con nosotros.</p>\r\n            <p>Esperamos que disfrutes de todas las funcionalidades que ofrecemos.</p>\r\n            <p>Gracias por unirte a nuestra comunidad.</p>\r\n            <p>Saludos,</p>\r\n            <p>El equipo de Web Securete Booking</p>\r\n        </div>\r\n        <div class=\"footer\">\r\n            <p>Este correo es automático, por favor no lo respondas.</p>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n";
        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        //    smtpClient.Credentials = new NetworkCredential("racieriver@gmail.com", "adshrkjdptmxebyk");
        //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpClient.EnableSsl = true;
        //    smtpClient.UseDefaultCredentials = false;

        //    MailMessage message = new MailMessage();
        //    message.From = new MailAddress("racieriver@gmail.com", "WebSecureteBooking");
        //    message.To.Add(new MailAddress(toAddress));
        //    message.Subject = "¡Bienvenido al sistema Web Securete Booking!";
        //    message.IsBodyHtml = true;
        //    message.Body = body;

        //    smtpClient.Send(message);
        //    //try
        //    //    {
        //    //        string fromAddress = "racieriver@gmail.com"; // Dirección de correo del remitente
        //    //        string subject = "¡Bienvenido al sistema Web Securete Booking!";
        //    //        string body = $"Hola {userName},\n\n" +
        //    //                  "¡Bienvenido a nuestro sistema! Agradecemos que te hayas registrado con nosotros.\n" +
        //    //                  "Esperamos que disfrutes de todas las funcionalidades que ofrecemos.\n\n" +
        //    //                  "Gracias por unirte a nuestra comunidad.\n\n" +
        //    //                  "Saludos,\n" +
        //    //                  "El equipo de nuestro sistema";


        //    //        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        //    //        {
        //    //            Port = 587,
        //    //            Credentials = new NetworkCredential("1220100044@alumnos.utng.edu.mx", "&Lq}kDx=7E{SM4V"), // Cambia esto por tu correo y contraseña
        //    //            EnableSsl = true,
        //    //        };

        //    //        MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

        //    //        smtpClient.Send(mailMessage);

        //    //        Console.WriteLine("Correo enviado con éxito.");
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        Console.WriteLine("Error al enviar el correo: " + ex.Message);
        //    //    }
        //    //}
        //}
    }
}