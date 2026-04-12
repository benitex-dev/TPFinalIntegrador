<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleDeuda.aspx.cs" Inherits="TPFinalIntegrador.DetalleDeuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <h2>Detalle de Deuda</h2>

  <%-- Información de la deuda --%>
  <div class="card mb-4">
      <div class="card-body">
          <div class="row">
              <div class="col-md-3">
                  <label class="text-muted">Deudor</label>
                  <p class="fw-bold"><asp:Label ID="lblNombre" runat="server" /></p>
              </div>
              <div class="col-md-3">
                  <label class="text-muted">Descripción</label>
                  <p class="fw-bold"><asp:Label ID="lblDescripcion" runat="server" /></p>
              </div>
              <div class="col-md-2">
                  <label class="text-muted">Monto Total</label>
                  <p class="fw-bold"><asp:Label ID="lblMontoTotal" runat="server" /></p>
              </div>
              <div class="col-md-2">
                  <label class="text-muted">Cuotas</label>
                  <p class="fw-bold"><asp:Label ID="lblCuotas" runat="server" /></p>
              </div>
              <div class="col-md-2">
                  <label class="text-muted">Monto por Cuota</label>
                  <p class="fw-bold"><asp:Label ID="lblMontoCuota" runat="server" /></p>
              </div>
          </div>
      </div>
  </div>

  <%-- Grilla de cuotas --%>
  <asp:GridView ID="gvCuotas" runat="server" AutoGenerateColumns="False"
      CssClass="table table-bordered table-hover"
      DataKeyNames="IdCuotaDeuda"
       OnRowCommand="gvCuotas_RowCommand"
      EmptyDataText="No hay cuotas registradas.">
      <Columns>
          <asp:BoundField DataField="NumeroCuota" HeaderText="N° Cuota" />
          <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" HtmlEncode="False" />
          <asp:BoundField DataField="FechaVencimiento" HeaderText="Vencimiento" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
          <asp:BoundField DataField="FechaPago" HeaderText="Fecha de Pago" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False"
  NullDisplayText="Pendiente" />
          <asp:TemplateField HeaderText="Estado">
              <ItemTemplate>
                  <%# ((dominio.EstadoCuota)Eval("Estado")).ToString() %>
              </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Acción">
              <ItemTemplate>
                  <asp:LinkButton ID="btnPagar" runat="server"
                      CommandName="Pagar"
                      CommandArgument='<%# Eval("IdCuotaDeuda") %>'
                      CssClass="btn btn-sm btn-success"
                      Visible='<%# (dominio.EstadoCuota)Eval("Estado") == dominio.EstadoCuota.Pendiente %>'
                      OnClientClick="return confirm('¿Confirmas el pago de esta cuota?');">
                      <i class="bi bi-check-circle"></i> Marcar Pagada
                  </asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>
      </Columns>
  </asp:GridView>

  <a href="Deudores.aspx" class="btn btn-secondary mt-2">Volver</a>
</asp:Content>
