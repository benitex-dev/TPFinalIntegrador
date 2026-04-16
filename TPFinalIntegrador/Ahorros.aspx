<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ahorros.aspx.cs" Inherits="TPFinalIntegrador.Ahorros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2>Mis Metas de Ahorro</h2>--%>

  <%-- Formulario para agregar nueva meta --%>
  <%--<div class="card mb-4">
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
  </div>--%>

  <%-- Grilla de metas --%>
 <%-- <asp:GridView ID="gvMetas" runat="server" AutoGenerateColumns="False"
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
  </asp:GridView>--%>
    
    <asp:HiddenField ID="hfIdMetaAporte" runat="server" />
    <asp:HiddenField ID="hfIdMetaEditar" runat="server" />

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h4 class="fw-bold mb-0">Metas de Ahorro</h4>
        <button 
            type="button" 
            class="btn btn-primary rounded-pill" 
            data-bs-toggle="modal"
            data-bs-target="#modalNuevaMeta">
            <span class="material-symbols-outlined" style="font-size: 18px; vertical-align: middle;">add</span> 
            Nueva meta
        </button>
    </div>
<%--    REAPEATER PARA MOSTRAR LAS METAS DE AHORRO--%>
    <div class="row">
          <asp:Repeater 
      ID="rptMetas" 
      runat="server" 
      OnItemCommand="rptMetas_ItemCommand">
              
                           <ItemTemplate>
<div class="col-md-6 col-lg-4 mb-3">
              <div class="card border-0 shadow-sm rounded-4 p-4 mb-3">
              <div class="d-flex justify-content-between align-items-start mb-3">
                  <div>
                      <h5 class="fw-bold mb-1"><%# Eval("Nombre") %></h5>
                      <span class="text-muted small">
                          Fecha objetivo: <%# Eval("FechaObjetivo") != null ? ((DateTime)Eval("FechaObjetivo")).ToString("dd/MM/yyyy") : "Sin fecha" %>
                      </span>
                  </div>
                  <div class="d-flex gap-2">
                      <asp:LinkButton 
                          runat="server"
                          CommandName="Editar" 
                          CommandArgument='<%# Eval("IdMeta") %>'
                          CssClass="btn btn-sm btn-outline-secondary rounded-pill" 
                          title="Editar">
                            <span 
                                class="material-symbols-outlined" 
                                style="font-size:16px; 
                                vertical-align:middle;">
                                edit

                            </span>
                      </asp:LinkButton>
                      <asp:LinkButton 
                          runat="server"
                          CommandName="Eliminar" 
                          CommandArgument='<%# Eval("IdMeta") %>'
                          CssClass="btn btn-sm btn-outline-danger rounded-pill"
                          title="Eliminar"
                          OnClientClick="return confirm('¿Eliminar esta meta?')"
                          >
                            <span 
                                class="material-symbols-outlined" 
                                style="font-size:16px;
                                vertical-align:middle;"
                                >delete

                            </span>
                      </asp:LinkButton>
                  </div>
              </div>

              <div class="progress rounded-pill mb-2" style="height: 10px;">
                  <div 
                      class="progress-bar bg-primary"
                      role="progressbar"
                      style="width: <%# Math.Min((int)Eval("Porcentaje"), 100) %>%"
                      >
                  </div>
              </div>

              <div class="d-flex justify-content-between mb-3">
                  <span class="text-muted small">
                      $ <%# Eval("MontoActual", "{0:N2}") %> ahorrado</span>
                  <span class="fw-semibold small">
                      <%# Eval("Porcentaje") %>% — Meta: $ <%# Eval("MontoObjetivo", "{0:N2}") %>

                  </span>
              </div>

              <div class="text-start">
    <button
        type="button"
        class="btn btn-sm btn-outline-primary rounded-pill"
        onclick="abrirModalAporte('<%# Eval("IdMeta") %>')">
        + Registrar aporte
    </button>
</div>
          </div>

