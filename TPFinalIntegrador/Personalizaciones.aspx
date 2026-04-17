<%@ Page Title="Personalizaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personalizaciones.aspx.cs" Inherits="TPFinalIntegrador.Personalizaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Personalizaciones</h2>
    <div class="row">
        <div class="col-md-6">
            <h4>Categorías</h4>
            <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="IdCategoria"
                OnRowEditing="gvCategorias_RowEditing"
                OnRowUpdating="gvCategorias_RowUpdating"
                OnRowCancelingEdit="gvCategorias_RowCancelingEdit"
                OnRowDeleting="gvCategorias_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="False" />
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server"
                                CommandName="Edit"
                                Text="Editar"
                                CssClass="btn btn-sm btn-outline-primary me-1" />
                            <asp:LinkButton ID="lnkDelete" runat="server"
                                CommandName="Delete"
                                Text="Eliminar"
                                CssClass="btn btn-sm btn-danger"
                                OnClientClick='<%# "confirmDelete(\"" + gvCategorias.UniqueID + "\"," + Container.DataItemIndex + "); return false;" %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server"
                                CommandName="Update"
                                Text="Guardar"
                                CssClass="btn btn-sm btn-primary me-1" />
                            <asp:LinkButton ID="lnkCancel" runat="server"
                                CommandName="Cancel"
                                Text="Cancelar"
                                CssClass="btn btn-sm btn-secondary" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-6">
            <h4>Medios de Pago</h4>
            <asp:GridView ID="gvMediosPago" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="IdMedioPago"
                OnRowEditing="gvMediosPago_RowEditing"
                OnRowUpdating="gvMediosPago_RowUpdating"
                OnRowCancelingEdit="gvMediosPago_RowCancelingEdit"
                OnRowDeleting="gvMediosPago_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="False" />
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEditMedio" runat="server"
                                CommandName="Edit"
                                Text="Editar"
                                CssClass="btn btn-sm btn-outline-primary me-1" />
                            <asp:LinkButton ID="lnkDeleteMedio" runat="server"
                                CommandName="Delete"
                                Text="Eliminar"
                                CssClass="btn btn-sm btn-danger" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdateMedio" runat="server"
                                CommandName="Update"
                                Text="Guardar"
                                CssClass="btn btn-sm btn-primary me-1" />
                            <asp:LinkButton ID="lnkCancelMedio" runat="server"
                                CommandName="Cancel"
                                Text="Cancelar"
                                CssClass="btn btn-sm btn-secondary" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- MODAL CATEGORÍA (copiado de Inicio.aspx) -->
    <div class="modal fade" id="modalCategoria" tabindex="-1" aria-labelledby="modalCategoriaLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content border-0 rounded-4 shadow">
                <div class="modal-header border-0 pb-0">
                    <h5 class="modal-title fw-bold" id="modalCategoriaLabel">Nueva categoría</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body pt-3">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Nombre</label>
                        <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tipo</label>
                        <asp:DropDownList ID="ddlTipoCategoria" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Ingreso" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Gasto" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:Label ID="lblMensajeCategoria" runat="server" CssClass="d-block text-center mt-3"></asp:Label>
                </div>
                <div class="modal-footer border-0 pt-0">
                    <button type="button" class="btn btn-light border rounded-3" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarCategoria" runat="server" Text="Guardar" CssClass="btn btn-primary rounded-3 px-4" OnClick="btnGuardarCategoria_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL MEDIO DE PAGO (copiado de Inicio.aspx) -->
    <div class="modal fade" id="modalMedioPago" tabindex="-1" aria-labelledby="modalMedioPagoLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content border-0 rounded-4 shadow">
                <div class="modal-header border-0 pb-0">
                    <h5 class="modal-title fw-bold" id="modalMedioPagoLabel">Nuevo medio de pago</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body pt-3">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tipo</label>
                        <asp:DropDownList ID="ddlTipoMedioPago" runat="server" CssClass="form-select" onchange="toggleCamposCredito()">
                            <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Efectivo" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Débito" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Crédito" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Transferencia" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Billetera Virtual" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Descripción</label>
                        <asp:TextBox ID="txtDescripcionMedioPago" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div id="camposCredito" style="display:none;">
                        <div class="mb-3">
                            <label class="form-label fw-semibold">Día de cierre</label>
                            <asp:TextBox ID="txtDiaCierre" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label fw-semibold">Día de vencimiento</label>
                            <asp:TextBox ID="txtDiaVencimiento" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <asp:Label ID="lblMensajeMedioPago" runat="server" CssClass="d-block text-center mt-3"></asp:Label>
                </div>
                <div class="modal-footer border-0 pt-0">
                    <button type="button" class="btn btn-light border rounded-3" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarMedioPago" runat="server" Text="Guardar" CssClass="btn btn-primary rounded-3 px-4" OnClick="btnGuardarMedioPago_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function toggleCamposCredito() {
            var ddl = document.getElementById('<%= ddlTipoMedioPago.ClientID %>');
            var cont = document.getElementById('camposCredito');
            cont.style.display = ddl.value === "3" ? "block" : "none";
        }
        function limpiarModalCategoria() {
            document.getElementById('<%= txtNombreCategoria.ClientID %>').value = '';
            document.getElementById('<%= ddlTipoCategoria.ClientID %>').selectedIndex = 0;
            var lbl = document.getElementById('<%= lblMensajeCategoria.ClientID %>');
            if (lbl) { lbl.innerText = ''; lbl.className = ''; }
        }
        function limpiarModalMedioPago() {
            document.getElementById('<%= ddlTipoMedioPago.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= txtDescripcionMedioPago.ClientID %>').value = '';
            document.getElementById('<%= txtDiaCierre.ClientID %>').value = '';
            document.getElementById('<%= txtDiaVencimiento.ClientID %>').value = '';
            var lbl = document.getElementById('<%= lblMensajeMedioPago.ClientID %>');
            if (lbl) { lbl.innerText = ''; lbl.className = ''; }
            document.getElementById('camposCredito').style.display = 'none';
        }
    </script>

</asp:Content>
