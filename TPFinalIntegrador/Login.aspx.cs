using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio; 

namespace TPFinalIntegrador
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            UsuarioNegocio negocioUsuario = new UsuarioNegocio();
            Usuario usuarioLogueado = negocioUsuario.Login(email, password);

            if (usuarioLogueado != null)
            {
                Session["usuario"] = usuarioLogueado;

                /*--------------ENVIO DE MAIL----------------------*/
                string rutaPlantillas = Server.MapPath("~/Template");

                string ip = Request.UserHostAddress;

                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                    ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0].Trim();

                string ubicacion = GeoHelper.ObtenerUbicacion(ip);

                var reemplazos = new Dictionary<string, string>()
                {
                    { "NOMBRE_USUARIO", usuarioLogueado.Nombre },
                    { "EMAIL", usuarioLogueado.Email },
                    { "FECHA_HORA", DateTime.Now.ToString("dd/MM/yyyy HH:mm") },
                    { "IP", ip },
                    { "UBICACION", ubicacion }
                };

                EmailService servicio = new EmailService();

                servicio.armarCorreo(
                    usuarioLogueado.Email,
                    "Nuevo inicio de sesión detectado",
                    reemplazos,
                    TipoCorreo.IniciodeSesion,
                    rutaPlantillas
                );

                servicio.enviarCorreo();
                /*---------------------------------------------------------------*/

                Response.Redirect("Inicio.aspx", false);
            }
            else
            {
                lblError.Text = "Email o contraseña incorrectos.";
            }
        }
    }
}