</div>
      </ItemTemplate>
                  
      
  </asp:Repeater>
    </div>
    
   <%-- SIN METAS DE AHORRO ACTIVAS--%>
    <asp:Panel 
        ID="pnlSinMetas" 
        runat="server"
        Visible="false">
        <div class="text-center text-muted py-5">
            <span class="material-symbols-outlined" style="font-size: 48px;">savings</span>
            <p class="mt-2">No tenés metas de ahorro activas.</p>
        </div>
    </asp:Panel>

    <%-- MODAL NUEVA META --%>
    <div 
        class="modal fade" 
        id="modalNuevaMeta" 
        tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-4 border-0">
                <div class="modal-header border-0 pb-0">
                    <h5 class="modal-title fw-bold">Nueva Meta de Ahorro</h5>
                    <button 
                        type="button" 
                        class="btn-close" 
                        data-bs-dismiss="modal">
                    </button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox 
                            ID="txtNombre" 
                            runat="server" 
                            CssClass="form-control rounded-3"
                            placeholder="Ej: Vacaciones" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Monto Objetivo ($)</label>
                        <asp:TextBox 
                            ID="txtMonto" 
                            runat="server"
                            CssClass="form-control rounded-3" 
                            TextMode="Number"
                            placeholder="0" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Fecha Objetivo</label>
                        <asp:TextBox 
                            ID="txtFecha" 
                            runat="server"
                            CssClass="form-control rounded-3" 
                            TextMode="Date" />
                    </div>
                </div>
                <div class="modal-footer border-0 pt-0">
                    <button type="button" class="btn btn-outline-secondary rounded-pill"
                        data-bs-dismiss="modal">
                        Cancelar</button>
                    <asp:Button 
                        ID="btnAgregar" 
                        runat="server" 
                        Text="Guardar" 
                        CssClass="btn btn-primary rounded-pill"
                        OnClick="btnAgregar_Click" />
                </div>
            </div>
        </div>
    </div>

    <%-- Modal Registrar Aporte --%>
    <div class="modal fade" id="modalAporte" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-4 border-0">
                <div class="modal-header border-0 pb-0">
                    <h5 class="modal-title fw-bold">Registrar Aporte</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">Monto ($)</label>
                        <asp:TextBox ID="txtMontoAporte" runat="server" CssClass="form-control rounded-3"
                            TextMode="Number" placeholder="0" />
                    </div>
                </div>
                <div class="modal-footer border-0 pt-0">
                    <button type="button" class="btn btn-outline-secondary rounded-pill"
                        data-bs-dismiss="modal">
                        Cancelar</button>
                    <asp:Button 
                        ID="btnConfirmarAporte" 
                        runat="server" 
                        Text="Confirmar"
                        CssClass="btn btn-primary rounded-pill"
                        OnClick="btnConfirmarAporte_Click" />
                </div>
            </div>
        </div>
    </div>

    <%-- Modal Editar Meta --%>
    <div class="modal fade" id="modalEditar" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-4 border-0">
                <div class="modal-header border-0 pb-0">
                    <h5 class="modal-title fw-bold">Editar Meta</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox 
                            ID="txtNombreEditar" 
                            runat="server" 
                            CssClass="form-control rounded-3" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Monto Objetivo ($)</label>
                        <asp:TextBox 
                            ID="txtMontoEditar" 
                            runat="server" 
                            CssClass="form-control rounded-3"
                            TextMode="Number" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Fecha Objetivo</label>
                        <asp:TextBox
                            ID="txtFechaEditar" 
                            runat="server" 
                            CssClass="form-control rounded-3"
                            TextMode="Date" />
                    </div>
                </div>
                <div class="modal-footer border-0 pt-0">
                    <button 
                        type="button"
                        class="btn btn-outline-secondary rounded-pill"
                        data-bs-dismiss="modal">
                        Cancelar
                    </button>
                    <asp:Button 
                        ID="btnGuardarEdicion" 
                        runat="server"
                        Text="Guardar" 
                        CssClass="btn btn-primary rounded-pill"
                        OnClick="btnGuardarEdicion_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function abrirModalAporte(idMeta) {
            document.getElementById('<%= hfIdMetaAporte.ClientID %>').value = idMeta;
            new bootstrap.Modal(document.getElementById('modalAporte')).show();
        }
    </script>
</asp:Content>
