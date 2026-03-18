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
            }
        }

        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                {
                    lblMensajeCategoria.Text = "Ingresá un nombre para la categoría.";
                    lblMensajeCategoria.CssClass = "text-danger d-block text-center mt-3";

                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "mostrarModal",
                        "var modal = new bootstrap.Modal(document.getElementById('modalCategoria')); modal.show();",
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

                lblMensajeCategoria.Text = "Categoría guardada correctamente.";
                lblMensajeCategoria.CssClass = "text-success d-block text-center mt-3";

                LimpiarModalCategoria();
                CargarCategoriasIngreso();
                CargarCategoriasGasto();
            }
            catch (Exception ex)
            {
                lblMensajeCategoria.Text = ex.Message;
                lblMensajeCategoria.CssClass = "text-danger d-block text-center mt-3";

                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "mostrarModal",
                    "var modal = new bootstrap.Modal(document.getElementById('modalCategoria')); modal.show();",
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
                    lblMensajeIngreso.Text = "Completá todos los campos obligatorios.";
                    lblMensajeIngreso.CssClass = "text-danger d-block text-center mt-3";

                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "mostrarModalIngreso",
                        "var modal = new bootstrap.Modal(document.getElementById('modalIngreso')); modal.show();",
                        true);
                    return;
                }

                if (ddlCategoriaIngreso.SelectedValue == "0")
                {
                    lblMensajeIngreso.Text = "Debés seleccionar una categoría.";
                    lblMensajeIngreso.CssClass = "text-danger d-block text-center mt-3";

                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "mostrarModalIngreso",
                        "var modal = new bootstrap.Modal(document.getElementById('modalIngreso')); modal.show();",
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

                lblMensajeIngreso.Text = "Ingreso guardado correctamente.";
                lblMensajeIngreso.CssClass = "text-success d-block text-center mt-3";

                txtDescripcionIngreso.Text = "";
                txtFechaIngreso.Text = "";
                txtMontoIngreso.Text = "";
                ddlCategoriaIngreso.SelectedIndex = 0;

                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "mostrarModalIngreso",
                    "var modal = new bootstrap.Modal(document.getElementById('modalIngreso')); modal.show();",
                    true);

                CargarResumenIngresos(); //refresca los ingresos luego de agregar uno nuevo
                CargarSaldoMes(); //refresca el saldo al agregar un ingreso nuevo


                CargarMovimientosDelMes();
            }
            catch (Exception ex)
            {
                lblMensajeIngreso.Text = ex.Message;
                lblMensajeIngreso.CssClass = "text-danger d-block text-center mt-3";

                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "mostrarModalIngreso",
                    "var modal = new bootstrap.Modal(document.getElementById('modalIngreso')); modal.show();",
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
                    lblMensajeMedioPago.Text = "Completá los campos obligatorios.";
                    lblMensajeMedioPago.CssClass = "text-danger d-block text-center mt-3";

                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "mostrarModalMedioPago",
                        "var modal = new bootstrap.Modal(document.getElementById('modalMedioPago')); modal.show(); toggleCamposCredito();",
                        true);
                    return;
                }

                TipoPago tipoSeleccionado = (TipoPago)int.Parse(ddlTipoMedioPago.SelectedValue);

                if (tipoSeleccionado == TipoPago.Credito)
                {
                    if (string.IsNullOrWhiteSpace(txtDiaCierre.Text) ||
                        string.IsNullOrWhiteSpace(txtDiaVencimiento.Text))
                    {
                        lblMensajeMedioPago.Text = "Para tarjeta de crédito debés completar día de cierre y vencimiento.";
                        lblMensajeMedioPago.CssClass = "text-danger d-block text-center mt-3";

                        ScriptManager.RegisterStartupScript(
                            this, this.GetType(),
                            "mostrarModalMedioPago",
                            "var modal = new bootstrap.Modal(document.getElementById('modalMedioPago')); modal.show(); toggleCamposCredito();",
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

                lblMensajeMedioPago.Text = "Medio de pago guardado correctamente.";
                lblMensajeMedioPago.CssClass = "text-success d-block text-center mt-3";

                txtDescripcionMedioPago.Text = "";
                txtDiaCierre.Text = "";
                txtDiaVencimiento.Text = "";
                ddlTipoMedioPago.SelectedIndex = 0;

                CargarMediosPago();

                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "mostrarModalMedioPago",
                    "var modal = new bootstrap.Modal(document.getElementById('modalMedioPago')); modal.show(); document.getElementById('camposCredito').style.display='none';",
                    true);
            }
            catch (Exception ex)
            {
                lblMensajeMedioPago.Text = ex.Message;
                lblMensajeMedioPago.CssClass = "text-danger d-block text-center mt-3";

                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "mostrarModalMedioPago",
                    "var modal = new bootstrap.Modal(document.getElementById('modalMedioPago')); modal.show(); toggleCamposCredito();",
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
                    lblMensajeGasto.Text = "Completá los campos obligatorios.";
                    lblMensajeGasto.CssClass = "text-danger d-block text-center mt-3";

                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "mostrarModalGasto",
                        "var modal = new bootstrap.Modal(document.getElementById('modalGasto')); modal.show();",
                        true);
                    return;
                }

                if (ddlCategoriaGasto.SelectedValue == "0")
                {
                    lblMensajeGasto.Text = "Seleccioná una categoría.";
                    lblMensajeGasto.CssClass = "text-danger d-block text-center mt-3";

                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "mostrarModalGasto",
                        "var modal = new bootstrap.Modal(document.getElementById('modalGasto')); modal.show();",
                        true);
                    return;
                }

                if (ddlMedioPagoGasto.SelectedValue == "0")
                {
                    lblMensajeGasto.Text = "Seleccioná un medio de pago.";
                    lblMensajeGasto.CssClass = "text-danger d-block text-center mt-3";

                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "mostrarModalGasto",
                        "var modal = new bootstrap.Modal(document.getElementById('modalGasto')); modal.show();",
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

                lblMensajeGasto.Text = "Gasto guardado correctamente.";
                lblMensajeGasto.CssClass = "text-success d-block text-center mt-3";

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
                    "mostrarModalGasto",
                    "var modal = new bootstrap.Modal(document.getElementById('modalGasto')); modal.show();",
                    true);
            }
            catch (Exception ex)
            {
                lblMensajeGasto.Text = ex.Message;
                lblMensajeGasto.CssClass = "text-danger d-block text-center mt-3";

                ScriptManager.RegisterStartupScript(
                    this, this.GetType(),
                    "mostrarModalGasto",
                    "var modal = new bootstrap.Modal(document.getElementById('modalGasto')); modal.show();",
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


    }
}
    
