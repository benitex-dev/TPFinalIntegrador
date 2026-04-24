<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Deudores.aspx.cs"
  Inherits="TPFinalIntegrador.Deudores" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <style>
      .summary-card {
          background: #fff; border-radius: 1.25rem; padding: 1.5rem;
          box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.08);
      }
      .summary-card .icon-wrap {
          width: 48px; height: 48px; border-radius: 0.875rem;
          display: flex; align-items: center; justify-content: center; flex-shrink: 0;
      }
      .summary-card .amount {
          font-family: 'Manrope', sans-serif; font-weight: 800;
          font-size: 1.5rem; letter-spacing: -0.03em; line-height: 1.1; white-space: nowrap;
      }
      .summary-card .label {
          font-size: 0.8rem; font-weight: 600; color: #6c757d;
          text-transform: uppercase; letter-spacing: 0.04em;
      }
      .deuda-card {
          background: #fff; border-radius: 1.5rem;
          box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.08); overflow: hidden;
      }
      .deuda-item {
          padding: 1.1rem 1.5rem; border-bottom: 1px solid #f0f1f2; transition: background 0.15s;
      }
      .deuda-item:last-child { border-bottom: none; }
      .deuda-item:hover { background: #f8f9fa; }
      .avatar-inicial {
          width: 44px; height: 44px; border-radius: 50%;
          display: flex; align-items: center; justify-content: center;
          font-weight: 700; font-size: 1rem; flex-shrink: 0;
      }
      .badge-estado {
          font-size: 0.72rem; font-weight: 600; padding: 0.3em 0.75em; border-radius: 50rem;
      }
      .progress-deuda {
          height: 5px; border-radius: 50rem; background-color: #e9ecef; overflow: hidden; margin-top: 6px;
      }
      .progress-deuda .progress-bar { border-radius: 50rem; height: 100%; }
      .btn-kebab {
          width: 32px; height: 32px; border-radius: 50%; color: #adb5bd;
          transition: background-color 0.2s, color 0.2s;
          display: inline-flex; align-items: center; justify-content: center;
      }
      .btn-kebab:hover { background-color: rgba(0,0,0,0.08); color: #495057; }
      .filter-card {
          background: #fff; border-radius: 1.5rem; padding: 0.8rem 1.2rem;
          margin-bottom: 1rem; box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.08);
      }
      .btn-filter {
          background: #fff; border: 1px solid rgba(114,119,135,0.2);
          border-radius: 50rem; padding: 0.5rem 1rem;
          font-size: 0.875rem; font-weight: 500; color: #191c1d; transition: all 0.2s;
      }
      .btn-filter:hover { background-color: #f0f1f2; }
      .cuotas-tag { font-size: 0.72rem; color: #6c757d; font-weight: 500; }
  </style>

  <div class="container py-4 mb-5">
      <div class="row justify-content-center mx-0">
          <div class="col-lg-10 col-xl-8">

              <!-- Header -->
              <div class="d-flex align-items-center justify-content-between mb-4 mt-2">
                  <h1 class="display-6 mb-0" style="font-family:'Manrope',sans-serif;font-weight:800;">Dinero
  Prestado</h1>
                  <button type="button" class="btn btn-primary rounded-pill px-3 d-flex align-items-center gap-2"
                      data-bs-toggle="modal" data-bs-target="#modalNuevaDeuda">
                      <span class="material-symbols-outlined" style="font-size:20px;">add</span>
                      Nueva deuda
                  </button>
              </div>

              <!-- Summary Cards -->
              <div class="row g-3 mb-4 align-items-stretch">
                  <div class="col-12 col-md-4">
                      <div class="summary-card d-flex align-items-center gap-3 h-100">
                          <div class="icon-wrap" style="background:rgba(13,110,253,0.1);">
                              <span class="material-symbols-outlined text-primary">payments</span>
                          </div>
                          <div>
                              <div class="label">Total prestado</div>
                              <div class="amount"><asp:Label ID="lblTotalPrestado" runat="server" Text="$0" /></div>
                          </div>
                      </div>
                  </div>
                  <div class="col-12 col-md-4">
                      <div class="summary-card d-flex align-items-center gap-3 h-100">
                          <div class="icon-wrap" style="background:rgba(220,53,69,0.1);">
                              <span class="material-symbols-outlined text-danger">pending</span>
                          </div>
                          <div>
                              <div class="label">Pendiente</div>
                              <div class="amount text-danger"><asp:Label ID="lblTotalPendiente" runat="server" Text="$0"
   /></div>
                          </div>
                      </div>
                  </div>
                  <div class="col-12 col-md-4">
                      <div class="summary-card d-flex align-items-center gap-3 h-100">
                          <div class="icon-wrap" style="background:rgba(25,135,84,0.1);">
                              <span class="material-symbols-outlined text-success">group</span>
                          </div>
                          <div>
                              <div class="label">Deudores</div>
                              <div class="amount"><asp:Label ID="lblTotalDeudores" runat="server" Text="0" /></div>
                          </div>
                      </div>
                  </div>
              </div>

              <!-- Filter Bar -->
              <div class="filter-card">
                  <div class="d-flex flex-wrap align-items-center gap-2">
                      <div class="dropdown">
                          <button class="btn-filter d-inline-flex align-items-center gap-2" data-bs-toggle="dropdown"
  type="button">
                              <asp:Label ID="lblFiltroEstado" runat="server" Text="Estado" />
                              <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
                          </button>
                          <ul class="dropdown-menu shadow-sm border-0 rounded-3">
                              <li><asp:LinkButton ID="btnFiltroTodos" runat="server" CssClass="dropdown-item small"
  OnCommand="FiltroEstado_Command" CommandArgument="-1">Todos</asp:LinkButton></li>
                              <li><asp:LinkButton ID="btnFiltroPendiente" runat="server" CssClass="dropdown-item small"
  OnCommand="FiltroEstado_Command" CommandArgument="1">Pendiente</asp:LinkButton></li>
                              <li><asp:LinkButton ID="btnFiltroCobrado" runat="server" CssClass="dropdown-item small"
  OnCommand="FiltroEstado_Command" CommandArgument="0">Cobrado</asp:LinkButton></li>
                          </ul>
                      </div>
                      <asp:LinkButton ID="btnBorrarFiltros" runat="server" CssClass="btn btn-link text-primary small
  fw-bold text-decoration-none ms-auto border-0 p-0" OnClick="btnBorrarFiltros_Click">
                          <small>Borrar filtros</small>
                      </asp:LinkButton>
                  </div>
              </div>

              <!-- Sin deudas -->
              <asp:Panel ID="pnlSinDeudas" runat="server" Visible="false" CssClass="deuda-card text-center py-5
  text-muted">
                  <span class="material-symbols-outlined d-block mb-2"
  style="font-size:40px;color:#dee2e6;">payments</span>
                  <p class="mb-0">No tenés deudores registrados.</p>
              </asp:Panel>

              <!-- Lista -->
              <asp:Panel ID="pnlDeudas" runat="server">
                  <div class="deuda-card">
                      <asp:Repeater ID="rptDeudas" runat="server" OnItemCommand="rptDeudas_ItemCommand">
                          <ItemTemplate>
                              <div class="deuda-item d-flex align-items-center gap-3">
                                  <div class="avatar-inicial" style='<%# GetAvatarStyle(Eval("NombreDeudor").ToString())
   %>'>
                                      <%# GetIniciales(Eval("NombreDeudor").ToString()) %>
                                  </div>
                                  <div class="flex-grow-1" style="min-width:0;">
                                      <div class="d-flex align-items-center justify-content-between">
                                          <div>
                                              <span class="fw-bold"><%# Eval("NombreDeudor") %></span>
                                              <%# GetBadgeEstado((int)Eval("Estado")) %>
                                          </div>
                                          <span class="fw-bold ms-3 <%# (int)Eval("Estado") == 0 ? "text-success" :
  "text-danger" %>"
                                                style="white-space:nowrap;font-family:'Manrope',sans-serif;">
                                              <%# FormatMonto((decimal)Eval("MontoTotal"), (int)Eval("Estado")) %>
                                          </span>
                                      </div>
                                      <div class="text-muted small mt-1"><%# Eval("Descripcion") %> · <%#
  GetCuotasTexto(Eval("Cuotas")) %></div>
                                      <div class="progress-deuda mt-2" style="max-width:200px;">
                                          <div class="progress-bar <%# GetColorBarra((int)Eval("Estado")) %>"
                                               style='<%# GetAnchoBarra((decimal)Eval("MontoTotal"),
  (decimal)Eval("MontoPendiente")) %>'></div>
                                      </div>
                                      <div class="cuotas-tag mt-1"><%# GetProgresoTexto((decimal)Eval("MontoTotal"),
  (decimal)Eval("MontoPendiente")) %></div>
                                  </div>
                                  <div class="dropdown ms-2">
                                      <button class="btn btn-link p-0 btn-kebab" data-bs-toggle="dropdown"
  type="button">
                                          <span class="material-symbols-outlined"
  style="font-size:20px;">more_vert</span>
                                      </button>
                                      <ul class="dropdown-menu dropdown-menu-end shadow-sm border-0 rounded-3">
                                          <li>
                                              <a class="dropdown-item small" href='<%# "DetalleDeuda.aspx?id=" +
  Eval("IdDeuda") %>'>
                                                  <span class="material-symbols-outlined"
  style="font-size:15px;vertical-align:middle;">visibility</span> Ver detalle
                                              </a>
                                          </li>
                                          <li>
                                              <asp:LinkButton runat="server" CssClass="dropdown-item small"
                                                  CommandName="Editar" CommandArgument='<%# Eval("IdDeuda") %>'>
                                                  <span class="material-symbols-outlined"
  style="font-size:15px;vertical-align:middle;">edit</span> Editar
                                              </asp:LinkButton>
                                          </li>
                                          <li>
                                              <asp:LinkButton runat="server" CssClass="dropdown-item small text-danger"
                                                  CommandName="Eliminar" CommandArgument='<%# Eval("IdDeuda") %>'
                                                  OnClientClick="return confirm('¿Estás seguro que querés eliminar esta
  deuda?');">
                                                  <span class="material-symbols-outlined"
  style="font-size:15px;vertical-align:middle;">delete</span> Eliminar
                                              </asp:LinkButton>
                                          </li>
                                      </ul>
                                  </div>
                              </div>
                          </ItemTemplate>
                      </asp:Repeater>
                  </div>
              </asp:Panel>

          </div>
      </div>
  </div>

  <!-- Modal Nueva Deuda -->
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
                          <label class="form-label fw-semibold">Nombre del Deudor</label>
                          <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control rounded-3" placeholder="Ej:
  Juan Pérez" />
                      </div>
                      <div class="col-md-6">
                          <label class="form-label fw-semibold">Email del Deudor</label>
                          <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control rounded-3"
  placeholder="email@ejemplo.com" />
                      </div>
                      <div class="col-md-12">
                          <label class="form-label fw-semibold">Descripción</label>
                          <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control rounded-3"
  placeholder="Motivo de la deuda" />
                      </div>
                      <div class="col-md-4">
                          <label class="form-label fw-semibold">Monto Total ($)</label>
                          <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control rounded-3" placeholder="0.00"
   />
                      </div>
                      <div class="col-md-4">
                          <label class="form-label fw-semibold">Cantidad de Cuotas</label>
                          <asp:TextBox ID="txtCuotas" runat="server" CssClass="form-control rounded-3" TextMode="Number"
   placeholder="1" />
                      </div>
                      <div class="col-md-4">
                          <label class="form-label fw-semibold">Fecha de Inicio</label>
                          <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control rounded-3" TextMode="Date" />
                      </div>
                  </div>
              </div>
              <div class="modal-footer border-0 pt-0">
                  <button type="button" class="btn btn-outline-secondary rounded-pill"
  data-bs-dismiss="modal">Cancelar</button>
                  <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="btn btn-primary rounded-pill"
  OnClick="btnAgregar_Click1" />
              </div>
          </div>
      </div>
  </div>

  <!-- Modal Editar Deuda -->
  <div class="modal fade" id="modalEditarDeuda" tabindex="-1">
      <div class="modal-dialog modal-dialog-centered">
          <div class="modal-content rounded-4 border-0">
              <div class="modal-header border-0 pb-0">
                  <h5 class="modal-title fw-bold">Editar Deuda</h5>
                  <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
              </div>
              <div class="modal-body">
                  <asp:HiddenField ID="hfIdDeudaEditar" runat="server" />
                  <div class="row g-3">
                      <div class="col-12">
                          <label class="form-label fw-semibold">Nombre del Deudor</label>
                          <asp:TextBox ID="txtNombreEditar" runat="server" CssClass="form-control rounded-3" />
                      </div>
                      <div class="col-12">
                          <label class="form-label fw-semibold">Email del Deudor</label>
                          <asp:TextBox ID="txtEmailEditar" runat="server" CssClass="form-control rounded-3" />
                      </div>
                      <div class="col-12">
                          <label class="form-label fw-semibold">Descripción</label>
                          <asp:TextBox ID="txtDescripcionEditar" runat="server" CssClass="form-control rounded-3" />
                      </div>
                  </div>
              </div>
              <div class="modal-footer border-0 pt-0">
                  <button type="button" class="btn btn-outline-secondary rounded-pill"
  data-bs-dismiss="modal">Cancelar</button>
                  <asp:Button ID="btnGuardarEdicion" runat="server" Text="Guardar cambios" CssClass="btn btn-primary
  rounded-pill" OnClick="btnGuardarEdicion_Click" />
              </div>
          </div>
      </div>
  </div>

  </asp:Content>