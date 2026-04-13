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
    public partial class Presupuesto : System.Web.UI.Page
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
                CargarPresupuesto();
            }
        }

        private void CargarPresupuesto()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            PresupuestoCategoriaNegocio negocio = new PresupuestoCategoriaNegocio();

            gvPresupuesto.DataSource = negocio.ListarPorUsuarioYMes(usuario.IdUsuario, MesSeleccionado, AnioSeleccionado);
            gvPresupuesto.DataBind();

            lblMesActual.Text = new DateTime(AnioSeleccionado, MesSeleccionado, 1)
                .ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));
        }
        protected void btnMesAnterior_Click(object sender, EventArgs e)
        {
            MesSeleccionado--;
            if (MesSeleccionado < 1) { MesSeleccionado = 12; AnioSeleccionado--; }
            CargarPresupuesto();
        }

        protected void btnMesSiguiente_Click(object sender, EventArgs e)
        {
            MesSeleccionado++;
            if (MesSeleccionado > 12) { MesSeleccionado = 1; AnioSeleccionado++; }
            CargarPresupuesto();
        }
        protected void gvPresupuesto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "GuardarPresupuesto") return;

            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow fila = gvPresupuesto.Rows[index];

                TextBox txtMonto = (TextBox)fila.FindControl("txtPresupuesto");
                HiddenField hfId = (HiddenField)fila.FindControl("hfIdCategoria");

                if (string.IsNullOrWhiteSpace(txtMonto.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Ingresá un monto.'});", true);
                    return;
                }

                decimal monto = decimal.Parse(txtMonto.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);

                if (monto <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'El monto debe ser mayor a cero.'});", true);
                    return;
                }

                Usuario usuario = (Usuario)Session["usuario"];

                PresupuestoCategoria presupuesto = new PresupuestoCategoria();
                presupuesto.Categoria = new Categoria { IdCategoria = int.Parse(hfId.Value) };
                presupuesto.Usuario = usuario;
                presupuesto.Mes = MesSeleccionado;
                presupuesto.Anio = AnioSeleccionado;
                presupuesto.MontoPresupuestado = monto;

                PresupuestoCategoriaNegocio negocio = new PresupuestoCategoriaNegocio();
                negocio.Guardar(presupuesto);

                CargarPresupuesto();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Presupuesto guardado.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }
        protected string GenerarBarra(object montoPresupuestado, object gastoReal)
        {
            decimal presupuesto = (decimal)montoPresupuestado;
            decimal gasto = (decimal)gastoReal;

            if (presupuesto <= 0)
                return "<span class='text-muted'>Sin presupuesto</span>";

            int porcentaje = (int)Math.Min((gasto / presupuesto) * 100, 100);
            string color = porcentaje >= 100 ? "bg-danger" : porcentaje >= 80 ? "bg-warning" : "bg-success";
            string alerta = gasto > presupuesto ? " <span class='text-danger'>⚠️ Excedido</span>" : "";

            return $@"
                  <div class='progress' style='min-width:120px'>
                      <div class='progress-bar {color}' style='width:{porcentaje}%'>{porcentaje}%</div>
                  </div>{alerta}";
        }

    }
}