using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalIntegrador
{
    public partial class Movimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMovimientos();
            }
        }

        protected void CargarMovimientos()
        {

            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            MovimientoNegocio negocio = new MovimientoNegocio();
            List<Movimiento> lista = negocio.ListarMovimientosDelMes(false, usuarioLogueado.IdUsuario);

            rptMovimientos.DataSource = lista;
            rptMovimientos.DataBind();

            //lblMesActual.Text = new DateTime(AnioSeleccionado, MesSeleccionado, 1).ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));
        }

        protected DateTime FechaGrupoAnterior = DateTime.MinValue;

        protected string ObtenerCabeceraFecha(object fechaObj)
        {
            DateTime fechaActual = Convert.ToDateTime(fechaObj);

            // Si cambiamos de día, imprimimos la franja gris con la fecha
            if (fechaActual.Date != FechaGrupoAnterior.Date)
            {
                FechaGrupoAnterior = fechaActual.Date;

                string tituloFecha = "";
                if (fechaActual.Date == DateTime.Today)
                    tituloFecha = "HOY";
                else if (fechaActual.Date == DateTime.Today.AddDays(-1))
                    tituloFecha = "AYER";
                else
                    tituloFecha = fechaActual.ToString("dd 'DE' MMMM", new System.Globalization.CultureInfo("es-AR")).ToUpper();

                return $"<div class=\"cabeceraMovimientos py-1 px-3 bg-light text-muted fw-bold\" style=\"font-size: 0.75rem; letter-spacing: 0.05em;\">{tituloFecha}</div>";
            }

            return string.Empty;
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
                            ; txtMontoUSDGasto.Text = objGasto.MontoUSD.ToString("0.00", CultureInfo.InvariantCulture);
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
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error",
                    $"alert('Ocurrió un error al procesar el movimiento: {ex.Message}');", true);
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
                if (!TryParseDecimalFlexible(txtMontoIngreso.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture, out montoIngreso))
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
    }
}