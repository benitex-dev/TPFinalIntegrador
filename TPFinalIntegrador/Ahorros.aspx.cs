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
           // MetaAhorroNegocio negocio = new MetaAhorroNegocio();
            MetaResumenNegocio negocio = new MetaResumenNegocio();
            var lista = negocio.obtenerMetasPorUsuario(usuario.IdUsuario)
                .OrderBy(x => x.FechaObjetivo ?? DateTime.MaxValue)
                .ToList(); ;
            // gvMetas.DataSource = negocio.Listar(idUsuario: usuario.IdUsuario, estado: EstadoMetaAhorro.Activa);
            //gvMetas.DataBind();
            pnlSinMetas.Visible = lista.Count == 0;
            rptMetas.DataSource = lista;
            rptMetas.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];

                MetaAhorro nueva = new MetaAhorro();
                nueva.Nombre = txtNombre.Text;
                nueva.MontoObjetivo = ParseDecimal(txtMonto.Text);
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

        //protected void gvMetas_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvMetas.EditIndex = e.NewEditIndex;
        //    CargarMetas();
        //}

        //protected void gvMetas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gvMetas.EditIndex = -1;
        //    CargarMetas();
        //}

        //protected void gvMetas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        int idMeta = (int)gvMetas.DataKeys[e.RowIndex].Value;
        //        string nombre = ((TextBox)gvMetas.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
        //        string monto = ((TextBox)gvMetas.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        //        string fecha = ((TextBox)gvMetas.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

        //        Usuario usuario = (Usuario)Session["usuario"];

        //        MetaAhorro meta = new MetaAhorro();
        //        meta.IdMeta = idMeta;
        //        meta.Nombre = nombre;
        //        meta.MontoObjetivo = decimal.Parse(monto);
        //        meta.FechaObjetivo = DateTime.Parse(fecha);
        //        meta.Usuario = usuario;
        //        meta.Estado = EstadoMetaAhorro.Activa;

        //        MetaAhorroNegocio negocio = new MetaAhorroNegocio();
        //        negocio.ModificarMeta(meta);

        //        gvMetas.EditIndex = -1;
        //        CargarMetas();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
        //            "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Meta modificada correctamente.'});", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
        //            $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
        //    }
        //}

        //protected void gvMetas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        int idMeta = (int)gvMetas.DataKeys[e.RowIndex].Value;
        //        MetaAhorroNegocio negocio = new MetaAhorroNegocio();
        //        negocio.EliminarLogico(idMeta);

        //        CargarMetas();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
        //            "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Meta eliminada correctamente.'});", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
        //            $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
        //    }
        //}

        protected void btnConfirmarAporte_Click(object sender, EventArgs e)
        {
            try
            {
                AporteMeta aporte = new AporteMeta();
                aporte.Meta = new MetaAhorro();
                aporte.Meta.IdMeta = int.Parse(hfIdMetaAporte.Value);
                aporte.Monto = ParseDecimal(txtMontoAporte.Text);
                MetaAhorroNegocio negocio = new MetaAhorroNegocio();
                negocio.AgregarAporte(aporte);

                txtMontoAporte.Text = "";
                CargarMetas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Aporte registrado correctamente.'});",
true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];

                MetaAhorro meta = new MetaAhorro();
                meta.IdMeta = int.Parse(hfIdMetaEditar.Value);
                meta.Nombre = txtNombreEditar.Text;
                meta.MontoObjetivo = ParseDecimal(txtMontoEditar.Text);
                meta.Usuario = usuario;
                meta.Estado = EstadoMetaAhorro.Activa;

                MetaAhorroNegocio negocio = new MetaAhorroNegocio();
                negocio.ModificarMeta(meta);

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

        protected void rptMetas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idMeta = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Eliminar")
            {
                try
                {
                    MetaAhorroNegocio negocio = new MetaAhorroNegocio();
                    negocio.EliminarLogico(idMeta);
                    CargarMetas();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                        "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Meta eliminada correctamente.'});",
true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                        $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});",
true);
                }
            }
            else if (e.CommandName == "Editar")
            {
                MetaAhorroNegocio negocio = new MetaAhorroNegocio();
                MetaAhorro meta = negocio.ObtenerPorId(idMeta);

                hfIdMetaEditar.Value = idMeta.ToString();
                txtNombreEditar.Text = meta.Nombre;
                txtMontoEditar.Text = meta.MontoObjetivo.ToString();
                txtFechaEditar.Text = meta.FechaObjetivo?.ToString("yyyy-MM-dd");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirEditar",
                    "new bootstrap.Modal(document.getElementById('modalEditar')).show();", true);
            }
        }
        private decimal ParseDecimal(string texto)
        {
            return decimal.Parse(texto.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}