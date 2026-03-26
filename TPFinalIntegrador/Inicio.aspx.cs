using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPFinalIntegrador
{
    public partial class Inicio : System.Web.UI.Page
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
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                lblBienvenida.Text = "Bienvenido/a, " + usuarioLogueado.Nombre;
                CargarCategoriasIngreso(); //Carga desplegable 
                CargarResumenIngresos();
                CargarResumenGastos();
                CargarCategoriasGasto();
                CargarMediosPago();
                CargarMovimientosDelMes();
                CargarSaldoMes();
                pnlReportes.Visible = false;
            }
        }

        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                {
                    ScriptManager.RegisterStartupScript(
                   this, this.GetType(),
                   "ingresaNombre",
                   "Swal.fire({icon: 'error', title: 'Error', text: 'Ingresá un nombre para la categoría.'});",
                   true);

                    return;
                }

                // Obtener usuario logueado
                Usuario usuarioLogueado = (Usuario)Session["usuario"];

                Categoria categoria = new Categoria();
                categoria.Nombre = txtNombreCategoria.Text.Trim();
                categoria.Usuario = usuarioLogueado;
                categoria.Tipo = (TipoCategoria)int.Parse(ddlTipoCategoria.SelectedValue);
                categoria.Estado = true;

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.AgregarCategoria(categoria);

                ScriptManager.RegisterStartupScript(
                   this, this.GetType(),
                   "categoriaCreada",
                   "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Categoría creada correctamente.'});",
                   true);

                LimpiarModalCategoria();
                CargarCategoriasIngreso();
                CargarCategoriasGasto();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "errorAlert",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});",
                    true);
            }
        }

        protected void LimpiarModalCategoria()
        {
            txtNombreCategoria.Text = "";
            ddlTipoCategoria.SelectedIndex = 0;
            lblMensajeCategoria.Text = "";
            lblMensajeCategoria.CssClass = "";
        }

        protected void LimpiarModalIngreso()
        {
            txtDescripcionIngreso.Text = "";
            txtFechaIngreso.Text = "";
            txtMontoIngreso.Text = "";
            ddlCategoriaIngreso.SelectedIndex = 0;
            lblMensajeIngreso.Text = "";
            lblMensajeIngreso.CssClass = "";
        }

        protected void LimpiarModalMedioPago()
        {
            ddlTipoMedioPago.SelectedIndex = 0;
            txtDescripcionMedioPago.Text = "";
            txtDiaCierre.Text = "";
            txtDiaVencimiento.Text = "";
            lblMensajeMedioPago.Text = "";
            lblMensajeMedioPago.CssClass = "";
        }

        protected void CargarCategoriasIngreso()
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                CategoriaNegocio negocio = new CategoriaNegocio();

                ddlCategoriaIngreso.DataSource = negocio.ListarPorUsuarioYTipo(usuarioLogueado.IdUsuario, TipoCategoria.Ingreso);
                ddlCategoriaIngreso.DataTextField = "Nombre";
                ddlCategoriaIngreso.DataValueField = "IdCategoria";
                ddlCategoriaIngreso.DataBind();

                ddlCategoriaIngreso.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcionIngreso.Text) ||
                    string.IsNullOrWhiteSpace(txtFechaIngreso.Text) ||
                    string.IsNullOrWhiteSpace(txtMontoIngreso.Text))
                {
                    ScriptManager.RegisterStartupScript(
                       this, this.GetType(),
                       "completaCampos",
                       "Swal.fire({icon: 'error', title: 'Error', text: 'Completá todos los campos obligatorios.'});",
                       true);
                    return;
                }

                if (ddlCategoriaIngreso.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "mostrarModalIngreso",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Debés seleccionar una categoría.'});",
                        true);

                    return;
                }

                Usuario usuarioLogueado = (Usuario)Session["usuario"];

                Ingreso ingreso = new Ingreso();
                ingreso.Descripcion = txtDescripcionIngreso.Text.Trim();
                ingreso.Fecha = DateTime.Parse(txtFechaIngreso.Text);
                ingreso.Monto = decimal.Parse(txtMontoIngreso.Text);

                ingreso.Categoria = new Categoria();
                ingreso.Categoria.IdCategoria = int.Parse(ddlCategoriaIngreso.SelectedValue);

                ingreso.Usuario = usuarioLogueado;
                ingreso.Estado = true;

                IngresoNegocio negocio = new IngresoNegocio();
                negocio.AgregarIngreso(ingreso);

                txtDescripcionIngreso.Text = "";
                txtFechaIngreso.Text = "";
                txtMontoIngreso.Text = "";
                ddlCategoriaIngreso.SelectedIndex = 0;

                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "ingresoCreado",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Ingreso guardado correctamente.'});",
                    true);

                CargarResumenIngresos(); //refresca los ingresos luego de agregar uno nuevo
                CargarSaldoMes(); //refresca el saldo al agregar un ingreso nuevo


                CargarMovimientosDelMes();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});",
                    true);
            }
        }

        protected void CargarResumenIngresos()
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                IngresoNegocio negocio = new IngresoNegocio();

                decimal total = negocio.TotalIngresosMesActual(usuarioLogueado.IdUsuario);
                lblIngresosMes.Text = "$ " + total.ToString("N2");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnGuardarMedioPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoMedioPago.SelectedValue == "0" ||
                    string.IsNullOrWhiteSpace(txtDescripcionMedioPago.Text))
                {
                    ScriptManager.RegisterStartupScript(
                       this, this.GetType(),
                       "completarCampos",
                       "Swal.fire({icon: 'error', title: 'Error', text: 'Completá los campos obligatorios.'});",
                       true);

                    return;
                }

                TipoPago tipoSeleccionado = (TipoPago)int.Parse(ddlTipoMedioPago.SelectedValue);

                if (tipoSeleccionado == TipoPago.Credito)
                {
                    if (string.IsNullOrWhiteSpace(txtDiaCierre.Text) ||
                        string.IsNullOrWhiteSpace(txtDiaVencimiento.Text))
                    {
                        ScriptManager.RegisterStartupScript(
                           this, this.GetType(),
                           "tarjetaError",
                           "Swal.fire({icon: 'error', title: 'Error', text: 'Para tarjeta de crédito debés completar día de cierre y vencimiento.'});",
                           true);

                        return;
                    }
                }

                Usuario usuarioLogueado = (Usuario)Session["usuario"];

                MedioPago medioPago = new MedioPago();
                medioPago.Tipo = tipoSeleccionado;
                medioPago.Descripcion = txtDescripcionMedioPago.Text.Trim();
                medioPago.Usuario = usuarioLogueado;
                medioPago.Estado = true;

                if (tipoSeleccionado == TipoPago.Credito)
                {
                    medioPago.DiaCierre = int.Parse(txtDiaCierre.Text);
                    medioPago.DiaVencimiento = int.Parse(txtDiaVencimiento.Text);
                }

                MedioPagoNegocio negocio = new MedioPagoNegocio();
                negocio.AgregarMedioPago(medioPago);

                txtDescripcionMedioPago.Text = "";
                txtDiaCierre.Text = "";
                txtDiaVencimiento.Text = "";
                ddlTipoMedioPago.SelectedIndex = 0;

                CargarMediosPago();

                ScriptManager.RegisterStartupScript(
                      this, this.GetType(),
                      "medioDePagoIngresado",
                      "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Medio de pago guardado correctamente.'});",
                      true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});",
                    true);
            }
        }

        protected void CargarCategoriasGasto()
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                CategoriaNegocio negocio = new CategoriaNegocio();

                ddlCategoriaGasto.DataSource = negocio.ListarPorUsuarioYTipo(usuarioLogueado.IdUsuario, TipoCategoria.Gasto);
                ddlCategoriaGasto.DataTextField = "Nombre";
                ddlCategoriaGasto.DataValueField = "IdCategoria";
                ddlCategoriaGasto.DataBind();

                ddlCategoriaGasto.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CargarMediosPago()
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];

                MedioPagoNegocio negocio = new MedioPagoNegocio();

                ddlMedioPagoGasto.DataSource = negocio.ListarPorUsuario(usuarioLogueado.IdUsuario);
                ddlMedioPagoGasto.DataTextField = "Descripcion";
                ddlMedioPagoGasto.DataValueField = "IdMedioPago";
                ddlMedioPagoGasto.DataBind();

                ddlMedioPagoGasto.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarGasto_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcionGasto.Text) ||
                    string.IsNullOrWhiteSpace(txtFechaGasto.Text) ||
                    string.IsNullOrWhiteSpace(txtMontoPesosGasto.Text))
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "errorGastoCampos",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Completá los campos obligatorios.'});",
                        true);
                    return;
                }

                if (ddlCategoriaGasto.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "errorGastoCategoria",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Seleccioná una categoría.'});",
                        true);
                    return;
                }

                if (ddlMedioPagoGasto.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "errorGastoMedioPago",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Seleccioná un medio de pago.'});",
                        true);
                    return;
                }

                Usuario usuarioLogueado = (Usuario)Session["usuario"];

                Gasto gasto = new Gasto();

                gasto.Descripcion = txtDescripcionGasto.Text.Trim();
                gasto.Fecha = DateTime.Parse(txtFechaGasto.Text);
                gasto.MontoPesos = decimal.Parse(txtMontoPesosGasto.Text);

                gasto.Moneda = (Moneda)int.Parse(ddlMonedaGasto.SelectedValue);

                gasto.Categoria = new Categoria();
                gasto.Categoria.IdCategoria = int.Parse(ddlCategoriaGasto.SelectedValue);

                gasto.MedioDePago = new MedioPago();
                gasto.MedioDePago.IdMedioPago = int.Parse(ddlMedioPagoGasto.SelectedValue);

                gasto.Usuario = usuarioLogueado;

                gasto.Estado = true;

                if (gasto.Moneda != Moneda.ARS)
                {
                    gasto.MontoUSD = decimal.Parse(txtMontoUSDGasto.Text);
                    gasto.Cotizacion = decimal.Parse(txtCotizacionGasto.Text);
                }

                GastoNegocio negocio = new GastoNegocio();
                negocio.AgregarGasto(gasto);

                // Limpiar campos
                txtDescripcionGasto.Text = "";
                txtFechaGasto.Text = "";
                txtMontoPesosGasto.Text = "";
                txtMontoUSDGasto.Text = "";
                txtCotizacionGasto.Text = "";
                ddlCategoriaGasto.SelectedIndex = 0;
                ddlMedioPagoGasto.SelectedIndex = 0;
                ddlMonedaGasto.SelectedIndex = 0;

                CargarResumenGastos(); //Refresca los gastos al agregar uno nuevo
                CargarSaldoMes(); //Refresca el saldo al agregar un gasto
                CargarMovimientosDelMes();

                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "okGasto",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Gasto guardado correctamente.'});",
                    true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});",
                    true);
            }
        }

        protected void CargarResumenGastos()
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                GastoNegocio negocio = new GastoNegocio();

                decimal total = negocio.TotalGastosMesActual(usuarioLogueado.IdUsuario);
                lblGastosMes.Text = "$ " + total.ToString("N2");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Movimiento> ObtenerMovimientosDelMes()
        {
            List<Movimiento> movimientos = new List<Movimiento>();

            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            IngresoNegocio ingresoNegocio = new IngresoNegocio();
            GastoNegocio gastoNegocio = new GastoNegocio();

            List<Ingreso> ingresos = ingresoNegocio.ListarPorUsuarioMesActual(usuarioLogueado.IdUsuario);
            List<Gasto> gastos = gastoNegocio.ListarPorUsuarioMesActual(usuarioLogueado.IdUsuario);

            foreach (Ingreso ingreso in ingresos)
            {
                Movimiento mov = new Movimiento();
                mov.Fecha = ingreso.Fecha;
                mov.Descripcion = ingreso.Descripcion;
                mov.Categoria = ingreso.Categoria.Nombre;
                mov.Tipo = "Ingreso";
                mov.Monto = ingreso.Monto;
                mov.Estado = ingreso.Estado ? "Activo" : "Eliminado";

                movimientos.Add(mov);
            }

            foreach (Gasto gasto in gastos)
            {
                Movimiento mov = new Movimiento();
                mov.Fecha = gasto.Fecha;
                mov.Descripcion = gasto.Descripcion;
                mov.Categoria = gasto.Categoria.Nombre;
                mov.Tipo = "Gasto";
                mov.Monto = gasto.MontoPesos;
                mov.Estado = gasto.Estado ? "Activo" : "Eliminado";

                movimientos.Add(mov);
            }

            return movimientos.OrderByDescending(x => x.Fecha).ToList();
        }

        private void CargarMovimientosDelMes()
        {
            List<Movimiento> lista = ObtenerMovimientosDelMes();

            rptMovimientos.DataSource = lista;
            rptMovimientos.DataBind();
        }

        private void CargarSaldoMes()
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                GastoNegocio negocioGasto = new GastoNegocio();
                IngresoNegocio negocioIngreso = new IngresoNegocio();
                decimal ingresos = negocioIngreso.TotalIngresosMesActual(usuarioLogueado.IdUsuario);
                decimal gastos = negocioGasto.TotalGastosMesActual(usuarioLogueado.IdUsuario);
                //decimal ingresoTotal = 0;
                //decimal gastoTotal = 0;

                //foreach (decimal numero in listaIngresos)
                //{
                //    ingresoTotal += numero;
                //}



                decimal total = ingresos - gastos;
                lblsaldoMes.Text = "$ " + total.ToString("N2");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarMesesAnios()
        {
            // Meses
            ddlMesIngresos.Items.Clear();
            ddlMesGastos.Items.Clear();
            var meses = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < 12; i++)
            {
                string nombre = meses[i];
                ddlMesIngresos.Items.Add(new ListItem(nombre, (i + 1).ToString()));
                ddlMesGastos.Items.Add(new ListItem(nombre, (i + 1).ToString()));
            }

            // Años: últimos 5 años hasta el año actual
            ddlAnioIngresos.Items.Clear();
            ddlAnioGastos.Items.Clear();
            int anioActual = DateTime.Now.Year;
            for (int y = anioActual; y >= anioActual - 5; y--)
            {
                ddlAnioIngresos.Items.Add(new ListItem(y.ToString(), y.ToString()));
                ddlAnioGastos.Items.Add(new ListItem(y.ToString(), y.ToString()));
            }
        }

        private void CargarIngresosPorMes(int mes, int anio)
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                IngresoNegocio ingresoNegocio = new IngresoNegocio();
                var lista = ingresoNegocio.ListarPorUsuarioPorMes(usuarioLogueado.IdUsuario, mes, anio);

                gvIngresosMes.DataSource = lista;
                gvIngresosMes.DataBind();
            }
            catch (Exception ex)
            {
                // Registrar alerta en caso de error
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorIngresos",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void btnMostrarIngresos_Click(object sender, EventArgs e)
        {
            int mes = 0;
            int anio = 0;

            if (!int.TryParse(ddlMesIngresos.SelectedValue, out mes))
                mes = DateTime.Now.Month;

            if (!int.TryParse(ddlAnioIngresos.SelectedValue, out anio))
                anio = DateTime.Now.Year;

            CargarIngresosPorMes(mes, anio);
        }

        private void CargarGastosPorMes(int mes, int anio)
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                negocio.GastoNegocio gastoNegocio = new negocio.GastoNegocio();
                var lista = gastoNegocio.ListarPorUsuarioPorMes(usuarioLogueado.IdUsuario, mes, anio);

                // Proyectar a objeto plano para evitar problemas de binding con propiedades anidadas
                var datos = lista.Select(g => new
                {
                    Fecha = g.Fecha,
                    Descripcion = g.Descripcion,
                    Categoria = g.Categoria != null ? g.Categoria.Nombre : "",
                    MedioPago = g.MedioDePago != null ? g.MedioDePago.Descripcion : "",
                    Monto = g.MontoPesos
                }).ToList();

                gvGastosMes.DataSource = datos;
                gvGastosMes.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorGastos",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void btnMostrarGastos_Click(object sender, EventArgs e)
        {
            int mes = 0;
            int anio = 0;

            if (!int.TryParse(ddlMesGastos.SelectedValue, out mes))
                mes = DateTime.Now.Month;

            if (!int.TryParse(ddlAnioGastos.SelectedValue, out anio))
                anio = DateTime.Now.Year;

            CargarGastosPorMes(mes, anio);
        }

        protected void lnkVerMetricas_Click(object sender, EventArgs e)
        {
            // Mostrar el panel de reportes y cargar datos iniciales (mes corriente)
            pnlReportes.Visible = true;

            CargarMesesAnios();
            // Ingresos
            ddlMesIngresos.SelectedValue = DateTime.Now.Month.ToString();
            ddlAnioIngresos.SelectedValue = DateTime.Now.Year.ToString();
            CargarIngresosPorMes(DateTime.Now.Month, DateTime.Now.Year);

            // Gastos
            ddlMesGastos.SelectedValue = DateTime.Now.Month.ToString();
            ddlAnioGastos.SelectedValue = DateTime.Now.Year.ToString();
            CargarGastosPorMes(DateTime.Now.Month, DateTime.Now.Year);

            // Hacer scroll suave hacia el contenedor de reportes
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollReportes",
                $"document.getElementById('{pnlReportes.ClientID}').scrollIntoView({{behavior:'smooth'}});", true);
        }

    }
}
    
