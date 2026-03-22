<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="TPFinalIntegrador.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .inicio-page {
            padding-top: 2rem;
            padding-bottom: 2rem;
        }

        .welcome-card,
        .summary-card,
        .panel-card,
        .table-card,
        .quick-card {
            background-color: #ffffff;
            border: 1px solid rgba(0,0,0,.06);
            border-radius: 24px;
            box-shadow: 0 10px 30px rgba(0,0,0,.05);
        }

        .welcome-card {
            padding: 2rem;
            margin-bottom: 1.5rem;
        }

        .summary-card,
        .quick-card,
        .panel-card {
            padding: 1.25rem;
        }

        .table-card {
            overflow: hidden;
        }

        .page-title {
            font-size: 2rem;
            font-weight: 800;
            margin-bottom: .4rem;
            color: #111827;
        }

        .page-subtitle {
            color: #6b7280;
            font-size: 1rem;
            margin-bottom: 0;
        }

        .welcome-icon {
            width: 64px;
            height: 64px;
            border-radius: 18px;
            display: flex;
            align-items: center;
            justify-content: center;
            background: rgba(var(--bs-primary-rgb), .10);
        }

        .welcome-icon .material-symbols-outlined,
        .summary-icon .material-symbols-outlined,
        .quick-icon .material-symbols-outlined {
            font-size: 30px;
        }

        .section-title {
            font-size: 1.1rem;
            font-weight: 700;
            color: #111827;
            margin-bottom: 1rem;
        }

        .summary-card .summary-label {
            font-size: .82rem;
            color: #6b7280;
            font-weight: 600;
            margin-bottom: .4rem;
        }

        .summary-card .summary-value {
            font-size: 1.8rem;
            font-weight: 800;
            margin-bottom: .25rem;
            line-height: 1;
            color: #111827;
        }

        .summary-card .summary-foot {
            font-size: .88rem;
            color: #6b7280;
            margin-bottom: 0;
        }

        .summary-icon {
            width: 52px;
            height: 52px;
            border-radius: 16px;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 1rem;
        }

        .summary-green .summary-icon {
            background: rgba(25, 135, 84, .12);
            color: #198754;
        }

        .summary-red .summary-icon {
            background: rgba(220, 53, 69, .12);
            color: #dc3545;
        }

        .summary-blue .summary-icon {
            background: rgba(13, 110, 253, .12);
            color: #0d6efd;
        }

        .summary-yellow .summary-icon {
            background: rgba(255, 193, 7, .18);
            color: #b88900;
        }

        .custom-table thead th {
            font-size: .75rem;
            text-transform: uppercase;
            letter-spacing: .04em;
            color: #6b7280;
            font-weight: 700;
            border-bottom: 1px solid rgba(0,0,0,.06);
            background: #f8fafc;
        }

        .custom-table tbody td {
            vertical-align: middle;
            border-color: rgba(0,0,0,.05);
            color: #6b7280;
        }

        .badge-soft-primary {
            background: rgba(13, 110, 253, .10);
            color: #0d6efd;
            font-weight: 600;
            border-radius: 999px;
            padding: .45rem .8rem;
        }

        .badge-soft-success {
            background: rgba(25, 135, 84, .10);
            color: #198754;
            font-weight: 600;
            border-radius: 999px;
            padding: .45rem .8rem;
        }

        .badge-soft-danger {
            background: rgba(220, 53, 69, .10);
            color: #dc3545;
            font-weight: 600;
            border-radius: 999px;
            padding: .45rem .8rem;
        }

        .quick-link {
            text-decoration: none;
            color: inherit;
            display: block;
        }

        .quick-item {
            border: 1px solid rgba(0,0,0,.06);
            border-radius: 18px;
            padding: 1rem;
            transition: .2s ease;
            background-color: #fff;
        }

        .quick-item:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(0,0,0,.05);
            border-color: rgba(13, 110, 253, .20);
        }

        .quick-icon {
            width: 46px;
            height: 46px;
            border-radius: 14px;
            display: flex;
            align-items: center;
            justify-content: center;
            background: rgba(var(--bs-primary-rgb), .10);
            color: #0d6efd;
            margin-bottom: .9rem;
        }

        .sidebar-note {
            font-size: .92rem;
            color: #6b7280;
            margin-bottom: 0;
        }

        .progress {
            height: .8rem;
            border-radius: 999px;
            background-color: #e9ecef;
        }

        .mini-kpi {
            border-radius: 18px;
            padding: 1rem;
            border: 1px solid rgba(0,0,0,.06);
        }

        .mini-kpi h6 {
            font-size: .78rem;
            text-transform: uppercase;
            letter-spacing: .04em;
            margin-bottom: .4rem;
            font-weight: 700;
        }

        .mini-kpi p {
            font-size: 1.35rem;
            font-weight: 800;
            margin-bottom: 0;
        }

        .empty-text {
            color: #9ca3af;
        }
    </style>

    <div class="inicio-page">

        <div class="welcome-card">
            <div class="row align-items-center g-4">
                <div class="col-lg-8">
                    <div class="d-flex align-items-center gap-3 mb-3">
                        <div class="welcome-icon">
                            <span class="material-symbols-outlined text-primary">account_balance_wallet</span>
                        </div>
                        <div>
                            <h1 class="page-title">
                                <asp:Label ID="lblBienvenida" runat="server" Text="Bienvenido/a"></asp:Label>
                            </h1>
                            <p class="page-subtitle"></p> 
                        </div>
                    </div>

                    <div class="d-flex flex-wrap gap-2">
                        <div class="d-flex flex-wrap gap-2">
                            <button type="button" class="btn btn-primary px-4 fw-semibold" data-bs-toggle="modal" data-bs-target="#modalGasto" onclick="limpiarModalGasto()">
                            Cargar gasto
                            </button>
                            <button type="button" class="btn btn-outline-primary px-4 fw-semibold" data-bs-toggle="modal" data-bs-target="#modalIngreso" onclick="limpiarModalIngreso()">
                            Cargar ingreso
                            </button>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="panel-card">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <span class="fw-semibold text-dark">Progreso mensual</span>
                            <span class="text-muted fw-bold">--</span>
                        </div>
                        <div class="progress mb-3">
                            <div class="progress-bar bg-primary" style="width:0%"></div>
                        </div>
                        <p class="sidebar-note">Acá vas a poder visualizar tu avance mensual una vez que cargues movimientos.</p>
                    </div>
                </div>
            </div>
        </div>

        <div class="row g-4 mb-4">
            <div class="col-md-6 col-xl-3">
                <div class="summary-card summary-green h-100">
                    <div class="summary-icon">
                        <span class="material-symbols-outlined">trending_up</span>
                    </div>
                    <div class="summary-label">Ingresos del mes</div>
                    <div class="summary-value text-success">
                    <asp:Label ID="lblIngresosMes" runat="server" Text="--"></asp:Label>
                    </div>
                    <p class="summary-foot">Total acumulado del mes actual</p>
                  </div>
            </div>

            <div class="col-md-6 col-xl-3">
                <div class="summary-card summary-red h-100">
                    <div class="summary-icon">
                        <span class="material-symbols-outlined">trending_down</span>
                    </div>
                    <div class="summary-label">Gastos del mes</div>
                    <div class="summary-value text-danger">
                        <asp:Label ID="lblGastosMes" runat="server" Text="--"></asp:Label>
                    </div>
                    <p class="summary-foot">Todavía no hay datos para mostrar</p>
                </div>
            </div>

            <div class="col-md-6 col-xl-3">
                <div class="summary-card summary-blue h-100">
                    <div class="summary-icon">
                        <span class="material-symbols-outlined">savings</span>
                    </div>
                    <div class="summary-label">Saldo disponible</div>
                    <div class="summary-value">
                        <asp:Label ID="lblsaldoMes" runat="server" Text="--"></asp:Label>
                    </div>
                    <p class="summary-foot">Se calculará según tus registros</p>
                </div>
            </div>

            <div class="col-md-6 col-xl-3">
                <div class="summary-card summary-yellow h-100">
                    <div class="summary-icon">
                        <span class="material-symbols-outlined">flag</span>
                    </div>
                    <div class="summary-label">Metas de ahorro</div>
                    <div class="summary-value">--</div>
                    <p class="summary-foot">Todavía no hay datos para mostrar</p>
                </div>
            </div>
        </div>

        <div class="row g-4">
            <div class="col-xl-8">
                <div class="table-card">
                    <div class="p-4 border-bottom">
                        <div class="d-flex justify-content-between align-items-center flex-wrap gap-2">
                            <h2 class="section-title mb-0">Últimos movimientos</h2>
                            <a href="#" class="btn btn-sm btn-outline-primary">Ver todos</a>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <table class="table custom-table align-middle mb-0">
                            <thead>
                                <tr>
                                    <th class="px-4 py-3">Fecha</th>
                                    <th class="px-4 py-3">Descripción</th>
                                    <th class="px-4 py-3">Categoría</th>
                                    <th class="px-4 py-3">Tipo</th>
                                    <th class="px-4 py-3 text-end">Monto</th>
                                    <th class="px-4 py-3">Estado</th>
                                </tr>
                            </thead>
                            <tbody>
    <asp:Repeater ID="rptMovimientos" runat="server">
        <ItemTemplate>
            <tr>
                <td class="px-4 py-3"><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
                <td class="px-4 py-3"><%# Eval("Descripcion") %></td>
                <td class="px-4 py-3"><%# Eval("Categoria") %></td>
                <td class="px-4 py-3"><%# Eval("Tipo") %></td>
                <td class="px-4 py-3 text-end">
                 <span class='<%# Eval("Tipo").ToString() == "Gasto" ? "text-danger fw-bold" : "text-success fw-bold" %>'>
                  <%# Eval("MontoMostrado") %>
                 </span>
                    </td>
                <td class="px-4 py-3"><%# Eval("Estado") %></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</tbody>
                        </table>
                    </div>

                    <div class="p-4 border-top bg-light-subtle">
                        <small class="text-muted fw-semibold">Cuando registres ingresos y gastos, van a aparecer acá.</small>
                    </div>
                </div>
            </div>

            <div class="col-xl-4">
                <div class="d-grid gap-4">

                    <div class="panel-card">
                        <h3 class="section-title">Resumen rápido</h3>

                        <div class="row g-3">
                            <div class="col-6">
                                <div class="mini-kpi bg-success bg-opacity-10 border-success-subtle">
                                    <h6 class="text-success">Ingresos</h6>
                                    <p class="text-success">--</p>
                                </div>
                            </div>

                            <div class="col-6">
                                <div class="mini-kpi bg-danger bg-opacity-10 border-danger-subtle">
                                    <h6 class="text-danger">Gastos</h6>
                                    <p class="text-danger">--</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="panel-card">
                        <h3 class="section-title">Accesos rápidos</h3>

                        <div class="d-grid gap-3">
                            <a href="#" class="quick-link">
                                <div class="quick-item">
                                    <div class="quick-icon">
                                        <span class="material-symbols-outlined">add_card</span>
                                    </div>
                                    <div class="fw-bold mb-1">Registrar gasto</div>
                                    <div class="text-muted small">Cargá un nuevo gasto manualmente</div>
                                </div>
                            </a>

                            <a href="#" class="quick-link">
                                <div class="quick-item">
                                    <div class="quick-icon">
                                        <span class="material-symbols-outlined">payments</span>
                                    </div>
                                    <div class="fw-bold mb-1">Registrar ingreso</div>
                                    <div class="text-muted small">Sumá un ingreso al período actual</div>
                                </div>
                            </a>

                            <a href="#" class="quick-link">
                                <div class="quick-item">
                                    <div class="quick-icon">
                                        <span class="material-symbols-outlined">monitoring</span>
                                    </div>
                                    <div class="fw-bold mb-1">Ver métricas</div>
                                    <div class="text-muted small">Consultá gráficos y reportes</div>
                                </div>
                            </a>
                        </div>
                    </div>

                    <div class="panel-card">
                        <h3 class="section-title">Información</h3>
                        <p class="sidebar-note">
                            Esta pantalla va a mostrar tu resumen general una vez que comiences a registrar datos en el sistema.
                        </p>
                    </div>

                </div>
            </div>
        </div>

    </div>

    <%-- VENTANAS MODALES PARA CARGAR GASTOS, CATEGORÍAS, MEDIOS DE PAGO E INGRESOS --%>
    <div class="modal fade" id="modalCategoria" tabindex="-1" aria-labelledby="modalCategoriaLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content border-0 rounded-4 shadow">

            <div class="modal-header border-0 pb-0">
                <h5 class="modal-title fw-bold" id="modalCategoriaLabel">Nueva categoría</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>

            <div class="modal-body pt-3">
                <div class="mb-3">
                    <label class="form-label fw-semibold">Nombre</label>
                    <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Tipo</label>
                    <asp:DropDownList ID="ddlTipoCategoria" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Ingreso" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Gasto" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <asp:Label ID="lblMensajeCategoria" runat="server" CssClass="d-block text-center mt-3"></asp:Label>
            </div>

            <div class="modal-footer border-0 pt-0">
                <button type="button" class="btn btn-light border rounded-3" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnGuardarCategoria" runat="server" Text="Guardar" CssClass="btn btn-primary rounded-3 px-4" OnClick="btnGuardarCategoria_Click" />
            </div>

        </div>
    </div>
