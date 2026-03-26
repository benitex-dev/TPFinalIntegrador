using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace negocio
{
    public class GeoHelper
    {
        public static string ObtenerUbicacion(string ip)
        {
            try
            {
                string url = $"http://ip-api.com/line/{ip}?fields=city,country";
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string[] datos = reader.ReadToEnd().Split('\n');

                    string ciudad = datos[0];
                    string pais = datos[1];

                    return $"{ciudad}, {pais}";
                }
            }
            catch
            {
                return "Ubicación no disponible";
            }
        }
    }
}
