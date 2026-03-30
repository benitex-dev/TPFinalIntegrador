<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ahorros.aspx.cs" Inherits="TPFinalIntegrador.Ahorros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Mis Metas de Ahorro</h2>

  <%-- Formulario para agregar nueva meta --%>
  <div class="card mb-4">
      <div class="card-header">
          <h5>Nueva Meta de Ahorro</h5>
      </div>
      <div class="card-body">
          <div class="row">
              <div class="col-md-4">
                  <label>Nombre</label>
                  <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Vacaciones" />
              </div>
              <div class="col-md-3">
                  <label>Monto Objetivo ($)</label>
                  <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" TextMode="Number" placeholder="0" />
              </div>
              <div class="col-md-3">
                  <label>Fecha Objetivo</label>
                  <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date" />
              </div>
              <div class="col-md-2 d-flex align-items-end">
                  <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click" />
              </div>
          </div>
      </div>
  </div>

  <%-- Grilla de metas --%>
  <asp:GridView ID="gvMetas" runat="server" AutoGenerateColumns="False"
      CssClass="table table-bordered table-hover"
      DataKeyNames="IdMeta"
      OnRowEditing="gvMetas_RowEditing"
      OnRowUpdating="gvMetas_RowUpdating"
      OnRowCancelingEdit="gvMetas_RowCancelingEdit"
      OnRowDeleting="gvMetas_RowDeleting"
      EmptyDataText="No tenés metas de ahorro registradas.">
      <Columns>
          <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
          <asp:BoundField DataField="MontoObjetivo" HeaderText="Monto Objetivo" DataFormatString="{0:C}" HtmlEncode="False" />
          <asp:BoundField DataField="FechaObjetivo" HeaderText="Fecha Objetivo" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
          <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
      </Columns>
  </asp:GridView>
</asp:Content>
