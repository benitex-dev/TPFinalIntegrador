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
                // Le sacamos el 'active' y le ponemos texto azul y negrita
                btnVistaPersonal.CssClass = "dropdown-item text-primary fw-bold";
            }
            else
            {
                // Estado normal (letrita oscura normal)
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

                // Le pasamos el lector de datos al Repeater y le decimos que se dibuje (DataBind)
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
            // Cambiamos el switch de la sesión a modo individual
            Session["ModoVista"] = "Personal";
            Session["IdHogarActual"] = null;
            Response.Redirect("Inicio.aspx");
        }

        // Evento: Cuando el usuario hace clic en algún hogar generado por el Repeater
        protected void rptHogares_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Validamos que el clic venga de nuestro LinkButton con el CommandName "SeleccionarHogar"
            if (e.CommandName == "SeleccionarHogar")
            {
                // Rescatamos el ID del hogar que dejamos oculto en el CommandArgument
                int idHogarSeleccionado = Convert.ToInt32(e.CommandArgument);

                // Cambiamos el switch de la sesión a modo Hogar y guardamos qué hogar eligió
                Session["ModoVista"] = "Hogar";
                Session["IdHogarActual"] = idHogarSeleccionado;
                Response.Redirect("Inicio.aspx");
            }
        }

        protected void rptHogares_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Validamos que sea una fila de datos (y no la cabecera o el pie del repeater)
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 1. Buscamos el botón de ese renglón
                LinkButton btnHogarActual = (LinkButton)e.Item.FindControl("btnHogar");

                // 2. Revisamos si estamos en "Modo Hogar" y si hay un ID guardado
                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    // 3. Comparamos el ID de este botón con el ID de la sesión
                    string idHogarSeleccionado = Session["IdHogarActual"].ToString();

                    if (btnHogarActual.CommandArgument == idHogarSeleccionado)
                    {
                        // ¡Bingo! Le clavamos texto azul y negrita sin fondo azul
                        btnHogarActual.CssClass = "dropdown-item text-primary fw-bold";
                    }
                    else
                    {
                        // A los demás hogares los dejamos normal
                        btnHogarActual.CssClass = "dropdown-item text-dark";
                    }
                }
            }
        }
    }
}