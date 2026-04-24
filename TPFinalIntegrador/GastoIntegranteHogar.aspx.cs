using negocio;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalIntegrador
{
    public partial class GastoIntegranteHogar : System.Web.UI.Page
    {
        public int MesSeleccionado
        {
            get
            {
                if (ViewState["Mes"] != null) return (int)ViewState["Mes"];
                return DateTime.Now.Month;
            }
            set { ViewState["Mes"] = value; }
        }

        public int AnioSeleccionado
        {
            get
            {
                if (ViewState["Anio"] != null) return (int)ViewState["Anio"];
                return DateTime.Now.Year;
            }
            set { ViewState["Anio"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {   
            

            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                MesSeleccionado = DateTime.Now.Month;
                AnioSeleccionado = DateTime.Now.Year;
            }

            lblMesActual.Text = new DateTime(AnioSeleccionado, MesSeleccionado, 1)
                .ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));

            if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
            {
                lblSinHogar.Visible = false;
                pnlContenido.Visible = true;
                CargarGastos();
            }
            else
            {
                lblSinHogar.Visible = true;
                pnlContenido.Visible = false;
            }
        }

        private void CargarGastos()
        {
            int idHogar = (int)Session["IdHogarActual"];

            GastoNegocio negocio = new GastoNegocio();
            List<GastoPorIntegrante> lista = negocio.ListarGastosPorIntegranteYCategoria(idHogar, MesSeleccionado, AnioSeleccionado);

            if (lista.Count == 0)
            {
                lblSinGastos.Visible = true;
                rptIntegrantes.Visible = false;
                return;
            }

            lblSinGastos.Visible = false;
            rptIntegrantes.Visible = true;
            rptIntegrantes.DataSource = lista;
            rptIntegrantes.DataBind();
        }

        protected void rptIntegrantes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {   

                GastoPorIntegrante integrante = new GastoPorIntegrante();
                integrante = (GastoPorIntegrante)e.Item.DataItem;


                Repeater rptCategorias = (Repeater)e.Item.FindControl("rptCategorias");
                rptCategorias.DataSource = integrante.Categorias;
                rptCategorias.DataBind();
            }
        }

        protected void btnMesAnterior_Click(object sender, EventArgs e)
        {
            MesSeleccionado--;
            if (MesSeleccionado < 1) { MesSeleccionado = 12; AnioSeleccionado--; }
            lblMesActual.Text = new DateTime(AnioSeleccionado, MesSeleccionado, 1)
                .ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));
            CargarGastos();
        }

        protected void btnMesSiguiente_Click(object sender, EventArgs e)
        {
            MesSeleccionado++;
            if (MesSeleccionado > 12) { MesSeleccionado = 1; AnioSeleccionado++; }
            lblMesActual.Text = new DateTime(AnioSeleccionado, MesSeleccionado, 1)
                .ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));
            CargarGastos();
        }
    }
}