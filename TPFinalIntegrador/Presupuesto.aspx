<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Presupuesto.aspx.cs" Inherits="TPFinalIntegrador.Presupuesto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Presupuesto mensual por categoría</h2>

      <div class="row mb-3">
          <div class="col-auto">
              <asp:Button ID="btnMesAnterior" runat="server" Text="&lt;" CssClass="btn btn-outline-secondary" OnClick="btnMesAnterior_Click" />
          </div>
          <div class="col-auto">
              <asp:Label ID="lblMesActual" runat="server" CssClass="h5 mt-1 d-inline-block" />
          </div>
          <div class="col-auto">
              <asp:Button ID="btnMesSiguiente" runat="server" Text="&gt;" CssClass="btn btn-outline-secondary" OnClick="btnMesSiguiente_Click" />
          </div>
      </div>

      <asp:GridView ID="gvPresupuesto" runat="server" CssClass="table table-bordered"
          AutoGenerateColumns="false" DataKeyNames="IdPresupuesto"
          OnRowCommand="gvPresupuesto_RowCommand">
          <Columns>
              <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />

              <asp:TemplateField HeaderText="Presupuestado">
                  <ItemTemplate>
                      <asp:TextBox ID="txtPresupuesto" runat="server"
                          Text='<%# ((decimal)Eval("MontoPresupuestado")) > 0 ? Eval("MontoPresupuestado") : "" %>'
                          CssClass="form-control" placeholder="Sin presupuesto" />
                      <asp:HiddenField ID="hfIdCategoria" runat="server" Value='<%# Eval("Categoria.IdCategoria") %>' />
                  </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Gastado">
                  <ItemTemplate>
                      <asp:Label runat="server" Text='<%# "$ " + ((decimal)Eval("GastoReal")).ToString("N2") %>' />
                  </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="% Usado">
                  <ItemTemplate>
                      <%# GenerarBarra(Eval("MontoPresupuestado"), Eval("GastoReal")) %>
                  </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="">
                  <ItemTemplate>
                      <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                          CssClass="btn btn-sm btn-primary"
                          CommandName="GuardarPresupuesto"
                          CommandArgument='<%# Container.DataItemIndex %>' />
                  </ItemTemplate>
              </asp:TemplateField>
          </Columns>
      </asp:GridView>

      <asp:Label ID="lblMensaje" runat="server" />
</asp:Content>
