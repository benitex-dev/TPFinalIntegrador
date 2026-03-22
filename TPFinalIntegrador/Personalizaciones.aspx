<%@ Page Title="Personalizaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personalizaciones.aspx.cs" Inherits="TPFinalIntegrador.Personalizaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Personalizaciones</h2>
    <div class="row">
        <div class="col-md-6">
            <h4>Categorías</h4>
            <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="IdCategoria" OnRowEditing="gvCategorias_RowEditing" OnRowUpdating="gvCategorias_RowUpdating"
                OnRowCancelingEdit="gvCategorias_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="False" />
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="True" />
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-6">
            <h4>Medios de Pago</h4>
            <asp:GridView ID="gvMediosPago" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="IdMedioPago" OnRowEditing="gvMediosPago_RowEditing" OnRowUpdating="gvMediosPago_RowUpdating"
                OnRowCancelingEdit="gvMediosPago_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="False" />
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="True" />
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
