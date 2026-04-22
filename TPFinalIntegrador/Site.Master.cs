using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalIntegrador
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string modoActual = Session["ModoVista"] != null ? Session["ModoVista"].ToString() : "Personal";

            if (modoActual == "Personal")
            {
                btnVistaPersonal.CssClass = "dropdown-item text-primary fw-bold";
            }
            else
            {
                btnVistaPersonal.CssClass = "dropdown-item text-dark";
            }

            if (Session["usuario"] != null)
            {
                divSesionIniciada.Visible = true;
                divInvitado.Visible = false;
                divMenuPrincipal.Visible = true;

                Usuario usuarioIniciado = (Usuario)Session["usuario"];
                if (!string.IsNullOrEmpty(usuarioIniciado.ImagenURL))
                    imgNavbar.ImageUrl = "~/Imagenes/" + usuarioIniciado.ImagenURL + "?v=" + DateTime.Now.Ticks;
                else
                    imgNavbar.ImageUrl = "~/Imagenes/default-avatar.png";
                lblNombreNavbar.Text = usuarioIniciado.Nombre + " " + usuarioIniciado.Apellido;
            }
            else
            {
                divSesionIniciada.Visible = false;
                divInvitado.Visible = true;
                divMenuPrincipal.Visible = false;
            }

            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    CargarHogaresDelUsuario();
                }
            }
            // Llamar siempre para que la visibilidad de los links se ajuste según la página actual
            SetNavButtonsVisibility();
        }

        private void SetNavButtonsVisibility()
        {
            // Obtener el nombre de archivo (puede venir con o sin extensión según la ruta)
            string rawFileName = VirtualPathUtility.GetFileName(Request.Path ?? string.Empty) ?? string.Empty;
            string fileNameNoExt = Path.GetFileNameWithoutExtension(rawFileName);

            // Comparación case-insensitive contra los nombres de página sin extensión
            bool mostrarAgregar = fileNameNoExt.Equals("Inicio", StringComparison.OrdinalIgnoreCase)
                               || fileNameNoExt.Equals("Personalizaciones", StringComparison.OrdinalIgnoreCase);

            if (lnkAgregarCategoria != null)
                lnkAgregarCategoria.Visible = mostrarAgregar;

            if (lnkAgregarMedio != null)
                lnkAgregarMedio.Visible = mostrarAgregar;
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Remove("usuario");
            Response.Redirect("/Login");
        }

        private void CargarHogaresDelUsuario()
        {
            Usuario usuarioIniciado = (Usuario)Session["usuario"];
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"SELECT h.IdHogar, h.Nombre 
                         FROM HOGAR h
                         INNER JOIN HOGAR_USUARIO hu ON h.IdHogar = hu.IdHogar
                         WHERE hu.IdUsuario = @IdUsuario AND h.Estado = 1";

                datos.setConsulta(query);
                datos.setParametro("@IdUsuario", usuarioIniciado.IdUsuario);

                datos.ejecutarLectura();

                rptHogares.DataSource = datos.Lector;
                rptHogares.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Evento: Cuando el usuario hace clic en "Vista Personal"
        protected void btnVistaPersonal_Click(object sender, EventArgs e)
        {
            Session["ModoVista"] = "Personal";
            Session["IdHogarActual"] = null;
            Session["HogarSeleccionado"] = null;
            Response.Redirect("Inicio.aspx");
        }

        // Evento: Cuando el usuario hace clic en algún hogar generado por el Repeater
        protected void rptHogares_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarHogar")
            {
                int idHogarSeleccionado = Convert.ToInt32(e.CommandArgument);

                HogarNegocio hogarSeleccionado = new HogarNegocio();
                Session["HogarSeleccionado"] = hogarSeleccionado.listarUno(idHogarSeleccionado);
                Session["ModoVista"] = "Hogar";
                Session["IdHogarActual"] = idHogarSeleccionado;
                Response.Redirect("Inicio.aspx");
            }
        }

        protected void rptHogares_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnHogarActual = (LinkButton)e.Item.FindControl("btnHogar");

                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    string idHogarSeleccionado = Session["IdHogarActual"].ToString();

                    if (btnHogarActual.CommandArgument == idHogarSeleccionado)
                    {
                        btnHogarActual.CssClass = "dropdown-item text-primary fw-bold";
                    }
                    else
                    {
                        btnHogarActual.CssClass = "dropdown-item text-dark";
                    }
                }
            }
        }

        protected void btnPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
    }
}