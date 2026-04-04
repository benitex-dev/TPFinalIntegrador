<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Deudores.aspx.cs" Inherits="TPFinalIntegrador.Deudores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Deudores</h2>
    <asp:Button ID="btnNuevaDeuda" runat="server" Visible="true" Text="+ Nueva Deuda" 
    CssClass="btn btn-primary mb-3" OnClick="btnNuevaDeuda_Click" />

<asp:Panel ID="pnlFormulario" runat="server" Visible="false">
  
        
        
            <%-- Formulario para agregar deuda --%>
<div class="card mb-4">
    <div class="card-header">
        <h5>Nueva Deuda</h5>
    </div>
    <div class="card-body">
        <div class="row g-3">
            <div class="col-md-3">
                <label>Nombre del Deudor</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Juan Pérez" />
            </div>
            <div class="col-md-3">
                <label>Email del Deudor</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="email@ejemplo.com" />
            </div>
            <div class="col-md-3">
                <label>Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Motivo de la deuda" />
            </div>
            <div class="col-md-3">
                <label>Monto Total ($)</label>
                <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" TextMode="Number" placeholder="0" />
            </div>
            <div class="col-md-3">
                <label>Cantidad de Cuotas</label>
                <asp:TextBox ID="txtCuotas" runat="server" CssClass="form-control" TextMode="Number" placeholder="1" />
            </div>
            <div class="col-md-3">
                <label>Fecha de Inicio</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3 d-flex align-items-end">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click1" />
            </div>
        </div>
    </div>
</div>
        
    
</asp:Panel>
  
    
     <!-- Filtros -->
  <div class="card mb-4">
      <div class="card-header">
          <h5 class="mb-0">Filtros</h5>
      </div>
      <div class="card-body">
          <div class="mb-3">
              <label for="ddlFiltroEstadoDeuda" class="form-label">Estado Deuda</label>
              <asp:DropDownList ID="ddlFiltroEstadoDeuda" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstadoDeuda_SelectedIndexChanged"></asp:DropDownList>
          </div>

          

<%--          <% if (negocio.Security.isAdmin(Session["usuario"]))
              { %>
          <asp:Button Text="Ver productos dados de baja" runat="server" CssClass="btn btn-outline-info mt-2" ID="btnProdBaja" OnClick="btnProdBaja_Click" />
          <% } %>--%>
      </div>
  </div>
  <%-- Grilla de deudores --%>
  <asp:GridView ID="gvDeudas" runat="server" AutoGenerateColumns="False"
      CssClass="table table-bordered table-hover"
      DataKeyNames="IdDeuda,NombreDeudor"
      OnRowEditing="gvDeudas_RowEditing"
      OnRowUpdating="gvDeudas_RowUpdating"
      OnRowCancelingEdit="gvDeudas_RowCancelingEdit"
      OnRowDeleting="gvDeudas_RowDeleting"
      EmptyDataText="No tenés deudores registrados."
      AllowPaging="True"
      PageSize="5"
      OnPageIndexChanging ="gvDeudas_PageIndexChanging">
      <Columns>
            <asp:TemplateField HeaderText="Nombre">
      <ItemTemplate>
          <%# Eval("NombreDeudor") %>
      </ItemTemplate>
      <EditItemTemplate>
          <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
              Text='<%# Eval("NombreDeudor") %>' />
      </EditItemTemplate>
  </asp:TemplateField>

  <asp:TemplateField HeaderText="Email">
      <ItemTemplate>
          <%# Eval("EmailDeudor") %>
      </ItemTemplate>
      <EditItemTemplate>
          <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
              Text='<%# Eval("EmailDeudor") %>' />
      </EditItemTemplate>
  </asp:TemplateField>

  <asp:TemplateField HeaderText="Descripción">
      <ItemTemplate>
          <%# Eval("Descripcion") %>
      </ItemTemplate>
      <EditItemTemplate>
          <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"
              Text='<%# Eval("Descripcion") %>' />
      </EditItemTemplate>
  </asp:TemplateField>
          <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total" DataFormatString="{0:C}" HtmlEncode="False"  ReadOnly="True"/>
          <asp:BoundField DataField="Cuotas" HeaderText="Cuotas" ReadOnly="True" />
          <asp:BoundField DataField="FechaInicio" ReadOnly="True" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
          <asp:BoundField DataField="Estado" ReadOnly="True" HeaderText="Estado" />
           <asp:TemplateField>
      <ItemTemplate>
          <asp:HyperLink ID="lnkDetalle" runat="server"
              NavigateUrl='<%# "DetalleDeuda.aspx?id=" + Eval("IdDeuda") %>'
              CssClass="btn btn-sm btn-info"
              ToolTip="Ver detalle">
              <i class="bi bi-eye"></i>
          </asp:HyperLink>
      </ItemTemplate>
                <EditItemTemplate></EditItemTemplate>
  </asp:TemplateField>
          <asp:TemplateField>
              <ItemTemplate>
                  <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit"
                      CssClass="btn btn-sm btn-warning" ToolTip="Editar">
            <i class="bi bi-pencil"></i>
        </asp:LinkButton>
              </ItemTemplate>
               <EditItemTemplate>
          <asp:LinkButton ID="btnGuardar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success" Text="Guardar" />
          <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary" Text="Cancelar" />
      </EditItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField>
              <ItemTemplate>
                  <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Delete"
                      CssClass="btn btn-sm btn-danger" ToolTip="Eliminar"
                      OnClientClick="return confirm('¿Estás seguro que querés eliminar esta deuda?');">
            <i class="bi bi-trash"></i>
        </asp:LinkButton>
              </ItemTemplate>
                <EditItemTemplate></EditItemTemplate>
          </asp:TemplateField>
      </Columns>
  </asp:GridView>
</asp:Content>
