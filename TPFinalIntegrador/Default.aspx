<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPFinalIntegrador._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

      <main>
          <!-- Hero Section -->
          <section class="py-5">
              <div class="container py-lg-5">
                  <div class="row align-items-center g-5">
                      <div class="col-lg-6">
                          <div class="d-inline-flex align-items-center gap-2 px-3 py-1 rounded-pill bg-primary
  bg-opacity-10 border border-primary border-opacity-25 text-primary small fw-bold mb-4">
                              <span class="bg-primary rounded-circle" style="width: 6px; height: 6px;"></span>
                              GESTIÓN DE FINANZAS PERSONALES Y GRUPALES
                          </div>
                          <h1 class="display-3 fw-bold mb-4">
                              Tomá el control de tus <span class="text-primary">finanzas</span>
                          </h1>
                          <p class="lead text-secondary mb-5">
                              Registrá tus gastos e ingresos, definí presupuestos, creá metas de ahorro y gestioná
  dinero prestado, todo en un solo lugar.
                          </p>
                          <div class="d-flex flex-wrap gap-3 mb-5">
                              <a href="Login.aspx" class="btn btn-primary btn-lg rounded-pill px-4">Iniciar Sesión</a>
                              <a href="NuevoUsuario.aspx" class="btn btn-outline-primary btn-lg rounded-pill
  px-4">Registrarse</a>
                          </div>
                          <div class="d-flex align-items-center gap-3">
                              <div class="avatar-group d-flex">
                                  <div class="avatar rounded-circle bg-primary text-white d-flex align-items-center
  justify-content-center fw-bold" style="font-size:13px;">JP</div>
                                  <div class="avatar rounded-circle bg-success text-white d-flex align-items-center
  justify-content-center fw-bold" style="font-size:13px;">ML</div>
                                  <div class="avatar rounded-circle bg-warning text-white d-flex align-items-center
  justify-content-center fw-bold" style="font-size:13px;">AR</div>
                              </div>
                              <p class="small text-secondary m-0">
                                  Usada por estudiantes y familias para organizar sus finanzas.
                              </p>
                          </div>
                      </div>
                      <div class="col-lg-6">
                          <div class="card shadow-lg border-secondary border-opacity-25 overflow-hidden">
                              <div class="card-header bg-dark py-2 px-3 d-flex gap-1">
                                  <div class="rounded-circle bg-danger" style="width: 10px; height: 10px;"></div>
                                  <div class="rounded-circle bg-warning" style="width: 10px; height: 10px;"></div>
                                  <div class="rounded-circle bg-success" style="width: 10px; height: 10px;"></div>
                              </div>
                              <div class="card-body bg-body-tertiary p-4">
                                  <div class="mb-3">
                                      <div class="d-flex justify-content-between mb-1">
                                          <span class="small fw-semibold">Alimentación</span>
                                          <span class="small text-muted">75%</span>
                                      </div>
                                      <div class="progress rounded-pill" style="height:8px">
                                          <div class="progress-bar bg-warning" style="width:75%"></div>
                                      </div>
                                  </div>
                                  <div class="mb-3">
                                      <div class="d-flex justify-content-between mb-1">
                                          <span class="small fw-semibold">Transporte</span>
                                          <span class="small text-muted">40%</span>
                                      </div>
                                      <div class="progress rounded-pill" style="height:8px">
                                          <div class="progress-bar bg-success" style="width:40%"></div>
                                      </div>
                                  </div>
                                  <div class="mb-3">
                                      <div class="d-flex justify-content-between mb-1">
                                          <span class="small fw-semibold">Entretenimiento</span>
                                          <span class="small text-danger">105%</span>
                                      </div>
                                      <div class="progress rounded-pill" style="height:8px">
                                          <div class="progress-bar bg-danger" style="width:100%"></div>
                                      </div>
                                  </div>
                                  <div class="mt-4 p-3 bg-primary bg-opacity-10 rounded-3 d-flex justify-content-between
   align-items-center">
                                      <span class="small fw-semibold text-primary">Meta de ahorro — Vacaciones</span>
                                      <span class="badge bg-primary rounded-pill">62%</span>
                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>
              </div>
          </section>

          <!-- Features Section -->
          <section class="py-5 bg-body-secondary bg-opacity-50" id="features">
              <div class="container py-5">
                  <div class="text-center mb-5">
                      <h6 class="text-primary fw-bold text-uppercase mb-3">Funcionalidades</h6>
                      <h2 class="display-5 fw-bold mb-3">Todo lo que necesitás para organizar tu dinero</h2>
                      <p class="text-secondary mx-auto lead" style="max-width: 700px;">
                          Herramientas simples y efectivas para llevar el control de tus finanzas personales y grupales.
                      </p>
                  </div>
                  <div class="row g-4">
                      <div class="col-md-6 col-lg-3">
                          <div class="card h-100 border-0 bg-body shadow-sm">
                              <div class="card-body p-4 text-center text-lg-start">
                                  <div class="d-inline-flex align-items-center justify-content-center bg-primary
  bg-opacity-10 text-primary rounded p-3 mb-4">
                                      <span class="material-symbols-outlined fs-3">payments</span>
                                  </div>
                                  <h4 class="h5 fw-bold mb-3">Gastos e Ingresos</h4>
                                  <p class="text-secondary small mb-0">Registrá tus movimientos con categoría, medio de
  pago y fecha. Soporte para cuotas y moneda extranjera.</p>
                              </div>
                          </div>
                      </div>
                      <div class="col-md-6 col-lg-3">
                          <div class="card h-100 border-0 bg-body shadow-sm">
                              <div class="card-body p-4 text-center text-lg-start">
                                  <div class="d-inline-flex align-items-center justify-content-center bg-primary
  bg-opacity-10 text-primary rounded p-3 mb-4">
                                      <span class="material-symbols-outlined fs-3">savings</span>
                                  </div>
                                  <h4 class="h5 fw-bold mb-3">Metas de Ahorro</h4>
                                  <p class="text-secondary small mb-0">Creá metas con un objetivo y fecha límite.
  Registrá aportes y seguí tu progreso con una barra visual.</p>
                              </div>
                          </div>
                      </div>
                      <div class="col-md-6 col-lg-3">
                          <div class="card h-100 border-0 bg-body shadow-sm">
                              <div class="card-body p-4 text-center text-lg-start">
                                  <div class="d-inline-flex align-items-center justify-content-center bg-primary
  bg-opacity-10 text-primary rounded p-3 mb-4">
                                      <span class="material-symbols-outlined fs-3">bar_chart</span>
                                  </div>
                                  <h4 class="h5 fw-bold mb-3">Presupuestos</h4>
                                  <p class="text-secondary small mb-0">Asigná un presupuesto mensual por categoría y
  visualizá cuánto llevás gastado en tiempo real.</p>
                              </div>
                          </div>
                      </div>
                      <div class="col-md-6 col-lg-3">
                          <div class="card h-100 border-0 bg-body shadow-sm">
                              <div class="card-body p-4 text-center text-lg-start">
                                  <div class="d-inline-flex align-items-center justify-content-center bg-primary
  bg-opacity-10 text-primary rounded p-3 mb-4">
                                      <span class="material-symbols-outlined fs-3">handshake</span>
                                  </div>
                                  <h4 class="h5 fw-bold mb-3">Dinero Prestado</h4>
                                  <p class="text-secondary small mb-0">Registrá préstamos a otras personas, controlá
  cuotas y llevá el saldo pendiente actualizado.</p>
                              </div>
                          </div>
                      </div>
                  </div>
              </div>
          </section>

          <!-- Trust Section -->
          <section class="py-5 border-top border-bottom bg-body" id="trust">
              <div class="container py-4">
                  <div class="row row-cols-1 row-cols-md-3 g-4 align-items-center text-center">
                      <div class="col">
                          <div class="d-flex align-items-center justify-content-center gap-3">
                              <span class="material-symbols-outlined text-primary fs-2">group</span>
                              <div class="text-start">
                                  <div class="fw-bold small">Vista Hogar</div>
                                  <div class="small text-secondary text-uppercase fw-bold" style="font-size:
  0.7rem;">Finanzas compartidas</div>
                              </div>
                          </div>
                      </div>
                      <div class="col border-start border-end border-secondary border-opacity-10">
                          <div class="d-flex align-items-center justify-content-center gap-3">
                              <span class="material-symbols-outlined text-primary fs-2">tune</span>
                              <div class="text-start">
                                  <div class="fw-bold small">Personalizable</div>
                                  <div class="small text-secondary text-uppercase fw-bold" style="font-size:
  0.7rem;">Categorías y medios de pago</div>
                              </div>
                          </div>
                      </div>
                      <div class="col">
                          <div class="d-flex align-items-center justify-content-center gap-3">
                              <span class="material-symbols-outlined text-primary fs-2">monitoring</span>
                              <div class="text-start">
                                  <div class="fw-bold small">Reportes visuales</div>
                                  <div class="small text-secondary text-uppercase fw-bold" style="font-size:
  0.7rem;">Gráficos por categoría</div>
                              </div>
                          </div>
                      </div>
                  </div>
              </div>
          </section>
      </main>

  </asp:Content>