</div>
    <%--MODAL INGRESO--%>
    <div class="modal fade" id="modalIngreso" tabindex="-1" aria-labelledby="modalIngresoLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content border-0 rounded-4 shadow">

            <div class="modal-header border-0 pb-0">
                <h5 class="modal-title fw-bold" id="modalIngresoLabel">Nuevo ingreso</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>

            <div class="modal-body pt-3">

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


    <div class="modal fade" id="modalMedioPago" tabindex="-1" aria-labelledby="modalMedioPagoLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content border-0 rounded-4 shadow">

            <div class="modal-header border-0 pb-0">
                <h5 class="modal-title fw-bold" id="modalMedioPagoLabel">Nuevo medio de pago</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>

            <div class="modal-body pt-3">

                <div class="mb-3">
                    <label class="form-label fw-semibold">Tipo</label>
                    <asp:DropDownList ID="ddlTipoMedioPago"
                                      runat="server"
                                      CssClass="form-select"
                                      onchange="toggleCamposCredito()">
                        <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Efectivo" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Débito" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Crédito" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Transferencia" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Billetera Virtual" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Descripción</label>
                    <asp:TextBox ID="txtDescripcionMedioPago" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div id="camposCredito" style="display:none;">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Día de cierre</label>
                        <asp:TextBox ID="txtDiaCierre" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold">Día de vencimiento</label>
                        <asp:TextBox ID="txtDiaVencimiento" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>

                <asp:Label ID="lblMensajeMedioPago" runat="server" CssClass="d-block text-center mt-3"></asp:Label>

            </div>

            <div class="modal-footer border-0 pt-0">
                <button type="button" class="btn btn-light border rounded-3" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnGuardarMedioPago"
                            runat="server"
                            Text="Guardar"
                            CssClass="btn btn-primary rounded-3 px-4"
                            OnClick="btnGuardarMedioPago_Click" />
            </div>

        </div>
    </div>
