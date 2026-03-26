<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperarContrasenia.aspx.cs" Inherits="TPFinalIntegrador.RecuperarContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-md-6">

            <h3 class="mb-4">Recuperar contraseña</h3>

            <%-- Campo Email --%>
            <div class="mb-3">
                <label class="form-label" for="txtEmail">Email</label>
                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="form-control"
                    pattern="[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}"
                    title="Por ejemplo: email@dominio.com"
                    placeholder="email@dominio.com"
                    AutoCompleteType="Email">
                </asp:TextBox>
            </div>

            <%-- Campo Código --%>
            <div class="mb-3">
                <asp:Label
                    ID="lblCodigo"
                    runat="server"
                    Text="Código"
                    CssClass="form-label"
                    Visible="false" />
                <asp:TextBox
                    ID="txtCodigo"
                    runat="server"
                    CssClass="form-control"
                    Visible="false" />
            </div>

            <%-- Campo Nueva Contraseña --%>
            <div class="mb-3">
                <asp:Label
                    ID="lblCambiar"
                    runat="server"
                    Text="Nueva Contraseña"
                    CssClass="form-label"
                    Visible="false" />
                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    TextMode="Password"
                    CssClass="form-control"
                    pattern="^\S+$"
                    title="¡Por favor, ingrese una contraseña sin espacios en blanco!"
                    Visible="false" />
            </div>
            <%-- Campo Confirmar Contraseña --%>
            <div class="mb-3">
                <asp:Label
                    ID="lblConfirmar"
                    runat="server"
                    Text="Confirmar Contraseña"
                    CssClass="form-label"
                    Visible="false" />
                <asp:TextBox
                    ID="txtConfirmarPassword"
                    runat="server"
                    TextMode="Password"
                    CssClass="form-control"
                    pattern="^\S+$"
                    title="¡Por favor, ingrese una contraseña sin espacios en blanco!"
                    Visible="false" />
            </div>
            <%-- Mensaje de éxito --%>
            <asp:Label
                ID="lblExito"
                runat="server"
                Text="¡Cambio de Password exitoso! Puede seguir navegando!"
                CssClass="alert alert-success d-block mb-3"
                Visible="false" />

            <%-- Mensaje general --%>
            <asp:Label
                ID="lblMensaje"
                runat="server"
                CssClass="text-success d-block mb-3" />

            <%-- Botones --%>
            <div class="d-flex gap-2 flex-wrap mt-2">
                <asp:Button
                    ID="btnGenerar"
                    runat="server"
                    Text="Generar Código"
                    CssClass="btn btn-primary"
                    OnClick="btnGenerar_Click" />
                <asp:Button
                    ID="btnComprobar"
                    runat="server"
                    Text="Comprobar Código"
                    CssClass="btn btn-primary"
                    Visible="false"
                    OnClick="btnComprobar_Click" />
                <asp:Button
                    ID="btnCambiar"
                    runat="server"
                    Text="Confirmar Password"
                    CssClass="btn btn-primary"
                    Visible="false"
                    OnClick="btnCambiar_Click" />
                <asp:Button
                    ID="btnPerfil"
                    runat="server"
                    Text="Ir a tu Perfil"
                    CssClass="btn btn-success"
                    Visible="false"
                    OnClick="btnPerfil_Click" />
                <asp:Button
                    ID="btnCancelar"
                    runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-secondary"
                    OnClick="btnCancelar_Click" />
            </div>

        </div>
    </div>

</asp:Content>
