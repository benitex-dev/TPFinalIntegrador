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
    public partial class DetalleDeuda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                int idDeuda = int.Parse(Request.QueryString["id"]);
                CargarDetalle(idDeuda);
                CargarCuotas(idDeuda);  
            }
        }
        private void CargarDetalle(int idDeuda)
        {
            DeudaNegocio negocio = new DeudaNegocio();
            Deuda deuda = negocio.ObtenerPorId(idDeuda);

            lblNombre.Text = deuda.NombreDeudor;
            lblDescripcion.Text = deuda.Descripcion;
            lblMontoTotal.Text = deuda.MontoTotal.ToString("C");
            lblCuotas.Text = deuda.Cuotas.Value.ToString();
            lblMontoCuota.Text = (deuda.MontoTotal / deuda.Cuotas.Value).ToString("C");

            Session["deudaActual"] = deuda;
        }
        private void CargarCuotas(int idDeuda)
        {
            CuotaDeudaNegocio negocio = new CuotaDeudaNegocio();
            gvCuotas.DataSource = negocio.ListarPorDeuda(idDeuda);
            gvCuotas.DataBind();
        }

        protected void gvCuotas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pagar")
            {
                try
                {
                    int idCuotaDeuda = int.Parse(e.CommandArgument.ToString());
                    CuotaDeudaNegocio negocio = new CuotaDeudaNegocio();
                    negocio.MarcarPagada(idCuotaDeuda);

                    Deuda deuda = (Deuda)Session["deudaActual"];
                    CargarCuotas(deuda.IdDeuda);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                        "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Cuota marcada como pagada.'});", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                        $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
                }
            }
        }
    }
}