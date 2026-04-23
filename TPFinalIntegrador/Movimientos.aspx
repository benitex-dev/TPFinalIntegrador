<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Movimientos.aspx.cs" Inherits="TPFinalIntegrador.Movimientos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    :root {
        --bs-primary: #0057cd;
        --bs-primary-rgb: 0, 87, 205;
        --surface-variant: #e1e3e4;
        --outline: #727787;
        --surface-container: #edeeef;
        --surface-container-high: #e7e8e9;
        --surface-container-lowest: #ffffff;
        --secondary-fixed: #93f7ba;
        --on-secondary-fixed: #002110;
        --primary-fixed: #dae2ff;
        --on-primary-fixed: #001946;
        --secondary-container: #90f4b7;
        --on-secondary-container: #007144;
        --tertiary-fixed: #ffdad9;
        --on-tertiary-fixed: #410008;
        --tertiary: #b91830;
        --secondary: #006d41;
    }

    body {
        font-family: 'Inter', sans-serif;
        background-color: #f8f9fa; /* bg-light equivalent */
        color: #191c1d;
    }

    h1, h2, h3, h4, .font-headline {
        font-family: 'Manrope', sans-serif;
        font-weight: 800;
        letter-spacing: -0.03em;
    }

    .material-symbols-outlined {
        font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        display: inline-block;
        vertical-align: middle;
    }

    .btn-filter {
        background-color: var(--surface-container-lowest);
        border: 1px solid rgba(114, 119, 135, 0.2);
        border-radius: 50rem;
        padding: 0.625rem 1.25rem;
        font-size: 0.875rem;
        font-weight: 500;
        color: #191c1d;
        transition: all 0.2s;
    }

        .btn-filter:hover {
            background-color: var(--surface-container);
        }

    .search-container {
        position: relative;
    }

        .search-container .material-symbols-outlined {
            position: absolute;
            left: 1.25rem;
            top: 50%;
            transform: translateY(-50%);
            color: var(--outline);
        }

    .search-input {
        padding-left: 3.5rem;
        padding-right: 1.5rem;
        height: 3.5rem;
        border-radius: 50rem;
        border: none;
        background-color: var(--surface-container-high);
        width: 100%;
    }

        .search-input:focus {
            box-shadow: 0 0 0 2px var(--bs-primary);
            outline: none;
        }

    .movement-card {
        background-color: var(--surface-container-lowest);
        border-radius: 1.5rem;
        padding: 2rem;
        box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.13);
-webkit-box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.13);
-moz-box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.13);
        /*box-shadow: 0 12px 32px rgba(0, 87, 206, 0.06);*/
    }

    .cabeceraMovimientos{
        border-radius: .5rem;
        background-color:#F2F2F2;
        height:2.3rem;
        align-content:center;
        margin-top:.5rem;
        margin-bottom:.5rem;
    }

    .movement-item {
        padding: 1.25rem;
        /*border-radius: 1.5rem;*/
        transition: all 0.2s;
        cursor: pointer;
        text-decoration: none;
        color: inherit;
    }

        .movement-item:hover {
            background-color: #f3f4f5;
        }

    .movement-card .movement-item:last-child {
    border-bottom: none !important;
}

.movement-item:has(+ .cabeceraMovimientos) {
    border-bottom: none !important;
}

    .icon-box {
        width: 3.5rem;
        height: 3.5rem;
        border-radius: 1rem;
        display: flex;
        align-items: center;
        justify-content: center;
        flex-shrink: 0;
    }

    .bg-summary-card {
        background: linear-gradient(135deg, var(--bs-primary) 0%, #0d6efd 100%);
        border-radius: 1.25rem;
        color: white;
        padding: 1.5rem 2rem;
    }

    .no-scrollbar::-webkit-scrollbar {
        display: none;
    }

    .no-scrollbar {
        -ms-overflow-style: none;
        scrollbar-width: none;
    }

    .btn-kebab {
    width: 32px;
    height: 32px;
    border-radius: 50%;
    color: #adb5bd; 
    transition: background-color 0.2s ease, color 0.2s ease;
}

    .btn-kebab:hover {
        background-color: rgba(0, 0, 0, 0.08);
        color: #495057 !important;
    }

        .filter-card {
            background-color: var(--surface-container-lowest);
            border-radius: 1.5rem;
            padding: .8rem;
            margin-bottom: 1rem;
            box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.13);
            -webkit-box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.13);
            -moz-box-shadow: 0px 1px 11px 1px rgba(0,0,0,0.13);
            /*box-shadow: 0 12px 32px rgba(0, 87, 206, 0.06);*/
        }


