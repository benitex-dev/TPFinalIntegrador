<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPFinalIntegrador.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<main class="container py-5">

    <header class="mb-5">
        <h1 class="display-5 fw-bold mb-1">Mi Perfil</h1>
        <p class="text-secondary fs-5">Administrá tu identidad digital y preferencias de seguridad.</p>
    </header>

    <%-- Tarjeta principal de perfil --%>
    <div class="card border rounded-3 overflow-hidden shadow-sm">
          <%-- Mensaje de feedback --%>
  <div class="col-12">
      <asp:Label ID="lblMensaje" runat="server" CssClass="d-block" />
  </div>
        <%-- Banner --%>
        <div class="bg-primary bg-opacity-10 border-bottom position-relative" style="height: 128px;">
            <%-- Avatar --%>
            <div class="position-absolute" style="bottom: -48px; left: 32px;">
                <div class="p-1 bg-white rounded-circle border" style="display: inline-block;">
                    <asp:Image
                        ID="imagenMiPerfil"
                         runat="server"
                        ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                        alt="Avatar de usuario"
                        class="rounded-circle object-fit-cover"
                        style="width: 96px; height: 96px;" />
                </div>
            </div>
            <%-- Badge estado --%>
            <%--<div class="position-absolute bottom-0 end-0 m-3">
                <span class="badge bg-success-subtle text-success border border-success-subtle fw-semibold text-uppercase" style="letter-spacing: 0.1rem; font-size: 0.6rem;">
                    <span class="me-1">●</span>Cuenta Activa
                </span>
            </div>--%>
        </div>

        <%-- Formulario --%>
        <div class="card-body p-4 p-md-5 mt-4">
            <div class="row g-4">

               

                <%-- Email --%>
                <div class="col-md-6">
                    <label class="form-label text-secondary text-uppercase fw-bold" style="font-size: 0.65rem; letter-spacing: 0.05rem;">Correo Electrónico</label>
                    <div class="input-group">
                        <span class="input-group-text bg-light text-primary">
                            <span class="material-symbols-outlined fs-6">mail</span>
                        </span>
                        <asp:TextBox
                            ID="txtEmail"
                            runat="server"
                            CssClass="form-control bg-light"
                            TextMode="Email"
                            ReadOnly="true" />
                        <span class="input-group-text bg-light text-secondary">
                            <span class="material-symbols-outlined fs-6">lock</span>
                        </span>
                    </div>
                </div>

                <%-- Nombre --%>
                <div class="col-md-6">
                    <label class="form-label text-secondary text-uppercase fw-bold" style="font-size: 0.65rem; letter-spacing: 0.05rem;">Nombre</label>
                    <div class="input-group">
                        <span class="input-group-text bg-light text-secondary">
                            <span class="material-symbols-outlined fs-6">person</span>
                        </span>
                        <asp:TextBox
                            ID="txtNombre"
                            runat="server"
                            CssClass="form-control" />
                    </div>
                </div>

                <%-- Apellido --%>
                <div class="col-md-6">
                    <label class="form-label text-secondary text-uppercase fw-bold" style="font-size: 0.65rem; letter-spacing: 0.05rem;">Apellido</label>
                    <div class="input-group">
                        <span class="input-group-text bg-light text-secondary">
                            <span class="material-symbols-outlined fs-6">badge</span>
                        </span>
                        <asp:TextBox
                            ID="txtApellido"
                            runat="server"
                            CssClass="form-control" />
                    </div>
                </div>

                <%-- Fecha de Nacimiento --%>
                <div class="col-md-6">
                    <label class="form-label text-secondary text-uppercase fw-bold" style="font-size: 0.65rem; letter-spacing: 0.05rem;">Fecha de Nacimiento</label>
                    <div class="input-group">
                        <span class="input-group-text bg-light text-secondary">
                            <span class="material-symbols-outlined fs-6">calendar_today</span>
                        </span>
                        <asp:TextBox
                            ID="txtFechaNac"
                            runat="server"
                            CssClass="form-control"
                            TextMode="Date" />
                    </div>
                </div>
               

                <%-- Seguridad --%>
                <div class="col-md-6">
                    <label class="form-label text-secondary text-uppercase fw-bold" style="font-size: 0.65rem; letter-spacing: 0.05rem;">Seguridad</label>
                    <asp:LinkButton
                        ID="lnkCambiarContrasenia"
                        runat="server"
                        CssClass="btn form-control bg-light text-start d-flex justify-content-between align-items-center border"
                        OnClick="lnkCambiarContrasenia_Click">
                    <span class="d-flex align-items-center gap-2 text-secondary">
                    <span class="material-symbols-outlined fs-6">shield_lock</span>
                     Cambiar contraseña
                    </span>
                    <span class="material-symbols-outlined fs-6 text-secondary">chevron_right</span>
                    </asp:LinkButton>
                   
                </div>
                <%-- Imagen --%>
                <div class="col-md-6">
                    <div class="mb-3">
                        <label class="form-label">Imagen de Perfil</label>
                        <input type="file" id="txtImagen" runat="server" class="form-control" />
                    </div>
                    <asp:Image ID="imagenNuevoPerfil"
                        CssClass="img-thumbnail mb-3"
                        Style="max-width: 250px;"
                        runat="server"
                        ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg" />
                </div>
            </div>
              

                <%-- Acciones --%>
                <div class="col-12 pt-4 border-top">
                    <div class="d-flex flex-column flex-md-row justify-content-between align-items-center gap-3">
                        <div class="d-flex gap-2">
                            <asp:Button
                                ID="btnGuardar"
                                runat="server"
                                Text="Guardar Cambios"
                                CssClass="btn btn-primary fw-semibold px-4"
                                OnClick="btnGuardar_Click" />
                            <asp:Button
                                ID="btnCancelar"
                                runat="server"
                                Text="Cancelar"
                                CssClass="btn btn-outline-secondary"
                                OnClick="btnCancelar_Click" />
                        </div>
                       <%-- <asp:Button
                            ID="btnDesactivar"
                            runat="server"
                            Text="Desactivar Cuenta"
                            CssClass="btn btn-outline-danger"
                            OnClick="btnDesactivar_Click" />--%>
                    </div>
                </div>

            </div>
        </div>
    </div>

   

</main>
</asp:Content>