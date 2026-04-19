<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Deudores.aspx.cs" Inherits="TPFinalIntegrador.Deudores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div class="d-flex justify-content-between align-items-center mb-4">
          <h4 class="fw-bold mb-0">Dinero Prestado</h4>
          <div class="d-flex align-items-center gap-2">
              <asp:DropDownList ID="ddlFiltroEstadoDeuda" runat="server" CssClass="form-select form-select-sm rounded-pill"
                  AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstadoDeuda_SelectedIndexChanged" style="max-width:160px" />
              <button type="button" class="btn btn-primary rounded-pill" data-bs-toggle="modal" data-bs-target="#modalNuevaDeuda">
                  <span class="material-symbols-outlined" style="font-size:18px; vertical-align:middle;">add</span> Nueva deuda
              </button>
          </div>
      </div>

      <div class="card border-0 shadow-sm rounded-4">
          <div class="card-body p-0">
              <div class="table-responsive">
                  <asp:GridView ID="gvDeudas" runat="server" AutoGenerateColumns="False"
                      CssClass="table table-hover align-middle mb-0"
                      DataKeyNames="IdDeuda,NombreDeudor"
                      OnRowEditing="gvDeudas_RowEditing"
                      OnRowUpdating="gvDeudas_RowUpdating"
                      OnRowCancelingEdit="gvDeudas_RowCancelingEdit"
                      OnRowDeleting="gvDeudas_RowDeleting"
                      EmptyDataText="No tenés deudores registrados."
                      AllowPaging="True"
                      PageSize="5"
                      OnPageIndexChanging="gvDeudas_PageIndexChanging">
                      <HeaderStyle CssClass="table-light" />
                      <Columns>
                          <asp:TemplateField HeaderText="Nombre">
                              <ItemTemplate><%# Eval("NombreDeudor") %></ItemTemplate>
                              <EditItemTemplate>
                                  <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" Text='<%# Eval("NombreDeudor")
  %>' />
                              </EditItemTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Email">
                              <ItemTemplate><%# Eval("EmailDeudor") %></ItemTemplate>
                              <EditItemTemplate>
                                  <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm" Text='<%# Eval("EmailDeudor")
  %>' />
                              </EditItemTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Descripción">
                              <ItemTemplate><%# Eval("Descripcion") %></ItemTemplate>
                              <EditItemTemplate>
                                  <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control form-control-sm" Text='<%#
  Eval("Descripcion") %>' />
                              </EditItemTemplate>
                          </asp:TemplateField>

                          <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total" DataFormatString="{0:C}" HtmlEncode="False"
  ReadOnly="True" />
                          <asp:BoundField DataField="MontoPendiente" HeaderText="Saldo Pendiente" DataFormatString="{0:C}" HtmlEncode="False"
  ReadOnly="True" />
                          <asp:BoundField DataField="Cuotas" HeaderText="Cuotas" ReadOnly="True" />
                          <asp:BoundField DataField="FechaInicio" ReadOnly="True" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}"
  HtmlEncode="False" />

                          <asp:TemplateField HeaderText="Estado">
                              <ItemTemplate>
                                  <%# GetBadgeEstado((int)Eval("Estado")) %>
                              </ItemTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:HyperLink ID="lnkDetalle" runat="server"
                                      NavigateUrl='<%# "DetalleDeuda.aspx?id=" + Eval("IdDeuda") %>'
                                      CssClass="btn btn-sm btn-outline-secondary rounded-pill" ToolTip="Ver detalle">
                                      <span class="material-symbols-outlined" style="font-size:16px; vertical-align:middle;">visibility</span>
                                  </asp:HyperLink>
                              </ItemTemplate>
                              <EditItemTemplate></EditItemTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit"
                                      CssClass="btn btn-sm btn-outline-warning rounded-pill" ToolTip="Editar">
                                      <span class="material-symbols-outlined" style="font-size:16px; vertical-align:middle;">edit</span>
                                  </asp:LinkButton>
                              </ItemTemplate>
                              <EditItemTemplate>
                                  <asp:LinkButton ID="btnGuardar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success rounded-pill"
   Text="Guardar" />
                                  <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary
  rounded-pill" Text="Cancelar" />
                              </EditItemTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Delete"
                                      CssClass="btn btn-sm btn-outline-danger rounded-pill" ToolTip="Eliminar"
                                      OnClientClick="return confirm('¿Estás seguro que querés eliminar esta deuda?');">
                                      <span class="material-symbols-outlined" style="font-size:16px; vertical-align:middle;">delete</span>
                                  </asp:LinkButton>
                              </ItemTemplate>
                              <EditItemTemplate></EditItemTemplate>
                          </asp:TemplateField>
                      </Columns>
                  </asp:GridView>
              </div>
          </div>
      </div>

      <%-- Modal Nueva Deuda --%>
      <div class="modal fade" id="modalNuevaDeuda" tabindex="-1">
          <div class="modal-dialog modal-dialog-centered modal-lg">
              <div class="modal-content rounded-4 border-0">
                  <div class="modal-header border-0 pb-0">
                      <h5 class="modal-title fw-bold">Nueva Deuda</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                  </div>
                  <div class="modal-body">
                      <div class="row g-3">
                          <div class="col-md-6">
                              <label class="form-label">Nombre del Deudor</label>
                              <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control rounded-3" placeholder="Ej: Juan Pérez" />
                          </div>
                          <div class="col-md-6">
                              <label class="form-label">Email del Deudor</label>
                              <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control rounded-3" placeholder="email@ejemplo.com" />
                          </div>
                          <div class="col-md-12">
                              <label class="form-label">Descripción</label>
                              <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control rounded-3" placeholder="Motivo de la deuda" />
                          </div>
                          <div class="col-md-4">
                              <label class="form-label">Monto Total ($)</label>
  <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control rounded-3" TextMode="SingleLine" placeholder="0.00"
  />                          </div>
                          <div class="col-md-4">
                              <label class="form-label">Cantidad de Cuotas</label>
                              <asp:TextBox ID="txtCuotas" runat="server" CssClass="form-control rounded-3" TextMode="Number" placeholder="1" />
                          </div>
                          <div class="col-md-4">
                              <label class="form-label">Fecha de Inicio</label>
                              <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control rounded-3" TextMode="Date" />
                          </div>
                      </div>
                  </div>
                  <div class="modal-footer border-0 pt-0">
                      <button type="button" class="btn btn-outline-secondary rounded-pill" data-bs-dismiss="modal">Cancelar</button>
                      <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="btn btn-primary rounded-pill" OnClick="btnAgregar_Click1"
  />
                  </div>
              </div>
          </div>
      </div>

  </asp:Content>
