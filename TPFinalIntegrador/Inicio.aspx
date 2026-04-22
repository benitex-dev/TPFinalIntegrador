<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="TPFinalIntegrador.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        body {
            font-family: 'Inter', sans-serif;
            background-color: #f8f9fa;
            color: #191c1d;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
        h1, h2, h3, h4, .balance-text {
            font-family: 'Manrope', sans-serif;
            font-weight: 800;
            letter-spacing: -0.03em;
        }
        .rounded-4 {
            border-radius: 1.5rem !important;
        }
        .btn-rounded {
            border-radius: 2rem;
        }
        .icon-box {
            width: 48px;
            height: 48px;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 1rem;
        }
        .bg-primary-light { background-color: #dae2ff; color: #0057cd; }
        .bg-success-light { background-color: #93f7ba; color: #006d41; }
        .bg-danger-light { background-color: #ffdad9; color: #b91830; }
        
        .progress { height: 8px; border-radius: 4px; }
        
        /* Custom Bottom Nav for Mobile */
        .bottom-nav {
            position: fixed;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(255, 255, 255, 0.9);
            backdrop-filter: blur(10px);
            border-top: 1px solid #dee2e6;
            z-index: 1030;
        }
        
        .nav-link-custom {
            display: flex;
            flex-direction: column;
            align-items: center;
            font-size: 10px;
            text-transform: uppercase;
            font-weight: 600;
            color: #6c757d;
            text-decoration: none;
            padding: 10px 0;
        }
        .nav-link-custom.active {
            color: #0d6efd;
        }
        
        @media (max-width: 768px) {
            .container {
                padding-left: 12px;
                padding-right: 12px;
            }
        }

        .btn-glow-primary {
            background-color: #0d6efd; /* Si usás otro azul en tu paleta, cambialo acá */
            color: #ffffff;
            border: none;
            /* Eje X, Eje Y, Difuminado, Color azul con 30% de opacidad */
            box-shadow: 0px 8px 24px rgba(13, 110, 253, 0.3);
            transition: all 0.3s ease;
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

            .btn-glow-primary:hover {
                transform: translateY(-2px);
                box-shadow: 0px 12px 28px rgba(13, 110, 253, 0.4);
                color: #ffffff;
            }

        
        .btn-outline-subtle {
            background-color: #ffffff;
            color: #0d6efd;
            border: 1px solid #e2e8f0;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.02);
            transition: all 0.3s ease;
        }

            .btn-outline-subtle:hover {
                background-color: #f8f9fa;
                border-color: #cbd5e1;
                color: #0056b3;
            }
    </style>

    <div class="bg-light">
        
        <!-- Top Navigation (Desktop) -->
        <%--<nav class="navbar navbar-expand-md navbar-light bg-white sticky-top shadow-sm d-none d-md-block">
            <div class="container py-2">
                <a class="navbar-brand d-flex align-items-center gap-2" href="#">
                    <span class="material-symbols-outlined text-primary">account_balance</span>
                    <span class="fw-bold">The Private Banker</span>
                </a>
                <div class="ms-auto d-flex align-items-center gap-4">
                    <a class="nav-link active text-primary fw-semibold" href="#">Inicio</a>
                    <a class="nav-link text-secondary" href="#">Ahorros</a>
                    <a class="nav-link text-secondary" href="#">Deudores</a>
                    <a class="nav-link text-secondary" href="#">Perfil</a>
                    <button class="btn btn-primary btn-rounded px-4 ms-2">Inicio</button>
                </div>
            </div>
        </nav>
        <!-- Mobile Header -->
        <div class="d-md-none bg-white p-3 shadow-sm sticky-top">
            <div class="d-flex justify-content-between align-items-center">
                <div class="d-flex align-items-center gap-2">
                    <span class="material-symbols-outlined text-primary">account_balance</span>
                    <span class="fw-bold h5 mb-0">The Private Banker</span>
                </div>
                <button class="btn btn-primary btn-sm btn-rounded px-3">Inicio</button>
            </div>
        </div>--%>
        <main class="py-4 mb-5 pb-5">
            <!-- Hero Section -->
            <section class="row align-items-center mb-4 g-3">
                <div class="col-lg-7">
                    <h1 class="display-6 mb-1">Tu Resumen Personal</h1>
                    <p class="text-secondary fw-medium">Gestiona tu patrimonio con precisión editorial.</p>
                </div>
                <div class="col-lg-5 d-flex gap-2 justify-content-lg-end">
                    <button type="button" class="btn btn-glow-primary rounded-pill d-flex align-items-center gap-2 px-4 py-2 fw-bold" data-bs-toggle="modal" data-bs-target="#modalGasto" onclick="limpiarModalGasto()">
                        <span class="material-symbols-outlined fs-5">add_circle</span>
                        Cargar gasto
   
                    </button>

                    <button type="button" class="btn btn-outline-subtle rounded-pill d-flex align-items-center gap-2 px-4 py-2 fw-bold" data-bs-toggle="modal" data-bs-target="#modalIngreso" onclick="limpiarModalIngreso()">
                        <span class="material-symbols-outlined fs-5">payments</span>
                        Cargar ingreso
   
                    </button>
                </div>
            </section>
            <!-- Metrics Grid (Bento) -->
            <section class="row g-3 mb-4">
                <!-- Saldo -->
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm rounded-4 p-4">
                        <div class="d-flex justify-content-between align-items-start mb-4">
                            <span class="text-uppercase small fw-bold text-secondary tracking-wider">Saldo Disponible</span>
                            <div class="icon-box bg-primary-light">
                                <span class="material-symbols-outlined">savings</span>
                            </div>
                        </div>
                        <div>
                            <h2 class="balance-text mb-1" runat="server" id="h2Saldo">$ --</h2>
                            <span class="text-primary small fw-bold text-uppercase">Actualizado hace 2 min</span>
                        </div>
                    </div>
                </div>
                <!-- Ingresos -->
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm rounded-4 p-4">
                        <div class="d-flex justify-content-between align-items-start mb-4">
                            <span class="text-uppercase small fw-bold text-secondary tracking-wider">Ingresos del mes</span>
                            <div class="icon-box bg-success-light">
                                <span class="material-symbols-outlined">trending_up</span>
                            </div>
                        </div>
                        <div>
                            <h2 class="balance-text text-success mb-1" runat="server" id="h2Ingresos">$ --</h2>
                            <div class="d-flex align-items-center gap-1 text-success small fw-bold">
                                <span class="material-symbols-outlined fs-6">arrow_upward</span>
                                <span>12% MÁS QUE EL MES PASADO</span>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Gastos -->
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm rounded-4 p-4">
                        <div class="d-flex justify-content-between align-items-start mb-4">
                            <span class="text-uppercase small fw-bold text-secondary tracking-wider">Gastos del mes</span>
                            <div class="icon-box bg-danger-light">
                                <span class="material-symbols-outlined">trending_down</span>
                            </div>
                        </div>
                        <div>
                            <h2 class="balance-text text-danger mb-3" runat="server" id="h2Gasto">$ --</h2>
                            <div class="progress mb-2">
                                <div class="progress-bar bg-danger" style="width: 68%"></div>
                            </div>
                            <span class="text-secondary small fw-medium">68% de tu límite mensual</span>
                        </div>
                    </div>
                </div>
            </section>
            <div class="row g-4">
                <!-- Left Column: Transactions -->
                <div class="col-lg-8">
                    <div class="card border-0 shadow-sm rounded-4 overflow-hidden h-100">
                        <div class="card-header bg-white border-bottom p-4 d-flex justify-content-between align-items-center">
                            <h3 class="h5 mb-0 fw-bold">Movimientos Recientes</h3>
                            <a class="text-primary text-decoration-none small fw-bold" href="Movimientos.aspx">Ver todo</a>
                        </div>
                        <div class="card-body p-0">
                            <div class="table-responsive-xl">
                                <table class="table table-borderless table-hover align-middle mb-0">
                                    <thead class="bg-light">
                                        <tr>
                                            <th class="ps-4 py-3 text-uppercase small text-secondary fw-bold">Concepto</th>
                                            <th class="py-3 text-uppercase small text-secondary fw-bold">Fecha</th>
                                            <th class="py-3 text-uppercase small text-secondary fw-bold">Categoría</th>
                                            <th class="pe-4 py-3 text-uppercase small text-secondary fw-bold text-end">Monto</th>
                                            <th class="pe-1 py-3 text-uppercase small text-secondary fw-bold text-end"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptMovimientos" runat="server" OnItemCommand="rptMovimientos_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="align-middle border-bottom">
                                                    <td class="ps-4 py-3">
                                                        <div class="d-flex align-items-center gap-3">

                                                            <div class='<%# Eval("Tipo").ToString() == "Gasto" ? "icon-box bg-danger-light" : "icon-box bg-success-light" %>' style="width: 40px; height: 40px;">
                                                                <span class="material-symbols-outlined fs-5">
                                                                    <%# Eval("Tipo").ToString() == "Gasto" ? "shopping_bag" : "payments" %>
                                                                </span>
                                                            </div>

                                                            <span class="fw-bold"><%# Eval("Descripcion") %></span>
                                                        </div>
                                                    </td>

                                                    <td class="text-secondary small">
                                                        <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                                                    </td>

                                                    <td>
                                                        <span class="badge bg-light text-secondary rounded-pill px-3 py-2 text-uppercase" style="font-size: 10px;">
                                                            <%# Eval("Categoria") %>
                                                        </span>
                                                    </td>

                                                    <td class='<%# Eval("Tipo").ToString() == "Gasto" ? "pe-4 text-end fw-bold text-danger" : "pe-4 text-end fw-bold text-success" %>'>
                                                        <%# Eval("MontoMostrado") %>
                                                    </td>
                                                    <td class="pe-4 align-middle text-end" style="width: 50px;">
                                                        <div class="dropdown">
                                                            <button class="btn btn-link p-0 text-decoration-none btn-kebab"
                                                                style="display: inline-flex; align-items: center; justify-content: center;"
                                                                type="button" data-bs-toggle="dropdown" aria-expanded="false">

                                                                <span class="material-symbols-outlined" style="font-size: 22px; line-height: 1;">more_vert</span>
                                                            </button>

                                                            <ul class="dropdown-menu dropdown-menu-end shadow-sm border-0">
                                                                <li>
                                                                    <asp:LinkButton ID="btnEditar" runat="server" CssClass="dropdown-item small"
                                                                        CommandName="Editar" CommandArgument='<%# Eval("Tipo") + "|" + Eval("IdReferencia") %>'>
                                                                        ✏️ Editar
                                                                    </asp:LinkButton>
                                                                </li>
                                                                <li>
                                                                    <asp:LinkButton ID="btnEliminar" runat="server" CssClass="dropdown-item small text-danger"
                                                                        CommandName="Eliminar" CommandArgument='<%# Eval("Tipo") + "|" + Eval("IdReferencia") %>'
                                                                        OnClientClick="return confirm('¿Estás seguro de que querés borrar este movimiento?');">
                                                                        🗑️ Eliminar
                                                                    </asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- COLUMNA DERECHA: Insights -->
                <div class="col-lg-4 d-flex flex-column gap-4">
                    <!-- Category Analysis -->
                    <asp:UpdatePanel ID="upGraficoTorta" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="card border-0 shadow-sm rounded-4 p-4">
                                <div class="mb-4">
                                    <span class="text-uppercase small fw-bold text-secondary tracking-widest" style="font-size: 10px;">Distribución de Gastos</span>
                                    <h4 class="h5 mt-1 fw-bold">Análisis por Categoría</h4>
                                </div>

                                <div class="d-flex flex-column align-items-center gap-4">
                                    <div class="position-relative" style="width: 160px; height: 160px;">
                                        <svg class="w-100 h-100" style="transform: rotate(-90deg);" viewBox="0 0 36 36">
                                            <circle cx="18" cy="18" fill="transparent" r="15.9" stroke="#e9ecef" stroke-width="4"></circle>

                                            <asp:Repeater ID="rptGraficoTorta" runat="server">
                                                <ItemTemplate>
                                                    <circle cx="18" cy="18" fill="transparent" r="15.9"
                                                        stroke='<%# Eval("ColorHex") %>'
                                                        stroke-dasharray='<%# Eval("PorcentajeStr") + " 100" %>'
                                                        stroke-dashoffset='<%# Eval("OffsetStr") %>'
                                                        stroke-width="4">
                                                    </circle>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </svg>

                                        <div class="position-absolute top-50 start-50 translate-middle text-center">
                                            <span class="d-block h5 fw-bold mb-0">
                                                <asp:Literal ID="litTotalGrafico" runat="server"></asp:Literal>
                                            </span>
                                            <span class="text-secondary small text-uppercase" style="font-size: 10px;">Total</span>
                                        </div>
                                    </div>

                                    <div class="row w-100 g-2">
                                        <asp:Repeater ID="rptLeyendaGrafico" runat="server">
                                            <ItemTemplate>
                                                <div class="col-6 d-flex align-items-center gap-2">
                                                    <div class="rounded-circle" style='<%# "width: 10px; height: 10px; background-color: " + Eval("ColorHex") + ";" %>'></div>
                                                    <span class="small text-secondary fw-medium"><%# Eval("Nombre") %></span>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!-- Metas de ahorro -->
                    <div class="card border-0 shadow-sm rounded-4 p-4">
                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <h4 class="h5 mb-0 fw-bold">Metas de Ahorro</h4>
                            <a href="#" class="text-decoration-none small text-primary fw-semibold">Ver detalles</a>
                        </div>

                        <asp:Panel ID="pnlMetasVacias" runat="server" Visible="false">
                            <div class="text-center p-4 border rounded-3 bg-light" style="border-style: dashed !important;">
                                <span class="material-symbols-outlined text-secondary mb-2" style="font-size: 32px;">savings</span>
                                <p class="text-muted small mb-3">Aún no tenés metas definidas. ¡Ponete un objetivo!</p>
                                <asp:Button ID="btnCrearMeta" runat="server" Text="+ Crear meta" CssClass="btn btn-sm btn-outline-primary rounded-pill" OnClientClick="abrirModalMeta(); return false;" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlMetasActivas" runat="server">
                            <div class="d-flex flex-column gap-4">

                                <asp:Repeater ID="rptMetasDashboard" runat="server">
                                    <ItemTemplate>
                                        <div>
                                            <div class="d-flex justify-content-between mb-2">
                                                <span class="fw-semibold text-secondary small"><%# Eval("Nombre") %></span>
                                                <span class="fw-bold text-primary small"><%# Eval("Porcentaje") %>%</span>
                                            </div>
                                            <div class="progress">
                                                <div class="progress-bar bg-primary"
                                                    style='<%# "width: " + Eval("Porcentaje") + "%" %>'
                                                    aria-valuenow='<%# Eval("Porcentaje") %>'
                                                    aria-valuemin="0"
                                                    aria-valuemax="100">
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </asp:Panel>
                    </div>
                      <!-- Presupuesto -->
                      <div class="card border-0 shadow-sm rounded-4 p-4">
                          <div class="d-flex justify-content-between align-items-center mb-2">
                              <div>
                                  <span class="text-uppercase small fw-bold text-secondary" style="font-size:
  10px;">Mensual</span>
                                  <h4 class="h5 mt-1 fw-bold mb-0">Presupuesto</h4>
                              </div>
                              <span class="material-symbols-outlined text-primary">account_balance_wallet</span>
                          </div>
                          <p class="text-muted small mb-3">Controlá cuánto querés gastar por categoría cada mes.</p>
                          <a href="Presupuesto.aspx" class="btn btn-sm btn-outline-primary rounded-pill w-100">Ver
  presupuesto</a>
                      </div>
                    <%--GASTOS POR INTEGRANTE DEL HOGAR--%>
                    <asp:Panel ID="pnlLinkGastosIntegrante" runat="server" Visible="false">
                        <div class="card border-0 shadow-sm rounded-4 p-4">
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <div>
                                    <span class="text-uppercase small fw-bold text-secondary" style="font-size: 10px;">Hogar</span>
                                    <h4 class="h5 mt-1 fw-bold mb-0">Gastos por integrante</h4>
                                </div>
                                <span class="material-symbols-outlined text-primary">group</span>
                            </div>
                            <p class="text-muted small mb-2">Mirá cuánto gastó cada integrante del hogar por categoría.</p>

                            <asp:Repeater ID="rptIntegrantes" runat="server">
                                <ItemTemplate>
                                    <div class="d-flex align-items-center gap-2 mb-1">
                                        <span class="material-symbols-outlined text-muted" style="font-size: 16px;">person</span>
                                        <span class="small"><%# Eval("Usuario.Nombre") %> <%# Eval("Usuario.Apellido") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <div class="d-flex gap-2 mt-3">
      <a href="GastoIntegranteHogar.aspx" class="btn btn-sm btn-outline-primary rounded-pill w-50">Ver gastos</a>
      <button type="button" class="btn btn-sm btn-outline-secondary rounded-pill w-50"
              data-bs-toggle="modal" data-bs-target="#modalIntegrante"
              onclick="limpiarModalAgregarIntegrante()">
          + Agregar integrante
      </button>
  </div>
                        </div>

                    </asp:Panel>
                     
                </div>
            </div>
        </main>
        <!-- Bottom Nav (Mobile Only) -->
        <%--<div class="bottom-nav d-md-none">
            <div class="container">
                <div class="row text-center">
                    <div class="col">
                        <a class="nav-link-custom active" href="#">
                            <span class="material-symbols-outlined">grid_view</span>
                            <span>Inicio</span>
                        </a>
                    </div>
                    <div class="col">
                        <a class="nav-link-custom" href="#">
                            <span class="material-symbols-outlined">account_balance_wallet</span>
                            <span>Ahorros</span>
                        </a>
                    </div>
                    <div class="col">
                        <a class="nav-link-custom" href="#">
                            <span class="material-symbols-outlined">payments</span>
                            <span>Deudores</span>
                        </a>
                    </div>
                    <div class="col">
                        <a class="nav-link-custom" href="#">
                            <span class="material-symbols-outlined">person</span>
                            <span>Perfil</span>
                        </a>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>

    <%--<div class="inicio-page">

        <div class="welcome-card">
            <asp:Panel ID="pnlInicioPersonal" runat="server">
                <div class="row align-items-center g-4">
                    <div class="col-lg-8">
                        <div class="d-flex align-items-center gap-3 mb-3">
                            <div class="welcome-icon">
                                <span class="material-symbols-outlined text-primary">account_balance_wallet</span>
                            </div>
                            <div>
                                <h1 class="page-title">
                                    <asp:Label ID="lblBienvenidaPersonal" runat="server" Text="Bienvenido/a"></asp:Label>
                                </h1>
                                <p class="page-subtitle"></p>
                            </div>
                        </div>

                        <div class="d-flex flex-wrap gap-2">
                            <div class="d-flex flex-wrap gap-2">
                               <!-- CARGAR GASTO -->
                                <button type="button" class="btn btn-primary px-4 fw-semibold" data-bs-toggle="modal" data-bs-target="#modalGasto" onclick="limpiarModalGasto()">
                                    Cargar gasto
                                </button>
                               <!-- CARGAR INGRESO -->
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
                                <div class="progress-bar bg-primary" style="width: 0%"></div>
                            </div>
                            <p class="sidebar-note">Acá vas a poder visualizar tu avance mensual una vez que cargues movimientos.</p>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlInicioHogar" runat="server" Visible="false">
                <div class="row align-items-center g-4">
                    <div class="col-lg-8">
                         <div class="d-flex align-items-center gap-3 mb-4">
                            <div class="welcome-icon">
                                <span class="material-symbols-outlined text-primary">home</span>
                            </div>
                            <div>
                                <h1 class="page-title mb-0">
                                    <asp:Label ID="lblBienvenidaHogar" runat="server" Text="Bienvenido/a"></asp:Label>
                                </h1>
                                <p class="page-subtitle mb-0"></p>
                            </div>
                          </div>

                        <div class="row mb-4">
                            <div class="col-md-6 col-xl-5">
                                <div class="summary-card h-100">
                                    <div class="summary-value">
                                        <asp:Label ID="lblGastosMesHogar" runat="server" Text="--"></asp:Label>
                                    </div>
                                    <p class="summary-foot">Total consumido por todos</p>
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-wrap gap-2">
                            <div class="d-flex flex-wrap gap-2">
                                <button type="button" class="btn btn-primary px-4 fw-semibold" data-bs-toggle="modal" data-bs-target="#modalIntegrante" onclick="limpiarModalAgregarIntegrante()">
                                    Añadir miembro
           
                                </button>
                                <!--  AGREGAR GASTO -->
                                <button type="button" class="btn btn-primary px-4 fw-semibold" data-bs-toggle="modal" data-bs-target="#modalGasto" onclick="limpiarModalGasto()">
                                    Cargar gasto

                                </button>
                                <!-- gastos por integrante -->
                               <!-- <a href="GastoIntegranteHogar.aspx" class="btn btn-outline-primary px-4 fw-semibold">Ver gastos por integrante
                                </a>-->
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
                                <div class="progress-bar bg-primary" style="width: 0%"></div>
                            </div>
                            <p class="sidebar-note">Acá vas a poder visualizar tu avance mensual una vez que cargues movimientos.</p>
                        </div>
                    </div>
                </div>
            </asp:Panel>
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

        <asp:UpdatePanel ID="upReportes" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlReportes" runat="server" Visible="false">
                    <div class="row g-4 mb-4">
                        <div class="col-12">
                            <div class="panel-card p-4">
                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <h3 class="section-title mb-0">Reporte de ingresos por mes</h3>
                                    <div class="d-flex gap-2 align-items-center">
                                        <div class="me-2">
                                            <label class="form-label mb-0 small">Mes</label>
                                            <asp:DropDownList ID="ddlMesIngresos" runat="server" CssClass="form-select"></asp:DropDownList>
                                        </div>
                                        <div class="me-2">
                                            <label class="form-label mb-0 small">Año</label>
                                            <asp:DropDownList ID="ddlAnioIngresos" runat="server" CssClass="form-select"></asp:DropDownList>
                                        </div>
                                        <div class="align-self-end">
                                            <asp:Button ID="btnMostrarIngresos" runat="server" Text="Mostrar" CssClass="btn btn-primary" OnClick="btnMostrarIngresos_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="table-responsive mt-3">
                                    <asp:GridView ID="gvIngresosMes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        EmptyDataText="No hay ingresos para el mes seleccionado.">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                            <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />
                                            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C2}" HtmlEncode="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Bloque reporte de gastos -->
                    <div class="row g-4 mb-4">
                        <div class="col-12">
                            <div class="panel-card p-4">
                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <h3 class="section-title mb-0">Reporte de gastos por mes</h3>
                                    <div class="d-flex gap-2 align-items-center">
                                        <div class="me-2">
                                            <label class="form-label mb-0 small">Mes</label>
                                            <asp:DropDownList ID="ddlMesGastos" runat="server" CssClass="form-select"></asp:DropDownList>
                                        </div>
                                        <div class="me-2">
                                            <label class="form-label mb-0 small">Año</label>
                                            <asp:DropDownList ID="ddlAnioGastos" runat="server" CssClass="form-select"></asp:DropDownList>
                                        </div>
                                        <div class="align-self-end">
                                            <asp:Button ID="btnMostrarGastos" runat="server" Text="Mostrar" CssClass="btn btn-primary" OnClick="btnMostrarGastos_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="table-responsive mt-3">
                                    <asp:GridView ID="gvGastosMes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        EmptyDataText="No hay gastos para el mes seleccionado.">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                            <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                                            <asp:BoundField DataField="MedioPago" HeaderText="Medio de pago" />
                                            <asp:BoundField DataField="Monto" HeaderText="Monto (ARS)" DataFormatString="{0:C2}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

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
                                <div class="d-flex justify-content-between align-items-center mb-3 px-4">
                                      <!-- FLECHAS -->

    <asp:Button ID="btnMesAnterior"
        runat="server"
        Text="←"
        CssClass="btn btn-light border rounded-3"
        OnClick="btnMesAnterior_Click" />

    <asp:Label ID="lblMesActual"
        runat="server"
        CssClass="fw-bold fs-5 text-capitalize" />

    <asp:Button ID="btnMesSiguiente"
        runat="server"
        Text="→"
        CssClass="btn btn-light border rounded-3"
        OnClick="btnMesSiguiente_Click" />

</div>

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

                            <asp:LinkButton ID="lnkVerMetricas" runat="server" CssClass="quick-link text-decoration-none" OnClick="lnkVerMetricas_Click">
                                <div class="quick-item">
                                    <div class="quick-icon">
                                        <span class="material-symbols-outlined">monitoring</span>
                                    </div>
                                    <div class="fw-bold mb-1">Ver métricas</div>
                                    <div class="text-muted small">Consultá gráficos y reportes</div>
                                </div>
                            </asp:LinkButton>
                        </div>
                    </div>
                  

                </div>
            </div>
        </div>

    </div>--%>
   
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ModalsContent" runat="server">
        <%-- MODAL PARA AGREGAR INTEGRANTE AL HOGAR --%>
    <div class="modal fade" id="modalIntegrante" tabindex="-1" aria-labelledby="modalIntegranteLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content border-0 rounded-4 shadow">
                <div class="modal-header border-0 pb-0">
                    <h5 class="modal-title fw-bold" id="modalIntegranteLabel">Agregar Integrante</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="modal-body pt-3">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Mail</label>
                        <asp:TextBox ID="txtMailIntegrante" TextMode="Email" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="modal-footer border-0 pt-0">
                    <button type="button" class="btn btn-light border rounded-3" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarIntegrante" runat="server" Text="Guardar" CssClass="btn btn-primary rounded-3 px-4" OnClick="btnGuardarIntegrante_Click" />
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
                txtMontoPesos.readOnly = true;
                txtMontoPesos.value = '';


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

        function limpiarModalAgregarIntegrante() {
            document.getElementById('<%= txtMailIntegrante.ClientID %>').value = '';
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
        function toggleFormaPagoGasto() {
            var cuotas = document.getElementById('<%= rbCuotas.ClientID %>').checked;

            document.getElementById('campoMontoPesosGasto').style.display = cuotas ? 'none' : 'block';
            document.getElementById('campoMontoCuotaGasto').style.display = cuotas ? 'block' : 'none';
            document.getElementById('campoCantidadCuotasGasto').style.display = cuotas ? 'block' : 'none';
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