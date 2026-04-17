<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Presupuesto.aspx.cs" Inherits="TPFinalIntegrador.Presupuesto" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div class="d-flex justify-content-between align-items-center mb-4">
          <h4 class="fw-bold mb-0">Presupuesto mensual por categoría</h4>
          <div class="d-flex align-items-center gap-2">
              <asp:Button ID="btnMesAnterior" runat="server" Text="&lt;" CssClass="btn btn-outline-secondary rounded-pill"
  OnClick="btnMesAnterior_Click" />
              <asp:Label ID="lblMesActual" runat="server" CssClass="fw-semibold" />
              <asp:Button ID="btnMesSiguiente" runat="server" Text="&gt;" CssClass="btn btn-outline-secondary rounded-pill"
  OnClick="btnMesSiguiente_Click" />
              <button type="button" class="btn btn-primary rounded-pill ms-2" data-bs-toggle="modal" data-bs-target="#modalConfigurar">
                  <span class="material-symbols-outlined" style="font-size:18px; vertical-align:middle;">settings</span> Configurar
              </button>
          </div>
      </div>

      <div class="row">
          <asp:Repeater ID="rptPresupuesto" runat="server">
              <ItemTemplate>
                  <div class="col-md-6 col-lg-4 mb-3">
                      <div class="card border-0 shadow-sm rounded-4 p-3 h-100">
                          <h6 class="fw-bold mb-2"><%# Eval("Categoria.Nombre") %></h6>
                          <%# GenerarBarra(Eval("MontoPresupuestado"), Eval("GastoReal")) %>
                          <div class="d-flex justify-content-between mt-1">
                              <span class="text-muted" style="font-size:11px;">$ <%# Eval("GastoReal", "{0:N2}") %> gastado</span>
                              <span class="text-muted" style="font-size:11px;">$ <%# Eval("MontoPresupuestado", "{0:N2}") %> presupuestado</span>
                          </div>
                      </div>
                  </div>
              </ItemTemplate>
          </asp:Repeater>
      </div>

      <asp:Panel ID="pnlSinPresupuestos" runat="server" Visible="false">
          <div class="text-center text-muted py-5">
              <span class="material-symbols-outlined" style="font-size:48px;">account_balance_wallet</span>
              <p class="mt-2">No tenés presupuestos configurados para este mes.</p>
              <button type="button" class="btn btn-primary rounded-pill" data-bs-toggle="modal" data-bs-target="#modalConfigurar">
                  Configurar presupuestos
              </button>
          </div>
      </asp:Panel>

      <%-- Modal Configurar --%>
      <div class="modal fade" id="modalConfigurar" tabindex="-1">
          <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
              <div class="modal-content rounded-4 border-0">
                  <div class="modal-header border-0 pb-0">
                      <h5 class="modal-title fw-bold">Configurar presupuestos</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                  </div>
                  <div class="modal-body">
                      <asp:Repeater ID="rptConfigurar" runat="server" OnItemCommand="rptConfigurar_ItemCommand">
                          <ItemTemplate>
                              <div class="d-flex align-items-center gap-2 mb-2">
                                  <span class="flex-grow-1 fw-semibold small"><%# Eval("Categoria.Nombre") %></span>
                                  <asp:TextBox ID="txtPresupuesto" runat="server" CssClass="form-control form-control-sm rounded-3"
                                      Text='<%# ((decimal)Eval("MontoPresupuestado")) > 0 ? Eval("MontoPresupuestado") : "" %>'
                                      placeholder="Monto" style="max-width:120px" />
                                  <asp:HiddenField ID="hfIdCategoria" runat="server" Value='<%# Eval("Categoria.IdCategoria") %>' />
                                  <asp:LinkButton runat="server" CommandName="Guardar" CommandArgument='<%# Container.ItemIndex %>'
                                      CssClass="btn btn-sm btn-primary rounded-pill">Guardar</asp:LinkButton>
                              </div>
                          </ItemTemplate>
                      </asp:Repeater>
                  </div>
              </div>
          </div>
      </div>

      <asp:Label ID="lblMensaje" runat="server" />

  </asp:Content>
