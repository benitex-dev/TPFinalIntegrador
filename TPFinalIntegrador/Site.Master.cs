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

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Remove("usuario");
            Response.Redirect("/Login");
        }
    }
}