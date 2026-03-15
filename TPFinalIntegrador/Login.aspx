<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPFinalIntegrador.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .login-wrapper {
            min-height: 75vh;
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
            background: rgba(var(--bs-primary-rgb), .10);
            margin: 0 auto 1rem auto;
        }

        .login-icon .material-symbols-outlined {
            font-size: 32px;
        }

        .form-control {
            border-radius: 14px;
            padding: .85rem 1rem;
        }

        .btn-login {
            border-radius: 14px;
            padding: .85rem 1rem;
            font-weight: 600;
        }

        .login-muted {
            color: #6c757d;
            font-size: .95rem;
        }
    </style>

    <div class="login-wrapper py-5">
        <div class="login-card p-4 p-md-5">
            <div class="text-center mb-4">
                <div class="login-icon">
                    <span class="material-symbols-outlined text-primary">account_balance_wallet</span>
                </div>
                <h1 class="h3 fw-bold mb-2">Iniciar sesión</h1>
                <p class="login-muted mb-0">
                    Ingresá a tu cuenta para administrar tus gastos e ingresos.
                </p>
            </div>

            <div class="mb-3">
                <label for="txtEmail" class="form-label fw-semibold">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@correo.com"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label fw-semibold">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingresá tu contraseña"></asp:TextBox>
            </div>

            <div class="d-grid mt-4">
                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-login" OnClick="btnIngresar_Click" />
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block text-center mt-3 fw-semibold"></asp:Label>

            <div class="text-center mt-4">
                <span class="login-muted">¿Todavía no tenés cuenta?</span>
                <a href="#" class="text-decoration-none fw-semibold ms-1">Registrate</a>
            </div>
        </div>
    </div>

</asp:Content>
