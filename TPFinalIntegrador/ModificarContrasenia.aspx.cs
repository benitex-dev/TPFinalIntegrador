using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalIntegrador
{
    public partial class ModificarContrasenia : System.Web.UI.Page
    {
       private UsuarioNegocio usuarioNegocio;
        private Usuario usuarioLogueado;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("Login.aspx", false);


        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string actual = txtContraseniaActual.Text.Trim();
            string nueva = txtContraseniaNueva.Text.Trim();
            string confirmar = txtConfirmarContrasenia.Text.Trim();

            // Validaciones básicas
            if (string.IsNullOrEmpty(actual) || string.IsNullOrEmpty(nueva) || string.IsNullOrEmpty(confirmar))
            {
                MostrarMensaje("Todos los campos son obligatorios.", "danger");
                return;
            }

            if (nueva != confirmar)
            {
                MostrarMensaje("La nueva contraseña y la confirmación no coinciden.", "danger");
                return;
            }

            if (nueva.Length < 6)
            {
                MostrarMensaje("La nueva contraseña debe tener al menos 6 caracteres.", "danger");
                return;
            }
            
            // Obtener usuario de sesión
            usuarioLogueado =(Usuario) Session["Usuario"];

            // Verificar contraseña actual y actualizar
            bool contraseniaCorrecta = VerificarPassword(actual);
            if (!contraseniaCorrecta)
            {
                MostrarMensaje("La contraseña actual es incorrecta.", "danger");
                return;
            }
            ActualizarPassword(usuarioLogueado, nueva);
            usuarioLogueado.Password = nueva;
            Session["Usuario"] = usuarioLogueado;
            Session["Mensaje"] = "Contraseña actualizada correctamente.";
            Response.Redirect("Perfil.aspx", false);
            MostrarMensaje("Contraseña actualizada correctamente.", "success");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx", false);
        }
        private void MostrarMensaje(string texto, string tipo)
        {
            lblMensaje.Text = texto;
            lblMensaje.CssClass = $"alert alert-{tipo} d-block";
        }
        // --- Reemplazá estos dos métodos con tu capa de datos ---

        private bool VerificarPassword(string contraseniaActual)
        {
           
            usuarioLogueado = (Usuario)Session["Usuario"];
            return usuarioLogueado.Password == contraseniaActual;
        }

        private void ActualizarPassword(Usuario usuario, string contraseniaNueva)
        {
           usuarioNegocio = new UsuarioNegocio();
           usuarioNegocio.ActualizarPassword(usuario, contraseniaNueva);
        }
    }
}