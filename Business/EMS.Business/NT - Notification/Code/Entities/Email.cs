using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Condesus.EMS.Business.NT.Entities
{
    public class Email
    {
        private SmtpClient clienteSmtp;
        private String _host;
        private String _userName;
        private String _password;
        private String _EmailFrom;

        internal Email()
        {
            Entities.NotificationConfiguration _NotifConfig = new NT.Collections.NotificationConfigurations().Item();
            _host = _NotifConfig.Host.ToString();
            _userName = _NotifConfig.UserName.ToString();
            _password = _NotifConfig.Password.ToString();
            _EmailFrom = _NotifConfig.EmailSender.ToString();

            //Server
            clienteSmtp = new SmtpClient(_host);
            // carga usuario y  contraseña si tiene
            if (_userName == String.Empty)
            {
                clienteSmtp.Credentials = new NetworkCredential();
            }
            else
            {
                clienteSmtp.Credentials = new NetworkCredential(_userName, _password);
            }
        }

        //para la dll
        internal void PrepareMail(List<String> to, String subject, String  body)
        {

            
            //Recorre la lista de destinatarios y los carga
            foreach (String _to in to)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    //Carga el from                
                    msg.From = new MailAddress(_EmailFrom);

                    msg.To.Add(_to);
                    //msg.To.Add(new MailAddress("ruben.short@condesus.com.ar"));

                    //carga el motivo
                    msg.Subject = subject;

                    //Carga el body
                    msg.Body = body;

                    //envia el mensaje
                    clienteSmtp.Send(msg);

                }
                catch (Exception ex)
                {
                    //String _EmailLog = System.Configuration.ConfigurationManager.AppSettings["EmailLog"].ToString();
                    //new Code.Entities.Email().CreateMail(_EmailFrom, _EmailLog, "Fallo el envio de mail en el servicio - " + DateTime.Now + " subject: " + _msg.Subject, " body: " + _msg.Body + "system error: " + ex.Message);
                }
            }
        }

        internal Boolean CreateMail(String from, String to, String subject, String body)
        {
            MailMessage msg = new MailMessage();
            //Carga el from                
            msg.From = new MailAddress(from);

            //msg.To.Clear();
            msg.To.Add(to);
            //msg.To.Add(new MailAddress("ruben.short@condesus.com.ar"));

            //carga el motivo
            msg.Subject = subject;

            //Carga el body
            msg.Body = body;

            //llama al metodo q envia el mail
            return SendMail(msg);
        }

        internal Boolean SendMail(MailMessage mail)
        {
            try
            {
                //envia el mensaje
                clienteSmtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //String _EmailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailFrom"].ToString();
                //String _EmailLog = System.Configuration.ConfigurationManager.AppSettings["EmailLog"].ToString();
                //new Code.Entities.Email().CreateMail(_EmailFrom, _EmailLog, "Fallo el envio de mail en el servicio - " + DateTime.Now, ex.Message);
            }
        }


    }
}

