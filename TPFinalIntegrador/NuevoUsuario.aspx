<%@ Page Title="Nuevo Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" Inherits="TPFinalIntegrador.NuevoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .nuevo-usuario-wrapper {
            min-height: 75vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .nuevo-usuario-card {
            width: 100%;
            max-width: 820px;
            border: 1px solid rgba(0,0,0,.06);
            border-radius: 24px;
            box-shadow: 0 10px 30px rgba(0,0,0,.06);
            background-color: #fff;
        }

        .nuevo-usuario-icon {
            width: 64px;
            height: 64px;
            border-radius: 18px;
            display: flex;
            align-items: center;
            justify-content: center;
            background: rgba(var(--bs-primary-rgb), .10);
            margin: 0 auto 1rem auto;
        }

        .nuevo-usuario-icon .material-symbols-outlined {
            font-size: 32px;
        }

        .form-control {
            border-radius: 14px;
            padding: .85rem 1rem;
        }

        .btn-crear {
            border-radius: 14px;
            padding: .85rem 1rem;
            font-weight: 600;
        }

        .nuevo-usuario-muted {
            color: #6c757d;
            font-size: .95rem;
        }

        .section-label {
            font-size: .85rem;
            text-transform: uppercase;
            letter-spacing: .04em;
            color: #6c757d;
            font-weight: 700;
            margin-bottom: 1rem;
        }
    </style>

    <div class="nuevo-usuario-wrapper py-5">
        <div class="nuevo-usuario-card p-4 p-md-5">
            
            <div class="text-center mb-4">
                <div class="nuevo-usuario-icon">
                    <span class="material-symbols-outlined text-primary">person_add</span>
                </div>
                <h1 class="h3 fw-bold mb-2">Crear nuevo usuario</h1>
                <p class="nuevo-usuario-muted mb-0">
                    Completá los datos para registrarte y comenzar a administrar tus gastos.
                </p>
            </div>

            <div class="section-label">Datos personales</div>

            <div class="row g-3 mb-4">
                <div class="col-12 col-md-6">
                    <label for="txtNombre" class="form-label fw-semibold">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresá tu nombre"></asp:TextBox>
                </div>

                <div class="col-12 col-md-6">
                    <label for="txtApellido" class="form-label fw-semibold">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingresá tu apellido"></asp:TextBox>
                </div>

                <div class="col-12 col-md-6">
                    <label for="txtFechaNac" class="form-label fw-semibold">Fecha de nacimiento</label>
                    <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <div class="col-12 col-md-6">
                    <label for="txtImagenURL" class="form-label fw-semibold">URL imagen de perfil</label>
                    <asp:TextBox ID="txtImagenURL" runat="server" CssClass="form-control" placeholder="https://..."></asp:TextBox>
                </div>
            </div>

            <div class="section-label">Datos de acceso</div>

            <div class="row g-3">
                <div class="col-12">
                    <label for="txtEmail" class="form-label fw-semibold">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@correo.com"></asp:TextBox>
                </div>

                <div class="col-12 col-md-6">
                    <label for="txtPassword" class="form-label fw-semibold">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingresá una contraseña"></asp:TextBox>
                </div>

                <div class="col-12 col-md-6">
                    <label for="txtConfirmarPassword" class="form-label fw-semibold">Confirmar contraseña</label>
                    <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Repetí la contraseña"></asp:TextBox>
                </div>
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="d-block text-center mt-4 fw-semibold"></asp:Label>

            <div class="d-grid d-md-flex justify-content-md-end gap-2 mt-4">
                <a href="Login.aspx" class="btn btn-outline-secondary px-4">Cancelar</a>
                <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear cuenta" CssClass="btn btn-primary btn-crear px-4" OnClick="btnCrearUsuario_Click" />
            </div>

            <div class="text-center mt-4">
                <span class="nuevo-usuario-muted">¿Ya tenés una cuenta?</span>
                <a href="Login.aspx" class="text-decoration-none fw-semibold ms-1">Iniciar sesión</a>
            </div>

        </div>
    </div>

</asp:Content>
