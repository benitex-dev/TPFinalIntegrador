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
                            string url = "~/Imagenes/" + usuario.ImagenURL + "?v=" + DateTime.Now.Ticks;
                            imagenNuevoPerfil.ImageUrl = url;
                            imagenMiPerfil.ImageUrl = url;
                            Image img = (Image)Master.FindControl("imgNavbar");
                            if (img != null) img.ImageUrl = url;

                        }
                    }
                    
                    if (Session["Mensaje"] != null)
                    {
                        lblMensaje.Text = Session["Mensaje"].ToString();
                        lblMensaje.CssClass = "alert alert-success d-block";
                        Session.Remove("Mensaje"); // lo limpiás para que no aparezca en el próximo load
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
                if (txtImagen.PostedFile != null && txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Imagenes/");
                    string nombreArchivo = "perfil-" + usuario.IdUsuario + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + nombreArchivo);
                    usuario.ImagenURL = nombreArchivo;

                }
                //datos personales
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                usuario.FechaNac = DateTime.Parse(txtFechaNac.Text);
                //modificar el usuario
                usuarioNegocio.ModificarUsuario(usuario);
                // Actualizar sesión
                Session["usuario"] = usuario;
                
                // Actualizar imágenes en la página
                if (!string.IsNullOrEmpty(usuario.ImagenURL))
                {
                    string url = "~/Imagenes/" + usuario.ImagenURL + "?v=" + DateTime.Now.Ticks;
                    imagenMiPerfil.ImageUrl = url;
                    imagenNuevoPerfil.ImageUrl = url;
                    Image img = (Image)Master.FindControl("imgNavbar");
                    if (img != null) img.ImageUrl = url;
                }

                lblMensaje.Text = "Perfil actualizado correctamente.";
                lblMensaje.CssClass = "alert alert-success d-block";
               


            }
            catch (Exception ex)
            {

                Session.Add("error", Security.ManejoError(ex));
                Response.Redirect("Error.aspx");

            }
        }

        

        protected void lnkCambiarContrasenia_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("ModificarContrasenia.aspx", false);
        }
    }
}