</style>

    <div class="bg-light">
        <main class="py-4 mb-5 pb-5">
            <%--<!-- Horizontal Summary Block -->
                <div class="bg-summary-card d-flex flex-column flex-md-row align-items-md-center justify-content-between gap-3 mb-5 shadow-sm">
                    <div class="d-flex align-items-center gap-4">
                        <div class="d-flex flex-column">
                            <span class="text-uppercase small fw-bold opacity-75 tracking-wider mb-1">Gastos del mes</span>
                            <div class="d-flex align-items-center gap-2">
                                <h2 class="mb-0 fw-extrabold font-headline" style="font-size: 2.5rem;">-$154k</h2>
                                <span class="material-symbols-outlined">trending_up</span>
                            </div>
                        </div>
                        <div class="vr opacity-25 d-none d-md-block mx-2"></div>
                        <p class="mb-0 small opacity-75 d-none d-md-block">Un 12% más que el mes pasado</p>
                    </div>
                    <button class="btn btn-light bg-opacity-25 border-0 text-white fw-bold px-4 py-2 rounded-3" style="background-color: rgba(255,255,255,0.2);">
                        Ver Detalles
                    </button>
                </div>--%>
            <!-- Page Title & Search -->
            <div class="row justify-content-center mx-0">
                <div class="col-lg-10 col-xl-8">
                    <div class="row align-items-end mb-4">
                        <div class="col-md-6">
                            <h1 class="display-6 mt-2 mb-3">Historial de Movimientos</h1>
                            <%--<div class="search-container">
                                <span class="material-symbols-outlined">search</span>
                                <input class="search-input" placeholder="Buscar por comercio o categoría..." type="text" />
                            </div>--%>

                            <div class="d-flex align-items-center bg-white border rounded-pill shadow-sm px-3 py-1 w-100" style="transition: all 0.2s ease-in-out;">
                                <span class="material-symbols-outlined text-muted fs-5">search</span>

                                <input class="form-control border-0 shadow-none bg-transparent w-100 ms-2"
                                    placeholder="Buscar por comercio o categoría..."
                                    type="text"
                                    style="outline: none;" />
                            </div>
                        </div>
                        <div class="col-md-6 text-md-end mt-4 mt-md-0">
                            <button class="btn btn-link text-primary fw-bold text-decoration-none d-inline-flex align-items-center gap-2">
                                <span class="material-symbols-outlined">refresh</span>
                                Actualizar listado
                            </button>
                        </div>
                    </div>
                    <!-- Filter Bar -->
                    <!-- Filter Bar -->
  <div class="filter-card">
      <div class="d-flex flex-wrap align-items-center gap-2">
 <%-- FILTRO PERÍODO --%>
  <div class="dropdown">
      <button class="btn-filter d-inline-flex align-items-center gap-2"
              type="button" data-bs-toggle="dropdown" aria-expanded="false">
          <asp:Label ID="lblFiltroPeriodo" runat="server" Text="Período"></asp:Label>
          <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
      </button>
      <ul class="dropdown-menu shadow-sm border-0">
          <asp:Repeater ID="rptPeriodos" runat="server">
              <ItemTemplate>
                  <li>
                      <asp:LinkButton ID="btnPeriodo" runat="server" CssClass="dropdown-item"
                          OnCommand="FiltroPeriodo_Command"
                          CommandArgument='<%# Eval("Valor") %>'><%# Eval("Texto") %></asp:LinkButton>
                  </li>
              </ItemTemplate>
          </asp:Repeater>
      </ul>
  </div>

          <%-- FILTRO TIPO DE OPERACIÓN --%>
          <div class="dropdown">
              <button class="btn-filter d-inline-flex align-items-center gap-2"
                      type="button" data-bs-toggle="dropdown" aria-expanded="false">
                  <asp:Label ID="lblFiltroTipo" runat="server" Text="Tipo de Operación"></asp:Label>
                  <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
              </button>
              <ul class="dropdown-menu shadow-sm border-0">
                  <li>
                      <asp:LinkButton ID="btnFiltroTodos" runat="server" CssClass="dropdown-item"
                          OnCommand="FiltroTipo_Command" CommandArgument="Todos">Todos</asp:LinkButton>
                  </li>
                  <li>
                      <asp:LinkButton ID="btnFiltroGastos" runat="server" CssClass="dropdown-item"
                          OnCommand="FiltroTipo_Command" CommandArgument="Gasto">Gastos</asp:LinkButton>
                  </li>
                  <li>
                      <asp:LinkButton ID="btnFiltroIngresos" runat="server" CssClass="dropdown-item"
                          OnCommand="FiltroTipo_Command" CommandArgument="Ingreso">Ingresos</asp:LinkButton>
                  </li>
              </ul>
          </div>

          <%-- FILTRO CATEGORÍA --%>
  <div class="dropdown">
      <button class="btn-filter d-inline-flex align-items-center gap-2"
              type="button" data-bs-toggle="dropdown" aria-expanded="false">
          <asp:Label ID="lblFiltroCategoria" runat="server" Text="Categoría"></asp:Label>
          <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
      </button>
      <ul class="dropdown-menu shadow-sm border-0">
          <asp:Repeater ID="rptCategorias" runat="server">
              <ItemTemplate>
                  <li>
                      <asp:LinkButton ID="btnCategoria" runat="server" CssClass="dropdown-item"
                          OnCommand="FiltroCategoria_Command"
                          CommandArgument='<%# Eval("Nombre") %>'><%# Eval("Nombre") %></asp:LinkButton>
                  </li>
              </ItemTemplate>
          </asp:Repeater>
      </ul>
  </div>
          <%-- FILTRO MEDIO DE PAGO --%>
  <div class="dropdown">
      <button class="btn-filter d-inline-flex align-items-center gap-2"
              type="button" data-bs-toggle="dropdown" aria-expanded="false">
          <asp:Label ID="lblFiltroMedioPago" runat="server" Text="Medio de pago"></asp:Label>
          <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
      </button>
      <ul class="dropdown-menu shadow-sm border-0">
          <asp:Repeater ID="rptMediosPago" runat="server">
              <ItemTemplate>
                  <li>
                      <asp:LinkButton ID="btnMedioPago" runat="server" CssClass="dropdown-item"
                          OnCommand="FiltroMedioPago_Command"
                          CommandArgument='<%# Eval("Nombre") %>'><%# Eval("Nombre") %></asp:LinkButton>
                  </li>
              </ItemTemplate>
          </asp:Repeater>
      </ul>
  </div>
          <asp:LinkButton ID="btnBorrarFiltros" runat="server"
              CssClass="btn btn-link text-primary small fw-bold text-decoration-none ms-md-auto border-0"
              OnClick="btnBorrarFiltros_Click"><small>Borrar filtros</small></asp:LinkButton>
      </div>
  </div>
                    <!-- Movements List -->
                    <div class="movement-card">
                        <!-- Group 1 -->
                        <asp:Repeater ID="rptMovimientos" runat="server" OnItemCommand="rptMovimientos_ItemCommand">
                            <ItemTemplate>

                                <asp:Literal ID="cabeceraMovimientos" runat="server" Text='<%# ObtenerCabeceraFecha(Eval("Fecha")) %>'></asp:Literal>
                                <%--<div class="cabeceraMovimientos"><%# if(ObtenerCabeceraFecha(Eval("Fecha")) != FechaGrupoAnterior) %></div>--%>

                                <div class="movement-item d-flex align-items-center border-bottom justify-content-between mb-0 px-2 pb-2">

                                    <div class="d-flex align-items-center gap-3">

                                        <div class='<%# Eval("Tipo").ToString() == "Gasto" ? "icon-box bg-danger-light text-danger rounded-circle d-flex align-items-center justify-content-center" : "icon-box bg-success-light text-success rounded-circle d-flex align-items-center justify-content-center" %>'
                                            style="width: 45px; height: 45px;">
                                            <span class="material-symbols-outlined fs-5">
                                                <%# Eval("Tipo").ToString() == "Gasto" ? "shopping_bag" : "payments" %>
                                            </span>
                                        </div>

                                        <div>
                                            <h4 class="h6 mb-0 fw-bold"><%# Eval("Descripcion") %></h4>
                                            <p class="mb-0 text-muted small"><%# Eval("Categoria") %></p>
                                            <%--<p class="mb-0 text-secondary-emphasis opacity-50 fw-medium" style="font-size: 0.7rem;">
                                        <%# Eval("Tipo").ToString() == "Gasto" ? Eval("MedioPago") : null %>
                                    </p>--%>
                                        </div>
                                    </div>

                                    <div class="d-flex align-items-center gap-3">

                                        <div class="text-end">
                                            <p class='<%# Eval("Tipo").ToString() == "Gasto" ? "mb-0 fw-bold font-headline h6 text-danger" : "mb-0 fw-bold font-headline h6 text-success" %>'>
                                                <%# Eval("MontoMostrado") %>
                                            </p>
                                            <%--<p class="mb-0 text-muted small"><%# Eval("Fecha", "{0:HH:mm} hs") %></p>--%>
                                        </div>

                                        <div class="dropdown">
                                            <button class="btn btn-link p-0 text-decoration-none text-secondary btn-kebab" style="display: inline-flex; align-items: center; justify-content: center;" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                <span class="material-symbols-outlined" style="font-size: 22px; line-height: 1;">more_vert</span>
                                            </button>

                                            <ul class="dropdown-menu dropdown-menu-end shadow-sm border-0">
                                                <li>
                                                    <asp:LinkButton ID="btnEditar" runat="server" CssClass="dropdown-item small"
                                                        CommandName="Editar" CommandArgument='<%# Eval("Tipo") + "|" + Eval("idReferencia") %>'>
                                ✏️ Editar
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="btnEliminar" runat="server" CssClass="dropdown-item small text-danger"
                                                        CommandName="Eliminar" CommandArgument='<%# Eval("Tipo") + "|" + Eval("idReferencia") %>'
                                                        OnClientClick="return confirm('¿Estás seguro de que querés borrar este movimiento?');">
                                🗑️ Eliminar
                                                    </asp:LinkButton>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </main>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModalsContent" runat="server">
        <%--MODAL INGRESO--%>
    <div class="modal fade" id="modalIngreso" tabindex="1" aria-labelledby="modalIngresoLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content border-0 rounded-4 shadow">

            <div class="modal-header border-0 pb-0">
                <h5 class="modal-title fw-bold" id="modalIngresoLabel" runat="server">Ingresos</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>

            <div class="modal-body pt-3">
                <asp:HiddenField ID="hfIdIngresoEdicion" runat="server" />
                <div class="mb-3">
                    <label class="form-label fw-semibold">Descripción</label>
                    <asp:TextBox ID="txtDescripcionIngreso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Fecha</label>
                    <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Monto</label>
                    <asp:TextBox ID="txtMontoIngreso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Categoría</label>
                    <asp:DropDownList ID="ddlCategoriaIngreso" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <asp:Label ID="lblMensajeIngreso" runat="server" CssClass="d-block text-center mt-3"></asp:Label>

            </div>

            <div class="modal-footer border-0 pt-0">
                <button type="button" class="btn btn-light border rounded-3" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnGuardarIngreso" runat="server" Text="Guardar" CssClass="btn btn-primary rounded-3 px-4" OnClick="btnGuardarIngreso_Click" />
            </div>

        </div>
    </div>
