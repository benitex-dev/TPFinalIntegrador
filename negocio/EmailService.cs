using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Negocio
{
    public enum TipoCorreo
    {
        //ACA VAN LOS NOMBRES DE LAS PLANTILLAS HTML 
        //PARA LUEGO USARLO EN EL ENVIO AUTOMATICO
    }

    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService() // GMAIL
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("clinicamedicameraki@gmail.com", "yfsyxonjlbamovxg");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        /*
        public EmailService() // MAILTRAP
        {
            server = new SmtpClient("sandbox.smtp.mailtrap.io", 2525);
            server.Credentials = new NetworkCredential("f8e02177848b48", "acbac658d54cae");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "sandbox.smtp.mailtrap.io";
        }
        */

        public void armarCorreo(string destino, string asunto, Dictionary<string, string> reemplazos, TipoCorreo tipo, string rutaBasePlantillas)
        {
            email = new MailMessage();
            email.From = new MailAddress("gestiondeturnos@clinica.com", "Clínica Médica Meraki");
            email.To.Add(destino);
            email.Subject = asunto;
            email.IsBodyHtml = true;

            string plantilla = ObtenerPlantilla(tipo, reemplazos, rutaBasePlantillas);
            email.Body = plantilla;
        }

        public void enviarCorreo()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtenerPlantilla(TipoCorreo tipo, Dictionary<string, string> reemplazos, string rutaBasePlantillas)
        {
            string archivo = tipo.ToString() + ".html";
            string ruta = Path.Combine(rutaBasePlantillas, archivo);

            if (!File.Exists(ruta))
                throw new FileNotFoundException("No se encontró la plantilla: " + ruta);

            string html = File.ReadAllText(ruta);

            foreach (var item in reemplazos)
            {
                html = html.Replace($"{{{{{item.Key}}}}}", item.Value); // Reemplaza {{CLAVE}} con su valor
            }

            return html;
        }

        public static bool EsEmailValido(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@");
        }

    }
}

