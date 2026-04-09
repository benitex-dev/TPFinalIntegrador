<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GastoIntegranteHogar.aspx.cs" Inherits="TPFinalIntegrador.GastoIntegranteHogar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Gastos por integrante</h2>

      <div class="d-flex align-items-center gap-3 mb-4">
          <asp:Button ID="btnMesAnterior" runat="server" Text="&lt;" CssClass="btn btn-outline-secondary" OnClick="btnMesAnterior_Click" />
          <asp:Label ID="lblMesActual" runat="server" CssClass="fw-bold fs-5 text-capitalize" />
          <asp:Button ID="btnMesSiguiente" runat="server" Text="&gt;" CssClass="btn btn-outline-secondary" OnClick="btnMesSiguiente_Click" />
      </div>

      <asp:Label ID="lblSinHogar" runat="server" Visible="false"
          Text="Esta pantalla solo está disponible en modo Hogar. Seleccioná un hogar desde el menú."
          CssClass="alert alert-warning d-block" />

      <asp:Panel ID="pnlContenido" runat="server">

          <asp:Label ID="lblSinGastos" runat="server" Visible="false"
              Text="No hay gastos registrados para este mes."
              CssClass="text-muted" />

          <div class="row g-4">
              <asp:Repeater ID="rptIntegrantes" runat="server" OnItemDataBound="rptIntegrantes_ItemDataBound">
                  <ItemTemplate>
                      <div class="col-md-6 col-xl-4">
                          <div class="card border-0 shadow-sm rounded-4 h-100">

                              <div class="card-header bg-primary text-white rounded-top-4 fw-bold">
                                  <%# Eval("NombreIntegrante") %>
                              </div>

                              <div class="card-body p-0">
                                  <table class="table table-sm mb-0">
                                      <tbody>
                                          <asp:Repeater ID="rptCategorias" runat="server">
                                              <ItemTemplate>
                                                  <tr>
                                                      <td class="px-3 text-muted"><%# Eval("NombreCategoria") %></td>
                                                      <td class="px-3 text-end fw-semibold">$ <%# ((decimal)Eval("Total")).ToString("N2") %></td>
                                                  </tr>
                                              </ItemTemplate>
                                          </asp:Repeater>
                                      </tbody>
                                  </table>
                              </div>

                              <div class="card-footer bg-light rounded-bottom-4 d-flex justify-content-between">
                                  <span class="fw-bold">Total</span>
                                  <span class="fw-bold text-primary">$ <%# ((decimal)Eval("Total")).ToString("N2") %></span>
                              </div>

                          </div>
                      </div>
                  </ItemTemplate>
              </asp:Repeater>
          </div>

      </asp:Panel>

</asp:Content>
