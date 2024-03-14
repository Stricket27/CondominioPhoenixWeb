using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesoADatos.ServicioEmail
{
    public abstract class Servicio
    {
        public Usuario Usuario { get; set; }

        private SmtpClient smtpClient;
        public string enviarEmail { get; set; }
        public string contrasenia { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public bool ssl { get; set; }

        List<string> Archivo = new List<string>();

        protected void InicializarSmtpClent()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(enviarEmail, contrasenia);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
        }

        public void EnviarEmail (string sujeto, string cuerpo, List<string> emailReciente)
        {
            var mensajeEmail = new MailMessage();
            try
            {
                mensajeEmail.From = new MailAddress(enviarEmail);
                foreach (string email in emailReciente)
                {
                    mensajeEmail.To.Add(email);
                }
                mensajeEmail.Subject = sujeto;
                mensajeEmail.Body = cuerpo;
                mensajeEmail.Priority = MailPriority.Normal;
                smtpClient.Send(mensajeEmail);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                mensajeEmail.Dispose();
                smtpClient.Dispose();
            }
        }

        public void EnviarArchivos(string sujeto, List<string> emailReciente)
        {
            var mensajeEmail = new MailMessage();
            try
            {
                //string filenameXML = @"Factura Electronica.xml";
                //Attachment dataXML = new Attachment(filenameXML, MediaTypeNames.Application.Octet);

                //string filenamePDF = @"Factura Electronica.pdf";
                //Attachment dataPDF = new Attachment(filenamePDF, MediaTypeNames.Application.Octet);

                mensajeEmail.From = new MailAddress(enviarEmail);
                foreach (string email in emailReciente)
                {
                    mensajeEmail.To.Add(email);
                }
                mensajeEmail.Subject = sujeto;
                mensajeEmail.Body = "Muy buenas, hemos resibido una nueva reservación, por favor verificar";
                mensajeEmail.Priority = MailPriority.Normal;
                //mensajeEmail.Attachments.Add(dataXML);
                //mensajeEmail.Attachments.Add(dataPDF);
                smtpClient.Send(mensajeEmail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                mensajeEmail.Dispose();
                smtpClient.Dispose();
            }
        }

    }
}
