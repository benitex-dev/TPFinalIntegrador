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
        box-shadow: 0 12px 32px rgba(0, 87, 206, 0.06);
    }

    .movement-item {
        padding: 1.25rem;
        border-radius: 1.5rem;
        transition: all 0.2s;
        cursor: pointer;
        text-decoration: none;
        color: inherit;
    }

        .movement-item:hover {
            background-color: #f3f4f5;
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
            <div class="row align-items-end mb-4">
                <div class="col-md-8">
                    <h1 class="display-6 mb-1">Historial de Movimientos</h1>
                    <div class="search-container">
                        <span class="material-symbols-outlined">search</span>
                        <input class="search-input" placeholder="Buscar por comercio o categoría..." type="text" />
                    </div>
                </div>
                <div class="col-md-4 text-md-end mt-4 mt-md-0">
                    <button class="btn btn-link text-primary fw-bold text-decoration-none d-inline-flex align-items-center gap-2">
                        <span class="material-symbols-outlined">download</span>
                        Exportar Resumen
                    </button>
                </div>
            </div>
            <!-- Filter Bar -->
            <div class="d-flex flex-wrap align-items-center gap-2 mb-5 no-scrollbar overflow-x-auto pb-2">
                <button class="btn-filter d-inline-flex align-items-center gap-2">
                    Período <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
                </button>
                <button class="btn-filter d-inline-flex align-items-center gap-2">
                    Tipo de Operación <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
                </button>
                <button class="btn-filter d-inline-flex align-items-center gap-2">
                    Categoría <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
                </button>
                <button class="btn-filter d-inline-flex align-items-center gap-2">
                    Medios de pago <span class="material-symbols-outlined fs-6">keyboard_arrow_down</span>
                </button>
                <button class="btn btn-link text-primary small fw-bold text-decoration-none ms-md-auto p-0 border-0">
                    Borrar filtros
                </button>
            </div>
            <!-- Movements List -->
            <div class="movement-card">
                <!-- Group 1 -->
                <asp:Repeater ID="rptMovimientos" runat="server" OnItemCommand="rptMovimientos_ItemCommand">
                    <ItemTemplate>

                        <%# ObtenerCabeceraFecha(Eval("Fecha")) %>

                        <div class="movement-item d-flex align-items-center justify-content-between mb-3 px-2 border-bottom pb-2">

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
                                    <p class="mb-0 text-secondary-emphasis opacity-50 fw-medium" style="font-size: 0.7rem;">
                                        Dinero en cuenta
                   
                                    </p>
                                </div>
                            </div>

                            <div class="d-flex align-items-center gap-3">

                                <div class="text-end">
                                    <p class='<%# Eval("Tipo").ToString() == "Gasto" ? "mb-0 fw-bold font-headline h5 text-danger" : "mb-0 fw-bold font-headline h5 text-success" %>'>
                                        <%# Eval("MontoMostrado") %>
                                    </p>
                                    <p class="mb-0 text-muted small"><%# Eval("Fecha", "{0:HH:mm} hs") %></p>
                                </div>

                                <div class="dropdown">
                                    <button class="btn btn-link p-0 text-decoration-none text-secondary" style="display: inline-flex; align-items: center; justify-content: center;" type="button" data-bs-toggle="dropdown" aria-expanded="false">
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
        </main>
    </div>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
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
