using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using System.Diagnostics.Eventing.Reader;

namespace TPFinalIntegrador
{
    public partial class Inicio : System.Web.UI.Page
    {
        public int MesSeleccionado
        {
            get
            {
                if (ViewState["MesSeleccionado"] != null)
                    return (int)ViewState["MesSeleccionado"];
                else
                    return DateTime.Now.Month;
            }
            set
            {
                ViewState["MesSeleccionado"] = value;
            }
        }

        public int AnioSeleccionado
        {
            get
            {
                if (ViewState["AnioSeleccionado"] != null)
                    return (int)ViewState["AnioSeleccionado"];
                else
                    return DateTime.Now.Year;
            }
            set
            {
                ViewState["AnioSeleccionado"] = value;
            }
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

                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    Hogar hogarSeleccionado = (Hogar)Session["HogarSeleccionado"];
                    lblBienvenidaHogar.Text = "Bienvenido a " + hogarSeleccionado.Nombre;
                    pnlInicioHogar.Visible = true;
                    pnlInicioPersonal.Visible = false;
                }
                else
                {
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];
                    lblBienvenidaPersonal.Text = "Bienvenido/a, " + usuarioLogueado.Nombre;
                    pnlInicioPersonal.Visible = true;
                    pnlInicioHogar.Visible = false;
                }
                CargarCategoriasIngreso();
                CargarResumenIngresos();
                CargarResumenGastos();
                CargarCategoriasGasto();
                CargarMediosPago();
                CargarMovimientosDelMes();
                CargarSaldoMes();
                CargarResumenPresupuesto();
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
                CategoriaNegocio negocio = new CategoriaNegocio();

                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    int idHogar = (int)Session["IdHogarActual"];
                    ddlCategoriaIngreso.DataSource = negocio.ListarPorHogarYTipo(idHogar, TipoCategoria.Ingreso);
                }
                else
                {
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];
                    ddlCategoriaIngreso.DataSource = negocio.ListarPorUsuarioYTipo(usuarioLogueado.IdUsuario, TipoCategoria.Ingreso);
                }

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

                /*--------------ENVIO DE MAIL----------------------*/
                string rutaPlantillas = Server.MapPath("~/Template");

                var reemplazos = new Dictionary<string, string>()
                {
                    { "NOMBRE_USUARIO", usuarioLogueado.Nombre },
                    { "DESCRIPCION", ingreso.Descripcion},
                    { "MONTO", ingreso.Monto.ToString("N2") },
                    { "FECHA", ingreso.Fecha.ToString("dd/MM/yyyy")}
                };

                EmailService servicio = new EmailService();

                servicio.armarCorreo(
                    usuarioLogueado.Email,
                    "Nuevo registro de ingreso",
                    reemplazos,
                    TipoCorreo.RegistroIngreso,
                    rutaPlantillas
                );

                servicio.enviarCorreo();
                /*---------------------------------------------------------------*/

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
                IngresoNegocio negocio = new IngresoNegocio();
                decimal total = 0;
                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    int idHogar = (int)Session["idHogarActual"];
                    total = negocio.TotalIngresosMesActualHogar(idHogar);
                }
                else
                {
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];
                    total = negocio.TotalIngresosMesActual(usuarioLogueado.IdUsuario);
                }

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


        protected void btnMesAnterior_Click(object sender, EventArgs e)
        {
            MesSeleccionado--;

            if (MesSeleccionado < 1)
            {
                MesSeleccionado = 12;
                AnioSeleccionado--;
            }

            CargarMovimientosDelMes();
        }

        protected void btnMesSiguiente_Click(object sender, EventArgs e)
        {
            MesSeleccionado++;

            if (MesSeleccionado > 12)
            {
                MesSeleccionado = 1;
                AnioSeleccionado++;
            }

            CargarMovimientosDelMes();
        }

        protected void CargarCategoriasGasto()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();

                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    int idHogar = (int)Session["IdHogarActual"];
                    ddlCategoriaGasto.DataSource = negocio.ListarPorHogarYTipo(idHogar, TipoCategoria.Gasto);
                }
                else
                {
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];
                    ddlCategoriaGasto.DataSource = negocio.ListarPorUsuarioYTipo(usuarioLogueado.IdUsuario, TipoCategoria.Gasto);
                }
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
                MedioPagoNegocio negocio = new MedioPagoNegocio();
                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    int idHogar = (int)Session["IdHogarActual"];
                    ddlMedioPagoGasto.DataSource = negocio.ListarPorHogar(idHogar);
                }
                else
                {
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];
                    ddlMedioPagoGasto.DataSource = negocio.ListarPorUsuario(usuarioLogueado.IdUsuario);
                }
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
                bool esCuotas = rbCuotas.Checked;

                if (string.IsNullOrWhiteSpace(txtDescripcionGasto.Text) ||
                    string.IsNullOrWhiteSpace(txtFechaGasto.Text))
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "errorGastoCampos",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Completá los campos obligatorios.'});",
                        true);
                    return;
                }

                if (!esCuotas && string.IsNullOrWhiteSpace(txtMontoPesosGasto.Text))
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "errorMonto",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Ingresá el monto del gasto.'});",
                        true);
                    return;
                }

                if (esCuotas)
                {
                    if (string.IsNullOrWhiteSpace(txtMontoCuotaGasto.Text) ||
                        string.IsNullOrWhiteSpace(txtCantidadCuotasGasto.Text))
                    {
                        ScriptManager.RegisterStartupScript(
                            this, this.GetType(),
                            "errorCuotas",
                            "Swal.fire({icon: 'error', title: 'Error', text: 'Completá monto de cuota y cantidad de cuotas.'});",
                            true);
                        return;
                    }
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
                gasto.Moneda = (Moneda)int.Parse(ddlMonedaGasto.SelectedValue);

                gasto.Categoria = new Categoria();
                gasto.Categoria.IdCategoria = int.Parse(ddlCategoriaGasto.SelectedValue);

                gasto.MedioDePago = new MedioPago();
                gasto.MedioDePago.IdMedioPago = int.Parse(ddlMedioPagoGasto.SelectedValue);

                gasto.Usuario = usuarioLogueado;
                gasto.Estado = true;

                // PAGO EN CUOTAS O UN PAGO 
                gasto.EsEnCuotas = esCuotas;

                if (esCuotas)
                {
                    gasto.MontoCuota = decimal.Parse(txtMontoCuotaGasto.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    gasto.CantidadCuotas = int.Parse(txtCantidadCuotasGasto.Text.Trim());

                    // Calcular total cuota
                    gasto.MontoPesos = gasto.MontoCuota * gasto.CantidadCuotas;
                }
                else
                {
                    gasto.MontoPesos = decimal.Parse(txtMontoPesosGasto.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                }

                // MONEDA EXTRANJERA
                if (gasto.Moneda != Moneda.ARS)
                {
                    gasto.MontoUSD = decimal.Parse(txtMontoUSDGasto.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    gasto.Cotizacion = decimal.Parse(txtCotizacionGasto.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                }

                GastoNegocio negocio = new GastoNegocio();

                // Obtener el ID
                int idGasto = negocio.AgregarGasto(gasto);
                               
                // GENERAR CUOTAS
                if (gasto.EsEnCuotas)
                {
                    CuotaNegocio cuotaNegocio = new CuotaNegocio();

                    for (int i = 1; i <= gasto.CantidadCuotas; i++)
                    {
                        Cuota cuota = new Cuota();
                        cuota.Gasto = new Gasto { IdGasto = idGasto };
                        cuota.NumeroCuota = i;
                        cuota.TotalCuotas = gasto.CantidadCuotas;
                        cuota.Monto = gasto.MontoCuota;
                        cuota.Vencimiento = gasto.Fecha.AddMonths(i - 1);
                        cuota.Estado = EstadoCuota.Pendiente;

                        cuotaNegocio.AgregarCuota(cuota);

                    }
                    /*--------------ENVIO DE MAIL----------------------*/
                    string rutaPlantillas = Server.MapPath("~/Template");

                    var reemplazos = new Dictionary<string, string>()
                    {
                        { "NOMBRE_USUARIO", usuarioLogueado.Nombre },
                        { "DESCRIPCION", gasto.Descripcion },
                        { "MONTO_CUOTA", gasto.MontoCuota.ToString("N2") },
                        { "CANTIDAD_CUOTAS", gasto.CantidadCuotas.ToString() },
                        { "MONTO_TOTAL", gasto.MontoPesos.ToString("N2") },
                        { "FECHA", gasto.Fecha.ToString("dd/MM/yyyy") }
                    };

                    EmailService servicio = new EmailService();

                    servicio.armarCorreo(
                        usuarioLogueado.Email,
                        "Nuevo gasto en cuotas registrado",
                        reemplazos,
                        TipoCorreo.GastoEnCuotas,
                        rutaPlantillas
                    );

                    servicio.enviarCorreo();
                    /*---------------------------------------------------------------*/
                }
                else
                {
                    /*--------------ENVIO DE MAIL----------------------*/
                    string rutaPlantillas = Server.MapPath("~/Template");

                    var reemplazos = new Dictionary<string, string>()
                    {
                        { "NOMBRE_USUARIO", usuarioLogueado.Nombre },
                        { "DESCRIPCION", gasto.Descripcion },
                        { "MONTO", gasto.MontoPesos.ToString("N2") },
                        { "FECHA", gasto.Fecha.ToString("dd/MM/yyyy") }
                    };

                    EmailService servicio = new EmailService();

                    servicio.armarCorreo(
                        usuarioLogueado.Email,
                        "Nuevo gasto registrado",
                        reemplazos,
                        TipoCorreo.RegistroGasto,
                        rutaPlantillas
                    );

                    servicio.enviarCorreo();
                    /*---------------------------------------------------------------*/
                }

                // LIMPIAR CAMPOS
                txtDescripcionGasto.Text = "";
                txtFechaGasto.Text = "";
                txtMontoPesosGasto.Text = "";
                txtMontoUSDGasto.Text = "";
                txtCotizacionGasto.Text = "";
                txtMontoCuotaGasto.Text = "";
                txtCantidadCuotasGasto.Text = "";

                ddlCategoriaGasto.SelectedIndex = 0;
                ddlMedioPagoGasto.SelectedIndex = 0;
                ddlMonedaGasto.SelectedIndex = 0;

                rbUnPago.Checked = true;

                //REFRESCAR
                CargarResumenGastos();
                CargarSaldoMes();
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
                GastoNegocio negocio = new GastoNegocio();
                decimal total = 0;
                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    int idHogar = (int)Session["IdHogarActual"];
                    total = negocio.TotalGastosMesActualHogar(idHogar);
                    lblGastosMesHogar.Text = "$ " + total.ToString("N2");
                }
                else
                {
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];
                    total = negocio.TotalGastosMesActual(usuarioLogueado.IdUsuario);
                    lblGastosMes.Text = "$ " + total.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<Movimiento> ObtenerMovimientosDelMes()
        {
            List<Movimiento> movimientos = new List<Movimiento>();
            IngresoNegocio ingresoNegocio = new IngresoNegocio();
            GastoNegocio gastoNegocio = new GastoNegocio();
            List<Ingreso> ingresos = null;
            List<Gasto> gastos = null;

            if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
            {
                int idHogar = (int)Session["IdHogarActual"];
                ingresos = ingresoNegocio.ListarPorHogarMesActual(idHogar);
                gastos = gastoNegocio.ListarPorHogarMesActual(idHogar);
            }
            else
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                ingresos = ingresoNegocio.ListarPorUsuarioMesActual(usuarioLogueado.IdUsuario);
                gastos = gastoNegocio.ListarPorUsuarioMesActual(usuarioLogueado.IdUsuario);
            }

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

        protected void CargarMovimientosDelMes()
        {
            List<Movimiento> lista = ObtenerMovimientosDelMes();

            rptMovimientos.DataSource = lista;
            rptMovimientos.DataBind();

            lblMesActual.Text = new DateTime(AnioSeleccionado, MesSeleccionado, 1)
                .ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));
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

        protected void btnGuardarIntegrante_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMailIntegrante.Text))
                {
                    ScriptManager.RegisterStartupScript(
                   this, this.GetType(),
                   "ingresaNombre",
                   "Swal.fire({icon: 'error', title: 'Error', text: 'Ingresá el mail del integrante.'});",
                   true);

                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                HogarNegocio negocioHogar = new HogarNegocio();
                Usuario user = new Usuario();

                user = negocio.buscarMail(txtMailIntegrante.Text);

                if(user != null)
                {
                    if(negocioHogar.AgregarIntegrante((int)Session["IdHogarActual"], user.IdUsuario, "Miembro"))
                    {
                        ScriptManager.RegisterStartupScript(
                           this, this.GetType(),
                           "categoriaCreada",
                           "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Integrante agergado con exito!.'});",
                           true);
                    }
                    else
                    {
                            ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "errorAlert",
                        "Swal.fire({icon: 'error', title: 'El usuario ya forma parte del hogar!'});",
                        true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                   this, this.GetType(),
                   "errorAlert",
                   "Swal.fire({icon: 'error', title: 'No se encontro un usuario con ese Mail.'});",
                   true);
                }
                limpiarMailIntegrante();

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

        protected void limpiarMailIntegrante()
        {
            txtMailIntegrante.Text = "";
        }
        private void CargarResumenPresupuesto()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            PresupuestoCategoriaNegocio negocio = new PresupuestoCategoriaNegocio();

            var lista = negocio.ListarPorUsuarioYMes(usuario.IdUsuario, DateTime.Now.Month, DateTime.Now.Year)
                               .FindAll(p => p.MontoPresupuestado > 0);

            rptPresupuesto.DataSource = lista;
            rptPresupuesto.DataBind();
        }
        protected string ObtenerClaseBarra(object montoPresupuestado, object gastoReal)
        {
            decimal presupuesto = (decimal)montoPresupuestado;
            decimal gasto = (decimal)gastoReal;
            if (presupuesto <= 0) return "progress-bar bg-secondary";
            int porcentaje = (int)((gasto / presupuesto) * 100);
            if (porcentaje >= 100) return "progress-bar bg-danger";
            if (porcentaje >= 80) return "progress-bar bg-warning";
            return "progress-bar bg-success";
        }

        protected string ObtenerAnchoBarra(object montoPresupuestado, object gastoReal)
        {
            decimal presupuesto = (decimal)montoPresupuestado;
            decimal gasto = (decimal)gastoReal;
            if (presupuesto <= 0) return "width: 0%";
            int porcentaje = (int)Math.Min((gasto / presupuesto) * 100, 100);
            return $"width: {porcentaje}%";
        }
    }

}