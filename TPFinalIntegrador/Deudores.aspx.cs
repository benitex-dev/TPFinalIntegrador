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
    public partial class Deudores : System.Web.UI.Page
    {
        Dictionary<string, int> estadosDeuda = new Dictionary<string, int>();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarDeudas();
            }
               
        }
        private void CargarDeudas()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            DeudaNegocio negocio = new DeudaNegocio();
          
            int idUsuario = usuario.IdUsuario;
            gvDeudas.DataSource = negocio.ListarPorUsuario(idUsuario);
            gvDeudas.DataBind();
        }
        private void CargarFiltros()
        {
            estadosDeuda.Add("Pendiente", 1);
            estadosDeuda.Add("Pago", 0);
            estadosDeuda.Add("Eliminado", 2);
            ddlFiltroEstadoDeuda.DataSource = estadosDeuda;
            ddlFiltroEstadoDeuda.DataTextField = "Key";
            ddlFiltroEstadoDeuda.DataValueField = "Value";

            ddlFiltroEstadoDeuda.DataBind();
            ddlFiltroEstadoDeuda.Items.Insert(0, new ListItem("Todos los estados", "-1"));
        }
       

        protected void gvDeudas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDeudas.EditIndex = e.NewEditIndex;
            CargarDeudas();

        }

        protected void gvDeudas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                int idDeuda = (int)gvDeudas.DataKeys[e.RowIndex]["IdDeuda"];
                string nombre = ((TextBox)gvDeudas.Rows[e.RowIndex].FindControl("txtNombre")).Text;
                string email = ((TextBox)gvDeudas.Rows[e.RowIndex].FindControl("txtEmail")).Text;
                string descripcion = ((TextBox)gvDeudas.Rows[e.RowIndex].FindControl("txtDescripcion")).Text;

                Usuario usuario = (Usuario)Session["usuario"];

                // Traemos la deuda original para no perder monto, cuotas y fecha
                DeudaNegocio negocio = new DeudaNegocio();
                Deuda deuda = negocio.ObtenerPorId(idDeuda);

                deuda.NombreDeudor = nombre;
                deuda.EmailDeudor = email;
                deuda.Descripcion = descripcion;

                negocio.ModificarDeuda(deuda);

                gvDeudas.EditIndex = -1;
                CargarDeudas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Deuda modificada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void gvDeudas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                int idDeuda = (int)gvDeudas.DataKeys[e.RowIndex]["IdDeuda"];
                string email = ((TextBox)gvDeudas.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
                string descripcion = ((TextBox)gvDeudas.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
                string monto = ((TextBox)gvDeudas.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
                string cuotas = ((TextBox)gvDeudas.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
                string fecha = ((TextBox)gvDeudas.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

                Usuario usuario = (Usuario)Session["usuario"];

                Deuda deuda = new Deuda();
                deuda.IdDeuda = idDeuda;
                deuda.Usuario = usuario;
                deuda.NombreDeudor = (string)gvDeudas.DataKeys[e.RowIndex]["NombreDeudor"];
                deuda.EmailDeudor = email;
                deuda.Descripcion = descripcion;
                deuda.MontoTotal = decimal.Parse(monto);
                deuda.Cuotas = int.Parse(cuotas);
                deuda.FechaInicio = DateTime.Parse(fecha);
                deuda.Estado = EstadoDeuda.Pendiente;

                DeudaNegocio negocio = new DeudaNegocio();
                negocio.ModificarDeuda(deuda);

                gvDeudas.EditIndex = -1;
                CargarDeudas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Deuda modificada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void gvDeudas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                string nombreDeudor = (string)gvDeudas.DataKeys[e.RowIndex]["NombreDeudor"];

                DeudaNegocio negocio = new DeudaNegocio();
                negocio.EliminarLogico(usuario.IdUsuario, nombreDeudor);

                CargarDeudas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Deuda eliminada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }
        }

        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            try
            {

                Usuario usuario = (Usuario)Session["usuario"];

                Deuda nueva = new Deuda();
                nueva.Usuario = usuario;
                nueva.NombreDeudor = txtNombre.Text;
                nueva.EmailDeudor = txtEmail.Text;
                nueva.Descripcion = txtDescripcion.Text;
                nueva.MontoTotal = decimal.Parse(txtMonto.Text);
                nueva.Cuotas = int.Parse(txtCuotas.Text);
                nueva.FechaInicio = DateTime.Parse(txtFecha.Text);
                nueva.Estado = EstadoDeuda.Pendiente;

                DeudaNegocio negocio = new DeudaNegocio();
                negocio.AgregarDeuda(nueva);
                GenerarCuotas(nueva);

                /*--------------ENVIO DE MAIL AL DEUDOR----------------------*/
                string rutaPlantillas = Server.MapPath("~/Template");
                var reemplazosDeudor = new Dictionary<string, string>()
                {
                    { "NOMBRE_DEUDOR", nueva.NombreDeudor },
                    { "NOMBRE_USUARIO", usuario.Nombre},
                    { "DESCRIPCION", nueva.Descripcion },
                    { "MONTO_TOTAL", nueva.MontoTotal.ToString("N2") },
                    { "CUOTAS", nueva.Cuotas.ToString() },
                    { "MONTO_CUOTA", (nueva.MontoTotal / nueva.Cuotas).Value.ToString("N2") },
                    { "FECHA", nueva.FechaInicio.ToString("dd/MM/yyyy") }
                };

                EmailService servicioDeudor = new EmailService();

                servicioDeudor.armarCorreo(
                    nueva.EmailDeudor,
                    "Aviso de adquisición de deuda",
                    reemplazosDeudor,
                    TipoCorreo.RegistroDeudaDeudor,
                    rutaPlantillas
                );

                servicioDeudor.enviarCorreo();
                /*---------------------------------------------------------------*/
                /*--------------ENVIO DE MAIL AL ACREEDOR----------------------*/
                var reemplazosUsuario = new Dictionary<string, string>()
                {
                    { "NOMBRE_DEUDOR", nueva.NombreDeudor },
                    { "NOMBRE_USUARIO", usuario.Nombre },
                    { "DESCRIPCION", nueva.Descripcion },
                    { "MONTO_TOTAL", nueva.MontoTotal.ToString("N2") },
                    { "CUOTAS", nueva.Cuotas.ToString() },
                    { "MONTO_CUOTA", (nueva.MontoTotal / nueva.Cuotas).Value.ToString("N2") },
                    { "FECHA", nueva.FechaInicio.ToString("dd/MM/yyyy") }
                };
                EmailService servicioUsuario = new EmailService();

                servicioUsuario.armarCorreo(
                    usuario.Email,
                    "Registro de deuda realizado correctamente",
                    reemplazosUsuario,
                    TipoCorreo.RegistroDeudaAcreedor,
                    rutaPlantillas
                );

                servicioUsuario.enviarCorreo();
                /*---------------------------------------------------------------*/

                txtNombre.Text = "";
                txtEmail.Text = "";
                txtDescripcion.Text = "";
                txtMonto.Text = "";
                txtCuotas.Text = "";
                txtFecha.Text = "";

                pnlFormulario.Visible = false;
                btnNuevaDeuda.Visible = true;
                CargarDeudas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "Swal.fire({icon: 'success', title: '¡Éxito!', text: 'Deuda agregada correctamente.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}'}});", true);
            }

        }

        protected void btnNuevaDeuda_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = true;
            btnNuevaDeuda.Visible = false;
        }

        protected void gvDeudas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDeudas.PageIndex = e.NewPageIndex;
            CargarDeudas();
        }

        protected void ddlFiltroEstadoDeuda_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
           DeudaNegocio negocio = new DeudaNegocio();
           Usuario usuario = (Usuario)Session["usuario"];
            int idUsuario = usuario.IdUsuario;  
            int estadoDeuda = 0;

             List<Deuda> deudasFiltradas = new List<Deuda>();
            
            if (!(string.IsNullOrEmpty(ddlFiltroEstadoDeuda.SelectedValue)))
            {
                estadoDeuda = int.Parse(ddlFiltroEstadoDeuda.SelectedValue);
            }

            if (estadoDeuda>= 0 && estadoDeuda <=3)
            {
                deudasFiltradas = negocio.FiltrarPorEstado(estadoDeuda,idUsuario);
            }
            else if(estadoDeuda == -1)
            {
                deudasFiltradas = negocio.FiltrarPorEstado(estadoDeuda, idUsuario);
            }

            if(deudasFiltradas.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "info",
                    "Swal.fire({icon: 'info', title: 'Sin resultados', text: 'No se encontraron deudas con el estado seleccionado.'});", true);
            }

            gvDeudas.DataSource = deudasFiltradas;
            gvDeudas.DataBind();
        }
        private void GenerarCuotas(Deuda deuda)
        {
            CuotaDeudaNegocio cuotaNegocio = new CuotaDeudaNegocio();
            for (int i = 1; i <= deuda.Cuotas.Value; i++)
            {
                CuotaDeuda cuota = new CuotaDeuda();
                cuota.Deuda = deuda;
                cuota.NumeroCuota = i;
                cuota.Monto = deuda.MontoTotal / deuda.Cuotas.Value;
                cuota.FechaVencimiento = deuda.FechaInicio.AddMonths(i);
                cuota.FechaPago = null;
                cuota.Estado = EstadoCuota.Pendiente;

                cuotaNegocio.AgregarCuota(cuota);
            }
        }
    }
}