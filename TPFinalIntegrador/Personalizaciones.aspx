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

</asp:Content>
