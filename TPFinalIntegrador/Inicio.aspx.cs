using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
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

                // ¿Estamos en la vista de Hogar?
                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    Hogar hogarSeleccionado = (Hogar)Session["HogarSeleccionado"];
                    // Lógica de Hogar...
                    pnlLinkGastosIntegrante.Visible = true;
                }
                else
                {
                    if (Session["usuario"] != null)
                    {
                        Usuario usuarioLogueado = (Usuario)Session["usuario"];

                        GastoResumenNegocio negocio = new GastoResumenNegocio();
                        List<GastoResumen> gastos = negocio.ObtenerGastosDelMes(usuarioLogueado.IdUsuario);
                        CargarGraficoDeTorta(gastos);
                    }
                    else
                    {
                        Response.Redirect("Login.aspx", false);
                        return;
                    }
                }

                // Carga de combos y tablas
                CargarCategoriasIngreso();               
                CargarCategoriasGasto();
                CargarMediosPago();            
                CargarMovimientosDelMesRecientes();
                cargarMetasAhorro();
                CargarResumenPresupuesto();
                cargarDashboardInfo();
                //pnlReportes.Visible = false;
            }
        }

        protected void cargarDashboardInfo()
        {
            CargarSaldoMes();
            CargarResumenIngresos();
            CargarResumenGastos();
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

                // Parsear monto de ingreso usando cultura argentina (es-AR) por defecto
                var culturaARS = new CultureInfo("es-AR");
                decimal montoIngreso;
                if (!TryParseDecimalFlexible(txtMontoIngreso.Text.Trim(), culturaARS, out montoIngreso))
                {
                    ScriptManager.RegisterStartupScript(
                       this, this.GetType(),
                       "completaCampos",
                       "Swal.fire({icon: 'error', title: 'Error', text: 'Monto de ingreos inválido.'});",
                       true);
                    return;
                }

                Ingreso ingreso = new Ingreso();
                ingreso.Descripcion = txtDescripcionIngreso.Text.Trim();
                ingreso.Fecha = DateTime.Parse(txtFechaIngreso.Text);
                ingreso.Monto = montoIngreso;

                ingreso.Categoria = new Categoria();
                ingreso.Categoria.IdCategoria = int.Parse(ddlCategoriaIngreso.SelectedValue);

                ingreso.Usuario = usuarioLogueado;
                ingreso.Estado = true;

                IngresoNegocio negocio = new IngresoNegocio();

                if (!string.IsNullOrEmpty(hfIdIngresoEdicion.Value))
                {
                    ingreso.IdIngreso = int.Parse(hfIdIngresoEdicion.Value);
                    negocio.ModificarIngreso(ingreso);
                }
                else
                {
                    negocio.AgregarIngreso(ingreso);
                }

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

                cargarDashboardInfo();


                CargarMovimientosDelMesRecientes();
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

                h2Ingresos.InnerText = "$ " + total.ToString("N2");
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
            cargarDashboardInfo();
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
            cargarDashboardInfo();
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorGastoCampos",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Completá los campos obligatorios.'});", true);
                    return;
                }

                if (!esCuotas && string.IsNullOrWhiteSpace(txtMontoPesosGasto.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMonto",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Ingresá el monto del gasto.'});", true);
                    return;
                }

                if (esCuotas)
                {
                    if (string.IsNullOrWhiteSpace(txtMontoCuotaGasto.Text) ||
                        string.IsNullOrWhiteSpace(txtCantidadCuotasGasto.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCuotas",
                            "Swal.fire({icon: 'error', title: 'Error', text: 'Completá monto de cuota y cantidad de cuotas.'});", true);
                        return;
                    }
                }

                if (ddlCategoriaGasto.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorGastoCategoria",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Seleccioná una categoría.'});", true);
                    return;
                }

                if (ddlMedioPagoGasto.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorGastoMedioPago",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Seleccioná un medio de pago.'});", true);
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

                // Si la moneda es ARS usamos la cultura argentina preferente
                var culturaARS = new CultureInfo("es-AR");

                if (esCuotas)
                {
                    decimal montoCuota;
                    if (!TryParseDecimalFlexible(txtMontoCuotaGasto.Text.Trim(), culturaARS, out montoCuota))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorParseMontoCuota",
                            "Swal.fire({icon: 'error', title: 'Error', text: 'Monto de cuota inválido.'});", true);
                        return;
                    }

                    int cantidadCuotas;
                    if (!int.TryParse(txtCantidadCuotasGasto.Text.Trim(), out cantidadCuotas) || cantidadCuotas <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCantidadCuotas",
                            "Swal.fire({icon: 'error', title: 'Error', text: 'Cantidad de cuotas inválida.'});", true);
                        return;
                    }

                    gasto.MontoPesos = montoCuota;
                    gasto.CantidadCuotas = cantidadCuotas;
                    gasto.MontoCuota = gasto.MontoPesos / gasto.CantidadCuotas;

                    // Calcular total cuota
                    gasto.MontoPesos = gasto.MontoCuota * gasto.CantidadCuotas;
                }
                else
                {
                    decimal montoPesos;
                    // Para ARS: preferir cultura es-AR; para otras monedas aquí se guardará en MontoPesos si la moneda es ARS
                    if (gasto.Moneda == Moneda.ARS)
                    {
                        if (!TryParseDecimalFlexible(txtMontoPesosGasto.Text.Trim(), culturaARS, out montoPesos))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorParseMontoPesos",
                                "Swal.fire({icon: 'error', title: 'Error', text: 'Monto en pesos inválido.'});", true);
                            return;
                        }
                        gasto.MontoPesos = montoPesos;
                    }
                    else
                    {
                        // Si la moneda no es ARS, el campo txtMontoPesosGasto suele quedar vacío; manejamos más abajo MontoUSD/Cotizacion
                        // Si se completó el campo en pesos por alguna razón, intentamos parsear con cultura flexible
                        if (!string.IsNullOrWhiteSpace(txtMontoPesosGasto.Text) &&
                            TryParseDecimalFlexible(txtMontoPesosGasto.Text.Trim(), CultureInfo.InvariantCulture, out montoPesos))
                        {
                            gasto.MontoPesos = montoPesos;
                        }
                    }
                }

                // MONEDA EXTRANJERA: parseo MontoUSD y Cotizacion (aceptar '.' o ',' mediante flexible)
                if (gasto.Moneda != Moneda.ARS)
                {
                    decimal montoUsd;
                    decimal cotizacion;
                    if (!TryParseDecimalFlexible(txtMontoUSDGasto.Text.Trim(), CultureInfo.InvariantCulture, out montoUsd))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorParseUSD",
                            "Swal.fire({icon: 'error', title: 'Error', text: 'Monto en moneda original inválido.'});", true);
                        return;
                    }
                    if (!TryParseDecimalFlexible(txtCotizacionGasto.Text.Trim(), CultureInfo.InvariantCulture, out cotizacion))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorParseCotizacion",
                            "Swal.fire({icon: 'error', title: 'Error', text: 'Cotización inválida.'});", true);
                        return;
                    }

                    gasto.MontoUSD = montoUsd;
                    gasto.Cotizacion = cotizacion;

                    // Calcular MontoPesos si no calculado aún
                    gasto.MontoPesos = gasto.MontoUSD * gasto.Cotizacion;
                }

                //agregamos logica para guardar el id del hogar en el gasto
                if (Session["ModoVista"] != null && Session["ModoVista"].ToString() == "Hogar" && Session["IdHogarActual"] != null)
                {
                    gasto.Hogar = new Hogar();
                    gasto.Hogar.IdHogar = (int)Session["IdHogarActual"];
                }

                GastoNegocio negocio = new GastoNegocio();

                // Obtener el ID
                if (!string.IsNullOrEmpty(hfIdGastoEdicion.Value))

                {
                    gasto.IdGasto = int.Parse(hfIdGastoEdicion.Value);
                    negocio.ModificarGasto(gasto);
                }
                else
                {
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
                GastoResumenNegocio negocioResumen = new GastoResumenNegocio();
                List<GastoResumen> gastosFrescos = negocioResumen.ObtenerGastosDelMes(usuarioLogueado.IdUsuario);
                CargarGraficoDeTorta(gastosFrescos);
                upGraficoTorta.Update();
                cargarDashboardInfo();
                CargarMovimientosDelMesRecientes();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "okGasto",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Gasto guardado correctamente.'});", true);
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
                    //lblGastosMesHogar.Text = "$ " + total.ToString("N2");
                }
                else
                {
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];
                    total = negocio.TotalGastosMesActual(usuarioLogueado.IdUsuario);
                    h2Gasto.InnerText = "$ " + total.ToString("N2");
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
                ingresos = ingresoNegocio.ListarPorUsuarioMesActual(usuarioLogueado.IdUsuario, MesSeleccionado, AnioSeleccionado);
                gastos = gastoNegocio.ListarPorUsuarioMesActual(usuarioLogueado.IdUsuario, MesSeleccionado, AnioSeleccionado);
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

        private List<Movimiento> ObtenerMovimientosDelMesRecientes()
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
                ingresos = ingresoNegocio.ListarPorUsuarioMesReciente(usuarioLogueado.IdUsuario);
                gastos = gastoNegocio.ListarPorUsuarioMesReciente(usuarioLogueado.IdUsuario);
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
                mov.idReferencia = ingreso.IdIngreso;

                movimientos.Add(mov);
            }

            foreach (Gasto gasto in gastos)
            {
                Movimiento mov = new Movimiento();
                mov.Fecha = gasto.Fecha;
                mov.Descripcion = gasto.EsEnCuotas
     ? $"{gasto.Descripcion} (cuota {gasto.NumeroCuota}/{gasto.CantidadCuotas})"
     : gasto.Descripcion;
                mov.Categoria = gasto.Categoria.Nombre;
                mov.Tipo = "Gasto";
                mov.Monto = gasto.MontoPesos;
                mov.Estado = gasto.Estado ? "Activo" : "Eliminado";
                mov.idReferencia = gasto.IdGasto;

                movimientos.Add(mov);
            }

            return movimientos.OrderByDescending(x => x.Fecha).Take(8).ToList();
        }

        protected void CargarMovimientosDelMes()
        {
            List<Movimiento> lista = ObtenerMovimientosDelMes();

            rptMovimientos.DataSource = lista;
            rptMovimientos.DataBind();

            //lblMesActual.Text = new DateTime(AnioSeleccionado, MesSeleccionado, 1).ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));
        }

        protected void CargarMovimientosDelMesRecientes()
        {
            List<Movimiento> lista = ObtenerMovimientosDelMesRecientes();

            rptMovimientos.DataSource = lista;
            rptMovimientos.DataBind();

            //lblMesActual.Text = new DateTime(AnioSeleccionado, MesSeleccionado, 1).ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));
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
                h2Saldo.InnerText = "$ " + total.ToString("N2");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarMesesAnios()
        {
            // Meses
            //ddlMesIngresos.Items.Clear();
            //ddlMesGastos.Items.Clear();
            var meses = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < 12; i++)
            {
                string nombre = meses[i];
                //ddlMesIngresos.Items.Add(new ListItem(nombre, (i + 1).ToString()));
                //ddlMesGastos.Items.Add(new ListItem(nombre, (i + 1).ToString()));
            }

            // Años: últimos 5 años hasta el año actual
            //ddlAnioIngresos.Items.Clear();
            //ddlAnioGastos.Items.Clear();
            int anioActual = DateTime.Now.Year;
            for (int y = anioActual; y >= anioActual - 5; y--)
            {
                //ddlAnioIngresos.Items.Add(new ListItem(y.ToString(), y.ToString()));
                //ddlAnioGastos.Items.Add(new ListItem(y.ToString(), y.ToString()));
            }
        }

        private bool TryParseDecimalFlexible(string input, CultureInfo preferredCulture, out decimal result)
        {
            result = 0m;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();

            // 1) Intentar con la cultura preferida (ej. es-AR para ARS)
            if (decimal.TryParse(input, NumberStyles.Number, preferredCulture, out result))
                return true;

            // 2) Intentar con Invariant (punto decimal)
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                return true;

            // 3) Intentar con es-AR si aún no fue la preferida
            var esAr = new CultureInfo("es-AR");
            if (!preferredCulture.Equals(esAr) && decimal.TryParse(input, NumberStyles.Number, esAr, out result))
                return true;

            // 4) Normalizar reemplazando coma por punto y probar Invariant
            var normalized = input.Replace(',', '.');
            if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                return true;

            return false;
        }

        private void CargarIngresosPorMes(int mes, int anio)
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                IngresoNegocio ingresoNegocio = new IngresoNegocio();
                var lista = ingresoNegocio.ListarPorUsuarioPorMes(usuarioLogueado.IdUsuario, mes, anio);

                //gvIngresosMes.DataSource = lista;
                //gvIngresosMes.DataBind();
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

            //if (!int.TryParse(ddlMesIngresos.SelectedValue, out mes))
            mes = DateTime.Now.Month;

            //if (!int.TryParse(ddlAnioIngresos.SelectedValue, out anio))
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

                //gvGastosMes.DataSource = datos;
                //gvGastosMes.DataBind();
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

            //if (!int.TryParse(ddlMesGastos.SelectedValue, out mes))
            mes = DateTime.Now.Month;

            //if (!int.TryParse(ddlAnioGastos.SelectedValue, out anio))
            anio = DateTime.Now.Year;

            CargarGastosPorMes(mes, anio);
        }

        protected void lnkVerMetricas_Click(object sender, EventArgs e)
        {
            // Mostrar el panel de reportes y cargar datos iniciales (mes corriente)
            //pnlReportes.Visible = true;

            CargarMesesAnios();
            // Ingresos
            //ddlMesIngresos.SelectedValue = DateTime.Now.Month.ToString();
            //ddlAnioIngresos.SelectedValue = DateTime.Now.Year.ToString();
            CargarIngresosPorMes(DateTime.Now.Month, DateTime.Now.Year);

            // Gastos
            //ddlMesGastos.SelectedValue = DateTime.Now.Month.ToString();
            //ddlAnioGastos.SelectedValue = DateTime.Now.Year.ToString();
            CargarGastosPorMes(DateTime.Now.Month, DateTime.Now.Year);

            // Hacer scroll suave hacia el contenedor de reportes
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollReportes",$"document.getElementById('{pnlReportes.ClientID}').scrollIntoView({{behavior:'smooth'}});", true);
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

                if (user != null)
                {
                    if (negocioHogar.AgregarIntegrante((int)Session["IdHogarActual"], user.IdUsuario, "Miembro"))
                    {
                        /*--------------------------------ENVIO DE MAIL----------------------------------*/
                        Usuario usuarioLogueado = (Usuario)Session["usuario"];
                        Hogar hogar = (Hogar)Session["HogarSeleccionado"];

                        string rutaPlantillas = Server.MapPath("~/Template");

                        var reemplazos = new Dictionary<string, string>()
                        {
                            { "NOMBRE_USUARIO", user.Nombre },
                            { "NOMBRE_HOGAR", hogar.Nombre },
                            { "ADMIN_HOGAR", usuarioLogueado.Nombre }
                        };

                        EmailService servicio = new EmailService();

                        servicio.armarCorreo(
                            user.Email,
                            "Te han agregado a un hogar",
                            reemplazos,
                            TipoCorreo.TeAgregaronAHogar,
                            rutaPlantillas
                        );

                        servicio.enviarCorreo();
                        /*--------------------------------------------------------------------------------*/

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
            return;
            Usuario usuario = (Usuario)Session["usuario"];
            PresupuestoCategoriaNegocio negocio = new PresupuestoCategoriaNegocio();

            var lista = negocio.ListarPorUsuarioYMes(usuario.IdUsuario, DateTime.Now.Month, DateTime.Now.Year)
                               .FindAll(p => p.MontoPresupuestado > 0);

            //rptPresupuesto.DataSource = lista;
            //rptPresupuesto.DataBind();
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

        private void CargarGraficoDeTorta(List<GastoResumen> gastosMes)
        {
            GastoResumenNegocio negocio = new GastoResumenNegocio();

            decimal totalGastos = gastosMes.Sum(g => g.Monto);

            if (totalGastos == 0)
            {
                litTotalGrafico.Text = "$0";
                return;
            }

            litTotalGrafico.Text = totalGastos >= 1000 ? "$" + (totalGastos / 1000).ToString("0k") : "$" + totalGastos.ToString("0");

            var categoriasAgrupadas = gastosMes
                .GroupBy(g => g.Categoria)
                .Select(grupo => new { Nombre = grupo.Key, Total = grupo.Sum(x => x.Monto) })
                .OrderByDescending(x => x.Total)
                .ToList();

            string[] coloresHex = { "#0d6efd", "#198754", "#ffc107", "#e9ecef" };
            List<ItemGrafico> datosGrafico = new List<ItemGrafico>();

            decimal offsetActual = 0;

            for (int i = 0; i < Math.Min(3, categoriasAgrupadas.Count); i++)
            {
                var cat = categoriasAgrupadas[i];
                decimal porcentaje = (cat.Total / totalGastos) * 100;

                datosGrafico.Add(new ItemGrafico
                {
                    Nombre = cat.Nombre,
                    Monto = cat.Total,
                    Porcentaje = porcentaje,
                    Offset = offsetActual,
                    ColorHex = coloresHex[i]
                });

                offsetActual -= porcentaje;
            }
            if (categoriasAgrupadas.Count > 3)
            {
                decimal montoOtros = categoriasAgrupadas.Skip(3).Sum(x => x.Total);
                decimal porcentajeOtros = (montoOtros / totalGastos) * 100;

                datosGrafico.Add(new ItemGrafico
                {
                    Nombre = "Otros",
                    Monto = montoOtros,
                    Porcentaje = porcentajeOtros,
                    Offset = offsetActual,
                    ColorHex = coloresHex[3]
                });
            }
            rptGraficoTorta.DataSource = datosGrafico;
            rptGraficoTorta.DataBind();

            rptLeyendaGrafico.DataSource = datosGrafico;
            rptLeyendaGrafico.DataBind();
        }

        public void cargarMetasAhorro()
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            MetaResumenNegocio negocio = new MetaResumenNegocio();
            List<MetaResumen> misMetas = negocio.obtenerMetasPorUsuario(usuarioLogueado.IdUsuario);

            if (misMetas.Count == 0)
            {
                // Prendemos el cartelito vacío
                pnlMetasVacias.Visible = true;
                pnlMetasActivas.Visible = false;
            }
            else
            {
                pnlMetasVacias.Visible = false;
                pnlMetasActivas.Visible = true;

                var metasParaDashboard = misMetas
                .OrderByDescending(m => m.Porcentaje)
                .Take(2)
                .ToList();

                rptMetasDashboard.DataSource = metasParaDashboard;
                rptMetasDashboard.DataBind();
            }
        }

        protected void rptMovimientos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string argumento = e.CommandArgument.ToString();
                string[] datos = argumento.Split('|');

                string tipoMovimiento = datos[0];
                int idReal = int.Parse(datos[1]);

                // ==========================================
                // ACCIÓN 1: ELIMINAR
                // ==========================================
                if (e.CommandName == "Eliminar")
                {
                    if (tipoMovimiento == "Gasto")
                    {
                        GastoNegocio negocioGasto = new GastoNegocio();
                        negocioGasto.EliminarGasto(idReal);
                        ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "gastoModificado",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'El gasto fue eliminado correctamente!'});",
                    true);
                    }
                    else if (tipoMovimiento == "Ingreso")
                    {
                        IngresoNegocio negocioIngreso = new IngresoNegocio();
                        negocioIngreso.EliminarIngreso(idReal);
                        ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "ingresoModificado",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'El ingreso fue eliminado correctamente!'});",
                    true);
                    }
                }

                // ==========================================
                // ACCIÓN 2: EDITAR
                // ==========================================
                else if (e.CommandName == "Editar")
                {
                    if (tipoMovimiento == "Ingreso")
                    {

                        IngresoNegocio negocioIngreso = new IngresoNegocio();
                        Ingreso objIngreso = negocioIngreso.ListarPorId(idReal);

                        modalIngresoLabel.InnerText = "Editar Ingreso";
                        hfIdIngresoEdicion.Value = objIngreso.IdIngreso.ToString();

                        txtDescripcionIngreso.Text = objIngreso.Descripcion;
                        txtMontoIngreso.Text = objIngreso.Monto.ToString("0.00", CultureInfo.InvariantCulture);

                        txtFechaIngreso.Text = objIngreso.Fecha.ToString("yyyy-MM-dd");

                        ddlCategoriaIngreso.SelectedValue = objIngreso.Categoria.IdCategoria.ToString();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalIngreso",
                        "window.addEventListener('load', function() { " +
                        "   var myModal = new bootstrap.Modal(document.getElementById('modalIngreso')); " +
                        "   myModal.show(); " +
                        "});", true);


                    }
                    else if (tipoMovimiento == "Gasto")
                    {
                        
                        GastoNegocio negocioGasto = new GastoNegocio();
                        Gasto objGasto = negocioGasto.ListarPorId(idReal);

                        modalGastoLabel.InnerText = "Editar Gasto";
                        hfIdGastoEdicion.Value = objGasto.IdGasto.ToString();
                        txtDescripcionGasto.Text = objGasto.Descripcion;
                        txtFechaGasto.Text = objGasto.Fecha.ToString("yyyy-MM-dd");
                        ddlCategoriaGasto.SelectedValue = objGasto.Categoria.IdCategoria.ToString();
                        ddlMedioPagoGasto.SelectedValue = objGasto.MedioDePago.IdMedioPago.ToString();
                        if (objGasto.EsEnCuotas)
                        {
                            rbCuotas.Checked = true;
                            rbUnPago.Checked = false;
                            txtMontoCuotaGasto.Text = objGasto.MontoCuota.ToString("0.00", CultureInfo.InvariantCulture);
                            txtCantidadCuotasGasto.Text = objGasto.CantidadCuotas.ToString();
                        }
                        else if (objGasto.MontoUSD != 0) 
                        {
;                           txtMontoUSDGasto.Text = objGasto.MontoUSD.ToString("0.00", CultureInfo.InvariantCulture);
                            txtCotizacionGasto.Text = objGasto.Cotizacion.ToString("0.00", CultureInfo.InvariantCulture);
                        }

                        txtMontoPesosGasto.Text = objGasto.MontoPesos.ToString("0.00", CultureInfo.InvariantCulture);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalGasto",
                            "window.addEventListener('load', function() { " +
                            "   var myModal = new bootstrap.Modal(document.getElementById('modalGasto')); " +
                            "   myModal.show(); " +
                            "});", true);
                    }
                }
                CargarMovimientosDelMesRecientes();
                cargarDashboardInfo();
            }
            catch (Exception ex)
            {
                // Buena práctica: Si algo falla (ej: se cayó la BD), atajamos el error
                // Podés usar un ScriptManager acá para tirar un alert de JavaScript
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error",
                    $"alert('Ocurrió un error al procesar el movimiento: {ex.Message}');", true);
            }
        }
    }
}