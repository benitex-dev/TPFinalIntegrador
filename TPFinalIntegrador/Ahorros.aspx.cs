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
    public partial class Ahorros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
                CargarMetas();
        }
        private void CargarMetas()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            MetaAhorroNegocio negocio = new MetaAhorroNegocio();
            gvMetas.DataSource = negocio.Listar(idUsuario: usuario.IdUsuario, estado: EstadoMetaAhorro.Activa);
            gvMetas.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];

                MetaAhorro nueva = new MetaAhorro();
                nueva.Nombre = txtNombre.Text;
                nueva.MontoObjetivo = decimal.Parse(txtMonto.Text);
                nueva.FechaObjetivo = DateTime.Parse(txtFecha.Text);
                nueva.Usuario = usuario;
                nueva.Estado = EstadoMetaAhorro.Activa;

                MetaAhorroNegocio negocio = new MetaAhorroNegocio();
                negocio.AgregarMetaAhorro(nueva);

                txtNombre.Text = "";
                txtMonto.Text = "";
                txtFecha.Text = "";

                CargarMetas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Meta de ahorro agregada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void gvMetas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMetas.EditIndex = e.NewEditIndex;
            CargarMetas();
        }

        protected void gvMetas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMetas.EditIndex = -1;
            CargarMetas();
        }

        protected void gvMetas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idMeta = (int)gvMetas.DataKeys[e.RowIndex].Value;
                string nombre = ((TextBox)gvMetas.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
                string monto = ((TextBox)gvMetas.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
                string fecha = ((TextBox)gvMetas.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

                Usuario usuario = (Usuario)Session["usuario"];

                MetaAhorro meta = new MetaAhorro();
                meta.IdMeta = idMeta;
                meta.Nombre = nombre;
                meta.MontoObjetivo = decimal.Parse(monto);
                meta.FechaObjetivo = DateTime.Parse(fecha);
                meta.Usuario = usuario;
                meta.Estado = EstadoMetaAhorro.Activa;

                MetaAhorroNegocio negocio = new MetaAhorroNegocio();
                negocio.ModificarMeta(meta);

                gvMetas.EditIndex = -1;
                CargarMetas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Meta modificada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void gvMetas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idMeta = (int)gvMetas.DataKeys[e.RowIndex].Value;
                MetaAhorroNegocio negocio = new MetaAhorroNegocio();
                negocio.EliminarLogico(idMeta);

                CargarMetas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Meta eliminada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }


    }
}