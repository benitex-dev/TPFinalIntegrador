using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace negocio
{
    public enum TipoCorreo
    {
        IniciodeSesion
    }

    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService() // GMAIL
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("gestiondegastos.g5@gmail.com", "uvgjjqwtkjankdgm");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void armarCorreo(string destino, string asunto, Dictionary<string, string> reemplazos, TipoCorreo tipo, string rutaBasePlantillas)
        {
            email = new MailMessage();
            email.From = new MailAddress("gestiondegastos@grupo5.com", "Gestion de turnos");
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

