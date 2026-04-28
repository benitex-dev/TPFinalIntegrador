 <%@ Page Title="Nuevo Usuario" Language="C#" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs"
  Inherits="TPFinalIntegrador.NuevoUsuario" %>

  <!DOCTYPE html>
  <html lang="es">
  <head runat="server">
      <meta charset="utf-8" />
      <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      <title>Crear cuenta — GastApp</title>
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
      <link
  href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap"
  rel="stylesheet" />
      <link
  href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&family=Manrope:wght@700;800&display=swap"
  rel="stylesheet" />
      <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
      <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
      <style>
          body { font-family: 'Inter', sans-serif; background-color: #f8f9fa; }
          .auth-wrapper { min-height: 100vh; display: flex; align-items: center; justify-content: center; }
          .auth-card {
              width: 100%; max-width: 820px;
              border: 1px solid rgba(0,0,0,.06);
              border-radius: 24px;
              box-shadow: 0 10px 30px rgba(0,0,0,.06);
              background-color: #fff;
          }
          .auth-icon {
              width: 64px; height: 64px; border-radius: 18px;
              display: flex; align-items: center; justify-content: center;
              background: rgba(13,110,253,.10); margin: 0 auto 1rem auto;
          }
          .auth-icon .material-symbols-outlined { font-size: 32px; }
          .form-control { border-radius: 14px; padding: .85rem 1rem; }
          .btn-auth { border-radius: 14px; padding: .85rem 1rem; font-weight: 600; }
          .auth-muted { color: #6c757d; font-size: .95rem; }
          .section-label {
              font-size: .85rem; text-transform: uppercase;
              letter-spacing: .04em; color: #6c757d;
              font-weight: 700; margin-bottom: 1rem;
          }
      </style>
  </head>
  <body>
      <form runat="server">
          <asp:ScriptManager runat="server" />
          <div class="auth-wrapper py-5">
              <div class="auth-card p-4 p-md-5">

                  <div class="text-center mb-4">
                      <div class="auth-icon">
                          <span class="material-symbols-outlined text-primary">person_add</span>
                      </div>
                      <h1 class="h3 fw-bold mb-2">Crear nuevo usuario</h1>
                      <p class="auth-muted mb-0">Completá los datos para registrarte y comenzar a administrar tus
  gastos.</p>
                  </div>

                  <div class="section-label">Datos personales</div>

                  <div class="row g-3 mb-4">
                      <div class="col-12 col-md-6">
                          <label class="form-label fw-semibold">Nombre</label>
                          <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresá tu
  nombre"></asp:TextBox>
                      </div>
                      <div class="col-12 col-md-6">
                          <label class="form-label fw-semibold">Apellido</label>
                          <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingresá tu
  apellido"></asp:TextBox>
                      </div>
                      <div class="col-12 col-md-6">
                          <label class="form-label fw-semibold">Fecha de nacimiento</label>
                          <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control"
  TextMode="Date"></asp:TextBox>
                      </div>
                      <div class="col-12 col-md-6">
                          <label class="form-label fw-semibold">URL imagen de perfil</label>
                          <asp:TextBox ID="txtImagenURL" runat="server" CssClass="form-control"
  placeholder="https://..."></asp:TextBox>
                      </div>
                  </div>

                  <div class="section-label">Datos de acceso</div>

                  <div class="row g-3">
                      <div class="col-12">
                          <label class="form-label fw-semibold">Email</label>
                          <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"
  placeholder="nombre@correo.com"></asp:TextBox>
                      </div>
                      <div class="col-12 col-md-6">
                          <label class="form-label fw-semibold">Contraseña</label>
                          <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"
  placeholder="Ingresá una contraseña"></asp:TextBox>
                      </div>
                      <div class="col-12 col-md-6">
                          <label class="form-label fw-semibold">Confirmar contraseña</label>
                          <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="form-control"
  TextMode="Password" placeholder="Repetí la contraseña"></asp:TextBox>
                      </div>
                  </div>

                  <asp:Label ID="lblMensaje" runat="server" CssClass="d-block text-center mt-4 fw-semibold"></asp:Label>

                  <div class="d-grid d-md-flex justify-content-md-end gap-2 mt-4">
                      <a href="Login.aspx" class="btn btn-outline-secondary px-4">Cancelar</a>
                      <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear cuenta" CssClass="btn btn-primary
  btn-auth px-4" OnClick="btnCrearUsuario_Click" />
                  </div>

                  <div class="text-center mt-4">
                      <span class="auth-muted">¿Ya tenés una cuenta?</span>
                      <a href="Login.aspx" class="text-decoration-none fw-semibold ms-1">Iniciar sesión</a>
                  </div>

              </div>
          </div>
      </form>
      <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
  </body>
  </html>