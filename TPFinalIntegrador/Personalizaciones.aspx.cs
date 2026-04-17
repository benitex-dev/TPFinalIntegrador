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

        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idCategoria = (int)gvCategorias.DataKeys[e.RowIndex].Value;

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.EliminarLogico(idCategoria);

                // Recargar la grilla
                CargarCategorias();

                // Mostrar notificación
                ScriptManager.RegisterStartupScript(this, this.GetType(), "okEliminarCategoria",
                    "Swal.fire({icon: 'success', title: 'Eliminado', text: 'Categoría eliminada correctamente.'});",
                    true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorEliminarCategoria",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void gvMediosPago_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idMedioPago = (int)gvMediosPago.DataKeys[e.RowIndex].Value;

                MedioPagoNegocio negocio = new MedioPagoNegocio();
                negocio.EliminarMedioPago(idMedioPago);

                // Recargar la grilla
                CargarMediosPago();

                // Notificar con SweetAlert2
                ScriptManager.RegisterStartupScript(this, this.GetType(), "okEliminarMedio",
                    "Swal.fire({icon: 'success', title: 'Eliminado', text: 'Medio de pago eliminado correctamente.'});",
                    true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorEliminarMedio",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "okMedioPago",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Medio de pago modificado correctamente.'});", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorModificarMedio",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
                }
            }
        }

        // --- NUEVOS HANDLERS para los modales ---
        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ingresaNombre", "Swal.fire({icon: 'error', title: 'Error', text: 'Ingresá un nombre para la categoría.'});", true);
                    return;
                }

                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                Categoria nueva = new Categoria
                {
                    Nombre = txtNombreCategoria.Text.Trim(),
                    Usuario = usuarioLogueado,
                    Tipo = (TipoCategoria)int.Parse(ddlTipoCategoria.SelectedValue),
                    Estado = true
                };

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.AgregarCategoria(nueva);

                LimpiarModalCategoria();
                CargarCategorias();
                CargarMediosPago(); // por si hay dependencia visual
                ScriptManager.RegisterStartupScript(this, this.GetType(), "categoriaCreada", "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Categoría creada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCategoria", $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void btnGuardarMedioPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoMedioPago.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtDescripcionMedioPago.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMedioCampos", "Swal.fire({icon: 'error', title: 'Error', text: 'Completá los campos obligatorios.'});", true);
                    return;
                }

                TipoPago tipo = (TipoPago)int.Parse(ddlTipoMedioPago.SelectedValue);
                MedioPago medio = new MedioPago
                {
                    Tipo = tipo,
                    Descripcion = txtDescripcionMedioPago.Text.Trim(),
                    Usuario = (Usuario)Session["usuario"],
                    Estado = true
                };

                if (tipo == TipoPago.Credito)
                {
                    if (string.IsNullOrWhiteSpace(txtDiaCierre.Text) || string.IsNullOrWhiteSpace(txtDiaVencimiento.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorTarjeta", "Swal.fire({icon: 'error', title: 'Error', text: 'Para tarjeta de crédito debés completar día de cierre y vencimiento.'});", true);
                        return;
                    }
                    medio.DiaCierre = int.Parse(txtDiaCierre.Text);
                    medio.DiaVencimiento = int.Parse(txtDiaVencimiento.Text);
                }

                MedioPagoNegocio negocio = new MedioPagoNegocio();
                negocio.AgregarMedioPago(medio);

                LimpiarModalMedioPago();
                CargarMediosPago();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "medioCreado", "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Medio de pago guardado correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMedio", $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        // Reutilizo las mismas funciones de limpieza que definimos en JS, pero en servidor también dejo helpers si los necesitas
        protected void LimpiarModalCategoria()
        {
            txtNombreCategoria.Text = "";
            ddlTipoCategoria.SelectedIndex = 0;
            lblMensajeCategoria.Text = "";
            lblMensajeCategoria.CssClass = "";
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
    }
}