<%@ Page Title="Recuperar Contraseña" Language="C#" AutoEventWireup="true" CodeBehind="RecuperarContrasenia.aspx.cs"
  Inherits="TPFinalIntegrador.RecuperarContrasenia" %>

  <!DOCTYPE html>
  <html lang="es">
  <head runat="server">
      <meta charset="utf-8" />
      <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      <title>Recuperar Contraseña — GastApp</title>
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
          .auth-wrapper { min-height: 100vh; display: flex; align-items: center; justify-content: center; }
          .auth-card {
              width: 100%; max-width: 480px;
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
          .auth-muted { color: #6c757d; font-size: .95rem; }
      </style>
  </head>
  <body>
      <form runat="server">
          <asp:ScriptManager runat="server" />
          <div class="auth-wrapper py-5">
              <div class="auth-card p-4 p-md-5">

                  <div class="text-center mb-4">
                      <div class="auth-icon">
                          <span class="material-symbols-outlined text-primary">lock_reset</span>
                      </div>
                      <h1 class="h3 fw-bold mb-2">Recuperar contraseña</h1>
                      <p class="auth-muted mb-0">Ingresá tu email y te enviamos un código para restablecer tu
  contraseña.</p>
                  </div>

                  <div class="mb-3">
                      <label class="form-label fw-semibold">Email</label>
                      <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                          placeholder="email@dominio.com" AutoCompleteType="Email"
                          pattern="[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}"
                          title="Por ejemplo: email@dominio.com"></asp:TextBox>
                  </div>

                  <div class="mb-3">
                      <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="form-label fw-semibold"
  Visible="false" />
                      <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Visible="false" />
                  </div>

                  <div class="mb-3">
                      <asp:Label ID="lblCambiar" runat="server" Text="Nueva Contraseña" CssClass="form-label
  fw-semibold" Visible="false" />
                      <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"
  Visible="false" />
                  </div>

                  <div class="mb-3">
                      <asp:Label ID="lblConfirmar" runat="server" Text="Confirmar Contraseña" CssClass="form-label
  fw-semibold" Visible="false" />
                      <asp:TextBox ID="txtConfirmarPassword" runat="server" TextMode="Password" CssClass="form-control"
  Visible="false" />
                  </div>

                  <asp:Label ID="lblExito" runat="server" Text="¡Cambio de contraseña exitoso!" CssClass="alert
  alert-success d-block mb-3" Visible="false" />
                  <asp:Label ID="lblMensaje" runat="server" CssClass="text-success d-block mb-3" />

                  <div class="d-flex gap-2 flex-wrap mt-2">
                      <asp:Button ID="btnGenerar" runat="server" Text="Generar Código" CssClass="btn btn-primary"
  OnClick="btnGenerar_Click" />
                      <asp:Button ID="btnComprobar" runat="server" Text="Comprobar Código" CssClass="btn btn-primary"
  Visible="false" OnClick="btnComprobar_Click" />
                      <asp:Button ID="btnCambiar" runat="server" Text="Confirmar Contraseña" CssClass="btn btn-primary"
  Visible="false" OnClick="btnCambiar_Click" />
                      <asp:Button ID="btnPerfil" runat="server" Text="Ir a tu Perfil" CssClass="btn btn-success"
  Visible="false" OnClick="btnPerfil_Click" />
                      <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary"
  OnClick="btnCancelar_Click" />
                  </div>

                  <div class="text-center mt-4">
                      <span class="auth-muted">¿Recordaste tu contraseña?</span>
                      <a href="Login.aspx" class="text-decoration-none fw-semibold ms-1">Iniciar sesión</a>
                  </div>

              </div>
          </div>
      </form>
      <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
      <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
  </body>
  </html>