</div>
    <%--CARGAR GASTO--%>
    <div class="modal fade" id="modalGasto" tabindex="-1" aria-labelledby="modalGastoLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content border-0 rounded-4 shadow">

            <div class="modal-header border-0 pb-0">
                <h5 class="modal-title fw-bold" id="modalGastoLabel">Nuevo gasto</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>

            <div class="modal-body pt-3">

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

                    <div class="col-md-4 mb-3">
                        <label class="form-label fw-semibold">Monto en pesos</label>
                       
                        <asp:TextBox ID="txtMontoPesosGasto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4 mb-3" id="campoMontoUSDGasto" style="display:none;">
                        <label class="form-label fw-semibold">Monto en moneda original</label>
                        <asp:TextBox 
                            ID="txtMontoUSDGasto"
                            runat="server" 
                            CssClass="form-control"
                            oninput="calcularMontoPesosGasto()"
                            ></asp:TextBox>
                    </div>
                </div>

                <div class="row" id="campoCotizacionGasto" style="display:none;">
                    <div class="col-md-6 mb-3">
                        <label class="form-label fw-semibold">Cotización</label>
                        <asp:TextBox 
                            ID="txtCotizacionGasto" 
                            runat="server" 
                            CssClass="form-control"
                            oninput="calcularMontoPesosGasto()"
                            ></asp:TextBox>
                    </div>
                </div>

                <asp:Label ID="lblMensajeGasto" runat="server" CssClass="d-block text-center mt-3"></asp:Label>

            </div>

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

     <%-- Se ocultan campos FECHA cuando no es crèdito el tipo de medio de pago  --%>
    <script> 
        function toggleCamposCredito() {
            const ddl = document.getElementById('<%= ddlTipoMedioPago.ClientID %>');
            const contenedor = document.getElementById('camposCredito'); if (ddl.value === "3") { contenedor.style.display = "block"; } else { contenedor.style.display = "none"; }
        }
        function limpiarModalMedioPago() {
            document.getElementById('<%= ddlTipoMedioPago.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= txtDescripcionMedioPago.ClientID %>').value = '';
            document.getElementById('<%= txtDiaCierre.ClientID %>').value = '';
            document.getElementById('<%= txtDiaVencimiento.ClientID %>').value = '';
            let lbl = document.getElementById('<%= lblMensajeMedioPago.ClientID %>');
            lbl.innerText = '';
            lbl.className = '';
            document.getElementById('camposCredito').style.display = 'none';
        } 

    </script>

    <%-- Se oculta Cotizaciòn cuando no es moneda extranjera  --%>
    
    <script>
        function toggleCamposMonedaGasto() {
            const ddl = document.getElementById('<%= ddlMonedaGasto.ClientID %>');
            const campoMontoUSD = document.getElementById('campoMontoUSDGasto');
            const campoCotizacion = document.getElementById('campoCotizacionGasto');
            const txtMontoPesos = document.getElementById('<%= txtMontoPesosGasto.ClientID %>');
        const txtMontoUSD = document.getElementById('<%= txtMontoUSDGasto.ClientID %>');
            const txtCotizacion = document.getElementById('<%= txtCotizacionGasto.ClientID %>');

            if (ddl.value === "1") {
                // Pesos argentinos — ocultamos todo
                campoMontoUSD.style.display = "none";
                campoCotizacion.style.display = "none";
                txtMontoPesos.disabled = false;
                txtMontoUSD.value = '';
                txtCotizacion.value = '';
            } else {
                // Moneda extranjera — mostramos campos y traemos cotización
                campoMontoUSD.style.display = "block";
                campoCotizacion.style.display = "flex";
                txtMontoPesos.value = '';
                txtMontoPesos.disabled = true;

                // Llamamos a la API automáticamente
                buscarCotizacion(ddl.options[ddl.selectedIndex].text);
            }
        }
        function limpiarModalGasto() {
            document.getElementById('<%= txtDescripcionGasto.ClientID %>').value = '';
            document.getElementById('<%= txtFechaGasto.ClientID %>').value = '';
            document.getElementById('<%= ddlCategoriaGasto.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlMedioPagoGasto.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlMonedaGasto.ClientID %>').selectedIndex = 0;

            document.getElementById('<%= txtMontoPesosGasto.ClientID %>').value = '';
            document.getElementById('<%= txtMontoPesosGasto.ClientID %>').disabled = false;

            document.getElementById('<%= txtMontoUSDGasto.ClientID %>').value = '';
            document.getElementById('<%= txtCotizacionGasto.ClientID %>').value = '';

        let lbl = document.getElementById('<%= lblMensajeGasto.ClientID %>');
            lbl.innerText = '';
            lbl.className = '';

            document.getElementById('campoMontoUSDGasto').style.display = 'none';
            document.getElementById('campoCotizacionGasto').style.display = 'none';
        }
        function calcularMontoPesosGasto() {
            const txtMontoUSD = document.getElementById('<%= txtMontoUSDGasto.ClientID %>');
            const txtCotizacion = document.getElementById('<%= txtCotizacionGasto.ClientID %>');
            const txtMontoPesos = document.getElementById('<%= txtMontoPesosGasto.ClientID %>');
            const ddl = document.getElementById('<%= ddlMonedaGasto.ClientID %>');

            if (ddl.value === "1") return;

            const montoOriginal = parseFloat(txtMontoUSD.value.replace(',', '.'));
            const cotizacion = parseFloat(txtCotizacion.value.replace(',', '.'));

            if (!isNaN(montoOriginal) && !isNaN(cotizacion)) {
                txtMontoPesos.value = (montoOriginal * cotizacion).toFixed(2);
            } else {
                txtMontoPesos.value = '';
            }
        }


        async function buscarCotizacion(moneda) {            
            
            const txtCotizacion = document.getElementById('<%= txtCotizacionGasto.ClientID %>');
                
            // Mostramos que está cargando
            txtCotizacion.value = 'Cargando...';
            txtCotizacion.disabled = true;

            try {
                //buscamos la cotización según la moneda seleccionada
                const cotizacion = await obtenerCotizacion(moneda);
                // Usamos el valor de VENTA
                // .toFixed(2) devuelve un string redondeado a 2 decimales.
                txtCotizacion.value = cotizacion.toFixed(2);
            }
            catch (error) {
                txtCotizacion.value = '';
                alert('No se pudo obtener la cotización. Ingresala manualmente.');
            }
            finally {
                txtCotizacion.disabled = false;
                // Recalculamos por si ya había monto cargado
                calcularMontoPesosGasto();
            }
        }

        function limpiarModalIngreso() {
            document.getElementById('<%= txtDescripcionIngreso.ClientID %>').value = '';
            document.getElementById('<%= txtFechaIngreso.ClientID %>').value = '';
            document.getElementById('<%= ddlCategoriaIngreso.ClientID %>').selectedIndex = 0;
  <%-- // document.getElementById('<%= ddlMonedaIngreso.ClientID %>').selectedIndex = 0;--%>
            document.getElementById('<%= txtMontoIngreso.ClientID %>').value = '';
            let lbl = document.getElementById('<%= lblMensajeIngreso.ClientID %>');
            lbl.innerText = '';
            lbl.className = '';
            if (document.getElementById('campoMontoUSDIngreso')) {
                document.getElementById('campoMontoUSDIngreso').style.display = 'none';
            }
        }
    </script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const params = new URLSearchParams(window.location.search);
            const modal = params.get("modal");
            if (modal === "categoria") {
                var modalCategoria = new bootstrap.Modal(document.getElementById('modalCategoria'));
                modalCategoria.show();
            }
            if (modal === "mediopago") {
                var modalMedioPago = new bootstrap.Modal(document.getElementById('modalMedioPago'));
                modalMedioPago.show();
            }
        });
    </script>
   
   
</asp:Content>