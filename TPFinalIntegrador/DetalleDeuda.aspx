 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
  CodeBehind="DetalleDeuda.aspx.cs" Inherits="TPFinalIntegrador.DetalleDeuda" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <style>
      .detalle-summary-card {
          background: #fff;
          border-radius: 1.25rem;
          padding: 1.25rem 1.5rem;
          box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.08);
      }
      .detalle-summary-card .icon-wrap {
          width: 44px;
          height: 44px;
          border-radius: 0.75rem;
          display: flex;
          align-items: center;
          justify-content: center;
          flex-shrink: 0;
      }
      .detalle-summary-card .amount {
      font-family: 'Manrope', sans-serif;
      font-weight: 800;
      font-size: 1.2rem;
      letter-spacing: -0.03em;
      line-height: 1.1;
      white-space: nowrap;
  }
      .detalle-summary-card .label {
          font-size: 0.75rem;
          font-weight: 600;
          color: #6c757d;
          text-transform: uppercase;
          letter-spacing: 0.04em;
      }
      .cuotas-card {
          background: #fff;
          border-radius: 1.5rem;
          box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.08);
          overflow: hidden;
      }
      .cuotas-card table {
          margin-bottom: 0;
      }
      .cuotas-card thead th {
          background: #f8f9fa;
          font-size: 0.75rem;
          font-weight: 700;
          text-transform: uppercase;
          letter-spacing: 0.05em;
          color: #6c757d;
          border-bottom: 1px solid #f0f1f2;
          padding: 0.85rem 1.25rem;
      }
      .cuotas-card tbody td {
          padding: 0.9rem 1.25rem;
          border-bottom: 1px solid #f8f9fa;
          vertical-align: middle;
          font-size: 0.9rem;
      }
      .cuotas-card tbody tr:last-child td {
          border-bottom: none;
      }
       .cuotas-card .table {
      border-collapse: separate;
      border-spacing: 0;
  }
      .avatar-deudor {
          width: 52px;
          height: 52px;
          border-radius: 50%;
          display: flex;
          align-items: center;
          justify-content: center;
          font-weight: 700;
          font-size: 1.1rem;
          flex-shrink: 0;
          background: rgba(13,110,253,0.12);
          color: #0057cd;
      }
      .progress-deuda {
          height: 8px;
          border-radius: 50rem;
          background-color: #e9ecef;
          overflow: hidden;
      }
  </style>

  <div class="container py-4 mb-5">
      <div class="row justify-content-center mx-0">
          <div class="col-lg-10 col-xl-8">

              <%-- Header --%>
              <div class="d-flex align-items-center gap-3 mb-4 mt-2">
                  <a href="Deudores.aspx" class="btn btn-light border rounded-pill px-3 d-flex align-items-center
  gap-1">
                      <span class="material-symbols-outlined" style="font-size:18px;">arrow_back</span>
                      Volver
                  </a>
                  <h1 class="h4 fw-bold mb-0" style="font-family:'Manrope',sans-serif;">Detalle de deuda</h1>
              </div>

              <%-- Info del deudor --%>
              <div class="detalle-summary-card mb-4 d-flex align-items-center gap-3">
                  <div class="avatar-deudor">
                      <asp:Label ID="lblIniciales" runat="server" />
                  </div>
                  <div class="flex-grow-1">
                      <div class="fw-bold fs-5" style="font-family:'Manrope',sans-serif;">
                          <asp:Label ID="lblNombre" runat="server" />
                      </div>
                      <div class="text-muted small mt-1">
                          <asp:Label ID="lblDescripcion" runat="server" />
                      </div>
                  </div>
                  <div class="text-end">
                      <asp:Label ID="lblEstadoBadge" runat="server" />
                      <div class="text-muted small mt-1">
                          <asp:Label ID="lblFechaInicio" runat="server" />
                      </div>
                  </div>
              </div>

              <%-- Summary Cards --%>
              <div class="row g-3 mb-4 align-items-stretch">
                  <div class="col-12 col-md-4">
                      <div class="detalle-summary-card d-flex align-items-center gap-3">
                          <div class="icon-wrap" style="background: rgba(13,110,253,0.1);">
                              <span class="material-symbols-outlined text-primary">payments</span>
                          </div>
                          <div>
                              <div class="label">Total prestado</div>
                              <div class="amount"><asp:Label ID="lblMontoTotal" runat="server" /></div>
                          </div>
                      </div>
                  </div>
                  <div class="col-12 col-md-4">
                      <div class="detalle-summary-card d-flex align-items-center gap-3">
                          <div class="icon-wrap" style="background: rgba(25,135,84,0.1);">
                              <span class="material-symbols-outlined text-success">check_circle</span>
                          </div>
                          <div>
                              <div class="label">Cobrado</div>
                              <div class="amount text-success"><asp:Label ID="lblMontoPagado" runat="server" /></div>
                          </div>
                      </div>
                  </div>
                  <div class="col-12 col-md-4">
                      <div class="detalle-summary-card d-flex align-items-center gap-3">
                          <div class="icon-wrap" style="background: rgba(220,53,69,0.1);">
                              <span class="material-symbols-outlined text-danger">pending</span>
                          </div>
                          <div>
                              <div class="label">Pendiente</div>
                              <div class="amount text-danger"><asp:Label ID="lblMontoPendiente" runat="server" /></div>
                          </div>
                      </div>
                  </div>
              </div>

          

              <%-- Tabla de cuotas --%>
              <div class="cuotas-card">
                  <asp:GridView ID="gvCuotas" runat="server" AutoGenerateColumns="False"
                      CssClass="table align-middle mb-0"
                      DataKeyNames="IdCuotaDeuda"
                      OnRowCommand="gvCuotas_RowCommand"
                      EmptyDataText="No hay cuotas registradas.">
                      <Columns>
                          <asp:BoundField DataField="NumeroCuota" HeaderText="N° Cuota" />
                          <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}"
  HtmlEncode="False" />
                          <asp:BoundField DataField="FechaVencimiento" HeaderText="Vencimiento"
  DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                          <asp:BoundField DataField="FechaPago" HeaderText="Fecha de Pago"
  DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" NullDisplayText="—" />
                          <asp:TemplateField HeaderText="Estado">
                              <ItemTemplate>
                                  <%# GetBadgeCuota((dominio.EstadoCuota)Eval("Estado")) %>
                              </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="">
                              <ItemTemplate>
                                  <asp:LinkButton ID="btnPagar" runat="server"
                                      CommandName="Pagar"
                                      CommandArgument='<%# Eval("IdCuotaDeuda") %>'
                                      CssClass="btn btn-sm btn-success rounded-pill px-3"
                                      Visible='<%# (dominio.EstadoCuota)Eval("Estado") == dominio.EstadoCuota.Pendiente
  %>'
                                      OnClientClick="return confirm('¿Confirmas el cobro de esta cuota?');">
                                      <span class="material-symbols-outlined"
  style="font-size:15px;vertical-align:middle;">check</span>
                                      Cobrar
                                  </asp:LinkButton>
                              </ItemTemplate>
                          </asp:TemplateField>
                      </Columns>
                  </asp:GridView>
              </div>

          </div>
      </div>
  </div>
  </asp:Content>