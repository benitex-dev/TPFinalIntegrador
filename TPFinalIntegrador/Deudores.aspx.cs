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
    public partial class Deudores : System.Web.UI.Page
    {
       // Dictionary<string, int> estadosDeuda = new Dictionary<string, int>();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }
            if (!IsPostBack)
                CargarDeudas();
        }

        private void CargarDeudas()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            DeudaNegocio negocio = new DeudaNegocio();

            int filtro = ViewState["FiltroEstado"] != null ? (int)ViewState["FiltroEstado"] : -1;

            // Lista filtrada para mostrar (sin eliminados)
            List<Deuda> listaFiltrada = negocio.ListarPorUsuario(usuario.IdUsuario, filtro)
                .Where(d => d.Estado != EstadoDeuda.Eliminado).ToList();

            pnlSinDeudas.Visible = listaFiltrada.Count == 0;
            pnlDeudas.Visible = listaFiltrada.Count > 0;

            rptDeudas.DataSource = listaFiltrada;
            rptDeudas.DataBind();

            // Summary siempre sobre todas las deudas activas (sin filtro de estado)
            List<Deuda> todasActivas = negocio.ListarPorUsuario(usuario.IdUsuario, -1)
                .Where(d => d.Estado != EstadoDeuda.Eliminado).ToList();

            lblTotalPrestado.Text = "$" + todasActivas.Sum(d => d.MontoTotal).ToString("N0");
            lblTotalPendiente.Text = "$" + todasActivas.Sum(d => d.MontoPendiente).ToString("N0");
            lblTotalDeudores.Text = todasActivas.Count.ToString();

            if (filtro == 0) lblFiltroEstado.Text = "Cobrado";
            else if (filtro == 1) lblFiltroEstado.Text = "Pendiente";
            else lblFiltroEstado.Text = "Estado";
        }

        protected void FiltroEstado_Command(object sender, CommandEventArgs e)
        {
            ViewState["FiltroEstado"] = int.Parse(e.CommandArgument.ToString());
            CargarDeudas();
        }

        protected void btnBorrarFiltros_Click(object sender, EventArgs e)
        {
            ViewState["FiltroEstado"] = null;
            CargarDeudas();
        }

        protected void rptDeudas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idDeuda = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Eliminar")
            {
                try
                {
                    new DeudaNegocio().EliminarLogico(idDeuda);
                    CargarDeudas();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                        "Swal.fire({icon:'success',title:'¡Éxito!',text:'Deuda eliminada correctamente.'});", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                        $"Swal.fire({{icon:'error',title:'Error',text:'{ex.Message.Replace("'", "\\'")}'}});", true);
                }
            }
            else if (e.CommandName == "Editar")
            {
                Deuda deuda = new DeudaNegocio().ObtenerPorId(idDeuda);
                hfIdDeudaEditar.Value = idDeuda.ToString();
                txtNombreEditar.Text = deuda.NombreDeudor;
                txtEmailEditar.Text = deuda.EmailDeudor;
                txtDescripcionEditar.Text = deuda.Descripcion;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirEditar",
                    "new bootstrap.Modal(document.getElementById('modalEditarDeuda')).show();", true);
            }
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                int idDeuda = int.Parse(hfIdDeudaEditar.Value);
                Usuario usuario = (Usuario)Session["usuario"];
                DeudaNegocio negocio = new DeudaNegocio();
                Deuda deuda = negocio.ObtenerPorId(idDeuda);

                string emailAnterior = deuda.EmailDeudor;
                deuda.NombreDeudor = txtNombreEditar.Text.Trim();
                deuda.EmailDeudor = txtEmailEditar.Text.Trim();
                deuda.Descripcion = txtDescripcionEditar.Text.Trim();

                negocio.ModificarDeuda(deuda);

                if (deuda.EmailDeudor != emailAnterior)
                {
                    string rutaPlantillas = Server.MapPath("~/Template");
                    var reemplazos = new Dictionary<string, string>()
                      {
                          { "NOMBRE_DEUDOR", deuda.NombreDeudor },
                          { "NOMBRE_USUARIO", usuario.Nombre },
                          { "DESCRIPCION", deuda.Descripcion },
                          { "MONTO_TOTAL", deuda.MontoTotal.ToString("N2") },
                          { "CUOTAS", deuda.Cuotas.ToString() },
                          { "MONTO_CUOTA", (deuda.MontoTotal / deuda.Cuotas).Value.ToString("N2") },
                          { "FECHA", deuda.FechaInicio.ToString("dd/MM/yyyy") }
                      };
                    EmailService servicio = new EmailService();
                    servicio.armarCorreo(deuda.EmailDeudor, "Aviso de adquisición de deuda", reemplazos,
TipoCorreo.RegistroDeudaDeudor, rutaPlantillas);
                    servicio.enviarCorreo();
                }

                CargarDeudas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon:'success',title:'¡Éxito!',text:'Deuda modificada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon:'error',title:'Error',text:'{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                Deuda nueva = new Deuda();
                nueva.Usuario = usuario;
                nueva.NombreDeudor = txtNombre.Text.Trim();
                nueva.EmailDeudor = txtEmail.Text.Trim();
                nueva.Descripcion = txtDescripcion.Text.Trim();
                nueva.MontoTotal = ParseDecimal(txtMonto.Text);
                nueva.Cuotas = int.Parse(txtCuotas.Text);
                nueva.FechaInicio = DateTime.Parse(txtFecha.Text);
                nueva.Estado = EstadoDeuda.Pendiente;

                DeudaNegocio negocio = new DeudaNegocio();
                negocio.AgregarDeuda(nueva);
                GenerarCuotas(nueva);

                string rutaPlantillas = Server.MapPath("~/Template");
                var reemplazosDeudor = new Dictionary<string, string>()
                  {
                      { "NOMBRE_DEUDOR", nueva.NombreDeudor },
                      { "NOMBRE_USUARIO", usuario.Nombre },
                      { "DESCRIPCION", nueva.Descripcion },
                      { "MONTO_TOTAL", nueva.MontoTotal.ToString("N2") },
                      { "CUOTAS", nueva.Cuotas.ToString() },
                      { "MONTO_CUOTA", (nueva.MontoTotal / nueva.Cuotas).Value.ToString("N2") },
                      { "FECHA", nueva.FechaInicio.ToString("dd/MM/yyyy") }
                  };
                EmailService servicioDeudor = new EmailService();
                servicioDeudor.armarCorreo(nueva.EmailDeudor, "Aviso de adquisición de deuda", reemplazosDeudor,
TipoCorreo.RegistroDeudaDeudor, rutaPlantillas);
                servicioDeudor.enviarCorreo();

                var reemplazosUsuario = new Dictionary<string, string>()
                  {
                      { "NOMBRE_DEUDOR", nueva.NombreDeudor },
                      { "NOMBRE_USUARIO", usuario.Nombre },
                      { "DESCRIPCION", nueva.Descripcion },
                      { "MONTO_TOTAL", nueva.MontoTotal.ToString("N2") },
                      { "CUOTAS", nueva.Cuotas.ToString() },
                      { "MONTO_CUOTA", (nueva.MontoTotal / nueva.Cuotas).Value.ToString("N2") },
                      { "FECHA", nueva.FechaInicio.ToString("dd/MM/yyyy") }
                  };
                EmailService servicioUsuario = new EmailService();
                servicioUsuario.armarCorreo(usuario.Email, "Registro de deuda realizado correctamente",
reemplazosUsuario, TipoCorreo.RegistroDeudaAcreedor, rutaPlantillas);
                servicioUsuario.enviarCorreo();

                txtNombre.Text = txtEmail.Text = txtDescripcion.Text = txtMonto.Text = txtCuotas.Text = txtFecha.Text
= "";

                CargarDeudas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon:'success',title:'¡Éxito!',text:'Deuda agregada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon:'error',title:'Error',text:'{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        // ── Helpers ──────────────────────────────────────────────

        protected string GetIniciales(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "?";
            string[] p = nombre.Trim().Split(' ');
            return (p.Length >= 2 ? p[0][0].ToString() + p[1][0].ToString() : nombre.Substring(0, Math.Min(2,
nombre.Length))).ToUpper();
        }

        private static readonly string[] _avatarColors = {
              "rgba(13,110,253,0.12);color:#0057cd",
              "rgba(111,66,193,0.12);color:#6f42c1",
              "rgba(253,126,20,0.12);color:#fd7e14",
              "rgba(32,201,151,0.12);color:#13795b",
              "rgba(220,53,69,0.12);color:#b02a37"
          };

        protected string GetAvatarStyle(string nombre)
        {
            int idx = string.IsNullOrEmpty(nombre) ? 0 : Math.Abs(nombre.GetHashCode()) % _avatarColors.Length;
            return $"background:{_avatarColors[idx]};";
        }

        protected string GetBadgeEstado(int estado)
        {
            switch (estado)
            {
                case 0: return "<span class='badge-estado ms-2'style='background:rgba(25,135,84,0.12);color:#198754;'>Cobrado</span>";
                case 1: return "<span class='badge-estado ms-2'style='background:rgba(255,193,7,0.15);color:#856404;'>Pendiente</span>";
                default: return "";
            }
        }

        protected string FormatMonto(decimal monto, int estado)
        {
            return estado == 0 ? "$" + monto.ToString("N0") : "- $" + monto.ToString("N0");
        }

        protected string GetCuotasTexto(object cuotas)
        {
            if (cuotas == null || cuotas == DBNull.Value) return "Pago único";
            int c = Convert.ToInt32(cuotas);
            return c <= 1 ? "Pago único" : c + " cuotas";
        }

        protected string GetColorBarra(int estado)
        {
            return estado == 0 ? "bg-success" : "bg-warning";
        }

        protected string GetAnchoBarra(decimal montoTotal, decimal montoPendiente)
        {
            if (montoTotal <= 0) return "width:0%";
            int pct = (int)Math.Min(((montoTotal - montoPendiente) / montoTotal) * 100, 100);
            return $"width:{pct}%";
        }

        protected string GetProgresoTexto(decimal montoTotal, decimal montoPendiente)
        {
            if (montoPendiente <= 0) return "Cobrado en su totalidad";
            if (montoPendiente >= montoTotal) return "Sin cuotas cobradas aún";
            decimal cobrado = montoTotal - montoPendiente;
            return "$" + cobrado.ToString("N0") + " cobrados de $" + montoTotal.ToString("N0");
        }

        private void GenerarCuotas(Deuda deuda)
        {
            CuotaDeudaNegocio cuotaNegocio = new CuotaDeudaNegocio();
            for (int i = 1; i <= deuda.Cuotas.Value; i++)
            {
                CuotaDeuda cuota = new CuotaDeuda();
                cuota.Deuda = deuda;
                cuota.NumeroCuota = i;
                cuota.Monto = deuda.MontoTotal / deuda.Cuotas.Value;
                cuota.FechaVencimiento = deuda.FechaInicio.AddMonths(i);
                cuota.FechaPago = null;
                cuota.Estado = EstadoCuota.Pendiente;
                cuotaNegocio.AgregarCuota(cuota);
            }
        }

        private decimal ParseDecimal(string texto)
        {
            return decimal.Parse(texto.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
        }

    }
}