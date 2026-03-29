<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarContrasenia.aspx.cs" Inherits="TPFinalIntegrador.ModificarContrasenia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4" style="max-width: 480px;">
    <h5 class="fw-bold mb-4">Modificar Contraseña</h5>

    <asp:Label ID="lblMensaje" runat="server" CssClass="d-none" />

    <div class="mb-3">
        <label class="form-label text-secondary text-uppercase fw-bold" style="font-size: 0.65rem; letter-spacing: 0.05rem;">Contraseña Actual</label>
        <asp:TextBox ID="txtContraseniaActual" runat="server" CssClass="form-control" TextMode="Password" />
    </div>

    <div class="mb-3">
        <label class="form-label text-secondary text-uppercase fw-bold" style="font-size: 0.65rem; letter-spacing: 0.05rem;">Nueva Contraseña</label>
        <asp:TextBox ID="txtContraseniaNueva" runat="server" CssClass="form-control" TextMode="Password" />
    </div>

    <div class="mb-4">
        <label class="form-label text-secondary text-uppercase fw-bold" style="font-size: 0.65rem; letter-spacing: 0.05rem;">Confirmar Nueva Contraseña</label>
        <asp:TextBox ID="txtConfirmarContrasenia" runat="server" CssClass="form-control" TextMode="Password" />
    </div>

    <div class="d-flex gap-2">
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
    </div>
</div>
</asp:Content>
