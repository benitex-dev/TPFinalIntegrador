using dominio;
using negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalIntegrador
{
    public partial class Personalizaciones : System.Web.UI.Page
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
                CargarCategorias();
                CargarMediosPago();
            }
        }

        private void CargarCategorias()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            CategoriaNegocio negocio = new CategoriaNegocio();
            gvCategorias.DataSource = negocio.ListarPorUsuario(usuario.IdUsuario);
            gvCategorias.DataBind();
        }

        private void CargarMediosPago()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            MedioPagoNegocio negocio = new MedioPagoNegocio();
            gvMediosPago.DataSource = negocio.ListarPorUsuario(usuario.IdUsuario);
            gvMediosPago.DataBind();
        }

        protected void gvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategorias.EditIndex = e.NewEditIndex;
            CargarCategorias();
        }

        protected void gvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategorias.EditIndex = -1;
            CargarCategorias();
        }

        protected void gvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idCategoria = (int)gvCategorias.DataKeys[e.RowIndex].Value;
            string nombre = ((TextBox)gvCategorias.Rows[e.RowIndex].Cells[0].Controls[0]).Text;

            Usuario usuario = (Usuario)Session["usuario"];
            CategoriaNegocio negocio = new CategoriaNegocio();
            var lista = negocio.ListarPorUsuario(usuario.IdUsuario);
            var categoria = lista.Find(c => c.IdCategoria == idCategoria);

            if (categoria != null)
            {
                categoria.Nombre = nombre;
                try
                {
                    negocio.ModificarCategoria(categoria);
                    gvCategorias.EditIndex = -1;
                    CargarCategorias();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "okCategoria",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Categoría modificada correctamente.'});", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCategoria",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
                }
            }
        }

        protected void gvMediosPago_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMediosPago.EditIndex = e.NewEditIndex;
            CargarMediosPago();
        }

        protected void gvMediosPago_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMediosPago.EditIndex = -1;
            CargarMediosPago();
        }

        protected void gvMediosPago_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idMedioPago = (int)gvMediosPago.DataKeys[e.RowIndex].Value;
            string descripcion = ((TextBox)gvMediosPago.Rows[e.RowIndex].Cells[0].Controls[0]).Text;

            Usuario usuario = (Usuario)Session["usuario"];
            MedioPagoNegocio negocio = new MedioPagoNegocio();
            var lista = negocio.ListarPorUsuario(usuario.IdUsuario);
            var medio = lista.Find(m => m.IdMedioPago == idMedioPago);

            if (medio != null)
            {
                medio.Descripcion = descripcion;
                try
                {
                    negocio.ModificarMedioPago(medio);
                    gvMediosPago.EditIndex = -1;
                    CargarMediosPago();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "okMedioPago", "alert('Medio de pago modificado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMedioPago", $"alert('{ex.Message.Replace("'", "\\'")}');", true);
                }
            }
        }
    }
}