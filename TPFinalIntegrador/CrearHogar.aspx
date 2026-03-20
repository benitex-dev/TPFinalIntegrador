<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearHogar.aspx.cs" Inherits="TPFinalIntegrador.CrearHogar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2>Crear Hogar</h2>

        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-3"></asp:Label>

        <div class="mb-3">
            <label class="form-label fw-semibold">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label class="form-label fw-semibold">Nombre Usuario</label>
            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" />
        </div>
        <div class="mb-3">
            <label class="form-label fw-semibold">Apellido Usuario</label>
            <asp:TextBox ID="txtApellidoUsuario" runat="server" CssClass="form-control" />
        </div>
        <div class="mb-3">
            <label class="form-label fw-semibold">Email Usuario</label>
            <asp:TextBox ID="txtEmailUsuario" runat="server" CssClass="form-control" />
        </div>
        <div class="mb-3 form-check">
            <asp:CheckBox ID="chkEstado" runat="server" CssClass="form-check-input" Checked="true" />
            <label class="form-check-label" for="chkEstado">Activo</label>
        </div>

        <div class="d-flex gap-2">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClientClick="history.back(); return false;" />
        </div>
    </div>
</asp:Content>