</div>

    <%--CARGAR GASTO--%>
<div class="modal fade" id="modalGasto" tabindex="-1" aria-labelledby="modalGastoLabel" aria-hidden="true">
<div class="modal-dialog modal-dialog-centered modal-lg">
    <div class="modal-content border-0 rounded-4 shadow">

        <div class="modal-header border-0 pb-0">
            <h5 class="modal-title fw-bold" id="modalGastoLabel" runat="server">Gastos</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
        </div>

        <div class="modal-body pt-3">
            <asp:HiddenField ID="hfIdGastoEdicion" runat="server" />
            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label fw-semibold">Descripción</label>
                    <asp:TextBox ID="txtDescripcionGasto" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-6 mb-3">
                    <label class="form-label fw-semibold">Fecha</label>
                    <asp:TextBox ID="txtFechaGasto" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label fw-semibold">Categoría</label>
                    <asp:DropDownList ID="ddlCategoriaGasto" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-6 mb-3">
                    <label class="form-label fw-semibold">Medio de pago</label>
                    <asp:DropDownList ID="ddlMedioPagoGasto" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
            </div>

            <%-- FORMA DE PAGO --%>
            <div class="row">
                <div class="col-md-12 mb-3">
                    <label class="form-label fw-semibold">Forma de pago</label>
                    <div class="d-flex gap-4">
                        <div class="form-check">
                            <asp:RadioButton ID="rbUnPago" runat="server" GroupName="FormaPago" CssClass="form-check-input" Checked="true" onclick="toggleFormaPagoGasto()" />
                            <label class="form-check-label">En un pago</label>
                        </div>
                        <div class="form-check">
                            <asp:RadioButton ID="rbCuotas" runat="server" GroupName="FormaPago" CssClass="form-check-input" onclick="toggleFormaPagoGasto()" />
                            <label class="form-check-label">En cuotas</label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4 mb-3">
                    <label class="form-label fw-semibold">Moneda</label>

                    <asp:DropDownList ID="ddlMonedaGasto"
                                      runat="server"
                                      CssClass="form-select"
                                      onchange="toggleCamposMonedaGasto()">
                        <asp:ListItem Text="ARS" Value="1"></asp:ListItem>
                        <asp:ListItem Text="USD" Value="2"></asp:ListItem>
                        <asp:ListItem Text="EUR" Value="3"></asp:ListItem>
                        <asp:ListItem Text="BRL" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <%-- MONTO NORMAL --%>
                <div class="col-md-4 mb-3" id="campoMontoPesosGasto">
                    <label class="form-label fw-semibold">Monto en pesos</label>
                    <asp:TextBox ID="txtMontoPesosGasto" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <%-- MONTO TOTAL --%>
                <div class="col-md-4 mb-3" id="campoMontoCuotaGasto" style="display:none;">
                    <label class="form-label fw-semibold">Monto total</label>
                    <asp:TextBox ID="txtMontoCuotaGasto" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <%-- CANTIDAD CUOTAS --%>
                <div class="col-md-4 mb-3" id="campoCantidadCuotasGasto" style="display:none;">
                    <label class="form-label fw-semibold">Cantidad de cuotas</label>
                    <asp:TextBox ID="txtCantidadCuotasGasto" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <%-- USD --%>
                <div class="col-md-4 mb-3" id="campoMontoUSDGasto" style="display:none;">
                    <label class="form-label fw-semibold">Monto en moneda original</label>
                    <asp:TextBox ID="txtMontoUSDGasto" runat="server" CssClass="form-control" oninput="calcularMontoPesosGasto()"></asp:TextBox>
                </div>
            </div>

            <div class="row" id="campoCotizacionGasto" style="display:none;">
                <div class="col-md-6 mb-3">
                    <label class="form-label fw-semibold">Cotización</label>
                    <asp:TextBox ID="txtCotizacionGasto" runat="server" CssClass="form-control" oninput="calcularMontoPesosGasto()"></asp:TextBox>
                </div>
            </div>

            <asp:Label ID="lblMensajeGasto" runat="server" CssClass="d-block text-center mt-3"></asp:Label>

        </div>
       <%-- BOTÓN GUARDAR GASTO--%>
        <div class="modal-footer border-0 pt-0">
            <button type="button" class="btn btn-light border rounded-3" data-bs-dismiss="modal">Cancelar</button>
            <asp:Button ID="btnGuardarGasto"
                        runat="server"
                        Text="Guardar"
                        CssClass="btn btn-primary rounded-3 px-4"
                        OnClick="btnGuardarGasto_Click" />
        </div>
</div>

    </div>
</div>
</asp:Content>
