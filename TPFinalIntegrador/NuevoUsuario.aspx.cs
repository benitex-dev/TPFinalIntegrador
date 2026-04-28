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
    public partial class NuevoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtConfirmarPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtFechaNac.Text))
                {
                    lblMensaje.CssClass = "text-danger d-block text-center mt-4 fw-semibold";
                    lblMensaje.Text = "Completá todos los campos obligatorios.";
                    return;
                }

                if (txtPassword.Text.Trim() != txtConfirmarPassword.Text.Trim())
                {
                    lblMensaje.CssClass = "text-danger d-block text-center mt-4 fw-semibold";
                    lblMensaje.Text = "Las contraseñas no coinciden.";
                    return;
                }

                UsuarioNegocio negocioUsuario = new UsuarioNegocio();

                if (negocioUsuario.ExisteEmail(txtEmail.Text.Trim()))
                {
                    lblMensaje.CssClass = "text-danger d-block text-center mt-4 fw-semibold";
                    lblMensaje.Text = "Ya existe un usuario registrado con ese email.";
                    return;
                }

                Usuario nuevo = new Usuario();
                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Apellido = txtApellido.Text.Trim();
                nuevo.Email = txtEmail.Text.Trim();
                nuevo.Password = txtPassword.Text.Trim();
                nuevo.FechaNac = DateTime.Parse(txtFechaNac.Text);
                nuevo.ImagenURL = txtImagenURL.Text.Trim();
                nuevo.Estado = true;

                negocioUsuario.AgregarUsuario(nuevo);

                /*--------------ENVIO DE MAIL----------------------*/
                try
                {
                    string rutaPlantillas = Server.MapPath("~/Template");
                    var reemplazos = new Dictionary<string, string>()
                    {
                        { "NOMBRE_USUARIO", nuevo.Nombre },
                    };
                    EmailService servicio = new EmailService();
                    servicio.armarCorreo(
                        nuevo.Email,
                        "Te damos la bienvenida al Sistema de Control de Gastos",
                        reemplazos,
                        TipoCorreo.NuevoUsuario,
                        rutaPlantillas
                    );
                    servicio.enviarCorreo();
                }
                catch { }
                /*---------------------------------------------------------------*/
                // Auto-login
                Usuario creado = negocioUsuario.buscarMail(nuevo.Email);
                Session["usuario"] = creado;

                ScriptManager
                    .RegisterStartupScript(this, this.GetType(), "ok","Swal.fire({icon: 'success', title: '¡Bienvenido!', text: 'Tu cuenta fue creada correctamente.'}).then(() => { window.location.href = 'Inicio.aspx';}); ", true);


            }
            catch (Exception)
            {
                lblMensaje.CssClass = "text-danger d-block text-center mt-4 fw-semibold";
                lblMensaje.Text = "No se pudo crear el usuario.";
            }
        }

    }
}