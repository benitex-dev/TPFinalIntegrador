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
            }
            else
            {
                divSesionIniciada.Visible = false;
                divInvitado.Visible = true;
            }

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            if (Session["usuario"] != null)
            {
                Usuario usuarioIniciado = (Usuario)Session["usuario"];
                imgNavbar.ImageUrl = "~/Imagenes/" + ((Usuario)Session["usuario"]).ImagenURL + "?v=" + DateTime.Now.Ticks;
                btnSesionIniciada.InnerText = usuarioIniciado.Nombre + " " + usuarioIniciado.Apellido;
            }

            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    CargarHogaresDelUsuario();
                }
            }

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
                Session["HogarSeleccionado"] = hogarSeleccionado.Listar(idHogarSeleccionado);
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