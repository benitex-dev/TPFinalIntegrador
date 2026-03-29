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
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Security.isLogin(Session["usuario"]))
                    {
                        Usuario usuario = (Usuario)Session["usuario"];
                        txtEmail.Text = usuario.Email;
                        txtEmail.ReadOnly = true;

                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        txtFechaNac.Text =usuario.FechaNac.ToString("yyyy-MM-dd");

                        if (!string.IsNullOrEmpty(usuario.ImagenURL))
                        {
                            imagenNuevoPerfil.ImageUrl = "~/Imagenes/" + usuario.ImagenURL;
                            imagenMiPerfil.ImageUrl = "~/Imagenes/" + usuario.ImagenURL;
                            Image img = (Image)Master.FindControl("imgNavbar");
                            img.ImageUrl = "~/Imagenes/" + usuario.ImagenURL;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", Security.ManejoError(ex));
                Response.Redirect("Error.aspx");
            }


        }

      

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx",false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {   //Escribir IMG
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuario = (Usuario)Session["usuario"];
                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Imagenes/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.IdUsuario + ".jpg");
                    usuario.ImagenURL = "perfil-" + usuario.IdUsuario + ".jpg";

                }

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                //modificar el usuario
                usuarioNegocio.ModificarUsuario(usuario);

                //Leer de la IMG
                Image img = (Image)Master.FindControl("imgNavbar");
                img.ImageUrl = "~/Imagenes/" + usuario.ImagenURL;


            }
            catch (Exception ex)
            {

                Session.Add("error", Security.ManejoError(ex));
                Response.Redirect("Error.aspx");

            }
        }
    }
}