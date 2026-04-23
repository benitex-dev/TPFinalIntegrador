 <%@ Page Title="Iniciar Sesión" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
  Inherits="TPFinalIntegrador.Login" %>

  <!DOCTYPE html>
  <html lang="es">
  <head runat="server">
      <meta charset="utf-8" />
      <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      <title>Iniciar Sesión — GastApp</title>
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
      <link
  href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap"
  rel="stylesheet" />
      <link
  href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&family=Manrope:wght@700;800&display=swap"
  rel="stylesheet" />
      <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
      <style>
          body { font-family: 'Inter', sans-serif; background-color: #f8f9fa; }
          .login-wrapper {
              min-height: 100vh;
              display: flex;
              align-items: center;
              justify-content: center;
          }
          .login-card {
              width: 100%;
              max-width: 460px;
              border: 1px solid rgba(0,0,0,.06);
              border-radius: 24px;
              box-shadow: 0 10px 30px rgba(0,0,0,.06);
              background-color: #fff;
          }
          .login-icon {
              width: 64px;
              height: 64px;
              border-radius: 18px;
              display: flex;
              align-items: center;
              justify-content: center;
              background: rgba(13,110,253,.10);
              margin: 0 auto 1rem auto;
          }
          .login-icon .material-symbols-outlined { font-size: 32px; }
          .form-control { border-radius: 14px; padding: .85rem 1rem; }
          .btn-login { border-radius: 14px; padding: .85rem 1rem; font-weight: 600; }
          .login-muted { color: #6c757d; font-size: .95rem; }
      </style>
  </head>
  <body>
      <form runat="server">
          <asp:ScriptManager runat="server" />
          <div class="login-wrapper py-5">
              <div class="login-card p-4 p-md-5">

                  <div class="text-center mb-4">
                      <div class="login-icon">
                          <span class="material-symbols-outlined text-primary">account_balance_wallet</span>
                      </div>
                      <h1 class="h3 fw-bold mb-2">Iniciar sesión</h1>
                      <p class="login-muted mb-0">Ingresá a tu cuenta para administrar tus gastos e ingresos.</p>
                  </div>

                  <div class="mb-3">
                      <label class="form-label fw-semibold">Email</label>
                      <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"
  placeholder="nombre@correo.com"></asp:TextBox>
                  </div>

                  <div class="mb-3">
                      <label class="form-label fw-semibold">Contraseña</label>
                      <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"
  placeholder="Ingresá tu contraseña"></asp:TextBox>
                  </div>

                  <div class="mt-2">
                      <a href="RecuperarContrasenia.aspx" class="text-decoration-none small">¿Olvidaste tu
  contraseña?</a>
                  </div>

                  <div class="d-grid mt-4">
                      <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-login"
  OnClick="btnIngresar_Click" />
                  </div>

                  <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block text-center mt-3
  fw-semibold"></asp:Label>

                  <div class="text-center mt-4">
                      <span class="login-muted">¿Todavía no tenés cuenta?</span>
                      <a href="NuevoUsuario.aspx" class="text-decoration-none fw-semibold ms-1">Registrate</a>
                  </div>

              </div>
          </div>
      </form>
      <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
      <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
  </body>
  </html>