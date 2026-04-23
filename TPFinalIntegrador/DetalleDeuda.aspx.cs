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
                int idDeuda;
                if (!int.TryParse(Request.QueryString["id"], out idDeuda) || idDeuda <= 0)
                {
                    Response.Redirect("Deudores.aspx", false);
                    return;
                }
                CargarDetalle(idDeuda);
                CargarCuotas(idDeuda);
            }
        }
        private void CargarDetalle(int idDeuda)
        {
            DeudaNegocio negocio = new DeudaNegocio();
            Deuda deuda = negocio.ObtenerPorId(idDeuda);

            CuotaDeudaNegocio cuotaNegocio = new CuotaDeudaNegocio();
            List<CuotaDeuda> cuotas = cuotaNegocio.ListarPorDeuda(idDeuda);

            decimal montoPagado = cuotas
         .Where(c => c.Estado == EstadoCuota.Pagada)
         .Sum(c => c.Monto);

            decimal montoPendiente = cuotas
                .Where(c => c.Estado == EstadoCuota.Pendiente)
                .Sum(c => c.Monto);


            lblNombre.Text = deuda.NombreDeudor;
            lblDescripcion.Text = deuda.Descripcion;
            lblMontoTotal.Text = deuda.MontoTotal.ToString("C");
            lblCuotas.Text = deuda.Cuotas.HasValue ? deuda.Cuotas.Value.ToString() : "0";
            if (deuda.Cuotas.HasValue && deuda.Cuotas.Value > 0)
            {
                lblMontoCuota.Text = (deuda.MontoTotal / deuda.Cuotas.Value).ToString("C");
            }
            else
            {
                lblMontoCuota.Text = "$ 0,00";
            }
            lblMontoPagado.Text = montoPagado.ToString("C");
            lblMontoPendiente.Text = montoPendiente.ToString("C");


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

                    // Marcar cuota como pagada
                    CuotaDeudaNegocio cuotaNegocio = new CuotaDeudaNegocio();
                    cuotaNegocio.MarcarPagada(idCuotaDeuda);

                    // Obtener datos de la cuota
                    CuotaDeuda cuota = cuotaNegocio.ObtenerPorId(idCuotaDeuda);
                    Deuda deuda = Session["deudaActual"] as Deuda;
                    if (deuda == null)
                    {
                        Response.Redirect("Deudores.aspx", false);
                        return;
                    }
                    Usuario usuario = (Usuario)Session["usuario"];

                    // Obtener o crear categoría "Cobro de deuda"
                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    Categoria categoria = categoriaNegocio.ObtenerOCrearCobroDeuda(usuario.IdUsuario);

                    // Registrar ingreso
                    Ingreso ingreso = new Ingreso();
                    ingreso.Descripcion = "Cobro cuota " + cuota.NumeroCuota + " - " + deuda.NombreDeudor;
                    ingreso.Fecha = DateTime.Today;
                    ingreso.Monto = cuota.Monto;
                    ingreso.Categoria = categoria;
                    ingreso.Usuario = usuario;
                    ingreso.Estado = true;

                    IngresoNegocio ingresoNegocio = new IngresoNegocio();
                    ingresoNegocio.AgregarIngreso(ingreso);

                    CargarCuotas(deuda.IdDeuda);
                    CargarDetalle(deuda.IdDeuda);
                    VerificarDeudaSaldada(cuotaNegocio, deuda);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                        $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
                }
            }
        }
        private void VerificarDeudaSaldada(CuotaDeudaNegocio cuotaNegocio, Deuda deuda)
        {
            List<CuotaDeuda> todasLasCuotas = cuotaNegocio.ListarPorDeuda(deuda.IdDeuda);
            bool todasPagadas = todasLasCuotas.TrueForAll(c => c.Estado == EstadoCuota.Pagada);

            if (todasPagadas)
            {
                DeudaNegocio deudaNegocio = new DeudaNegocio();
                deudaNegocio.MarcarPagada(deuda.IdDeuda);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Deuda saldada!', text: 'Todas las cuotas fueron pagadas. La deuda quedó registrada comopaga.'});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Cuota marcada como pagada e ingreso registrado.'});", true);
            }
        }
    }
}