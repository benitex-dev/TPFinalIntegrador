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
    }
}
    
