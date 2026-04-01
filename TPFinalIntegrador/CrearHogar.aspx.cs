using dominio;
using negocio;
using System;
using System.Web.UI;
using System.Collections.Generic;

namespace TPFinalIntegrador
{
    public partial class CrearHogar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuario();
            }
        }

        private void CargarUsuario()
        {
            try
            {
                

                var usuarioActual = (Usuario)Session["usuario"];

              if (usuarioActual == null) {
                    Response.Redirect("Login.aspx", false);
                    return;
                }
                txtNombreUsuario.ReadOnly=true;
                txtApellidoUsuario.ReadOnly=true;
                txtEmailUsuario.ReadOnly=true;
                txtNombreUsuario.Text = usuarioActual.Nombre;
                txtApellidoUsuario.Text = usuarioActual.Apellido;
                txtEmailUsuario.Text = usuarioActual.Email;
               
               



            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar usuario: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                lblMensaje.CssClass = "";

                string nombre = txtNombre.Text?.Trim();
                var usuarioActual = (Usuario)Session["usuario"];

                if (string.IsNullOrWhiteSpace(nombre))
                    throw new Exception("El nombre del hogar es obligatorio.");

               

                Hogar nuevo = new Hogar
                {
                    Nombre = nombre,
                    
                    Estado = chkEstado.Checked
                };

                HogarNegocio hogarNeg = new HogarNegocio();
                hogarNeg.AgregarHogar(nuevo,usuarioActual.IdUsuario);

                lblMensaje.Text = "Hogar creado correctamente.";
                lblMensaje.CssClass = "text-success";

                /*--------------ENVIO DE MAIL----------------------*/
                string rutaPlantillas = Server.MapPath("~/Template");

                var reemplazos = new Dictionary<string, string>()
                {
                    { "NOMBRE_USUARIO", usuarioActual.Nombre },
                    { "NOMBRE_HOGAR", nuevo.Nombre }
                };

                EmailService servicio = new EmailService();

                servicio.armarCorreo(
                    usuarioActual.Email,
                    "Nuevo hogar creado correctamente",
                    reemplazos,
                    TipoCorreo.NuevoHogar,
                    rutaPlantillas
                );

                servicio.enviarCorreo();
                /*---------------------------------------------------------------*/
                // Limpiar formulario
                txtNombre.Text = "";
               
                chkEstado.Checked = true;
                // Redirigir a Inicio.aspx (seguir el mismo patrón que en CargarUsuario)
                Response.Redirect("Inicio.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se pudo crear el hogar: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }
        }
    }
}