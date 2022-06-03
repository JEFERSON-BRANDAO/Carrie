<%@ Page Title="Default - Carries" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Default.aspx.cs" Inherits="Carrie._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 44)
                return false;

            if (charCode == 46)
                return false;

            if (charCode == 39)
                return false;

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }

        function Data(evt) {

            return false;

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="blocoGrupoCampos" style="margin-left: 18px; width: 900px;">
                <fieldset class="bordaFieldset" style="width: 856px;">
                    <legend>Lista de Serial</legend>
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                        <ProgressTemplate>
                            <div class="blocoGrupoCampos">
                                <br />
                                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/images/carregando.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="blocoGrupoCampos">
                        <fieldset class="bordaFieldset" style="width: 815px; margin: 2px; padding: 17px;
                            padding-top: 2px">
                            <legend>Pesquisar</legend>
                            <div class="blocoeditor" style="margin-left: 0px">
                                <div class="blocoeditor" style="margin-left: 0px">
                                    <label style="margin-left: 0px">
                                        Modelo</label>
                                    <br />
                                    <%--  <asp:TextBox ID="txtSerial" runat="server" Width="200px" Height="32px"></asp:TextBox>--%>
                                    <asp:DropDownList ID="cboModelo" runat="server" Width="180px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="blocoeditor">
                                <asp:ImageButton ID="btnPesquisa" runat="server" Height="64px" ImageUrl="~/images/icon/ajuda.bmp"
                                    Width="101px" ToolTip="Consultar" OnClick="btnPesquisa_Click" />
                            </div>
                            <div class="blocoGrupoCampos">
                                <div class="blocoeditor" style="margin-left: 0px">
                                    <asp:Button ID="btnExportarExcel" runat="server" BackColor="White" ForeColor="Black" 
                                        Height="40px" Style="background-image: url(images/excel2.png); background-repeat: no-repeat;
                                        background-position: center;" Text="" ToolTip="Exportar para excel" Width="120px"
                                        OnClick="btnExportarExcel_Click" Enabled="False" />
                                </div>
                            </div>
                            <div class="blocoGrupoCampos">
                                <div class="blocoeditor">
                                    <asp:Label ID="lbAviso" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="blocoGrupoCampos" style="margin-left: 0px;">
                        <div class="blocoeditor" style="margin-left: 0px;">
                            <asp:Button ID="btnSalvar" runat="server" Width="120px" Height="40px" Style="background-image: url(images/ic_ok.png);
                                background-repeat: no-repeat; background-position: center;" BackColor="White"
                                ForeColor="Black" Text="  Zerar" ToolTip="Salvar" OnClick="btnSalvar_Click" />
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px;">
                            <asp:Button ID="btnDeletar" runat="server" Width="120px" Height="40px" Style="background-image: url(images/icon/delete.png);
                                background-repeat: no-repeat; background-position: center;" BackColor="White"
                                ForeColor="Black" OnClientClick='return confirm("Tem certeza que deseja EXCLUIR?");'
                                Text="          Excluir " ToolTip="Excluir" OnClick="btnDeletar_Click" />
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px;">
                            <asp:Button ID="btnEdita" runat="server" Width="120px" Height="40px" Style="background-image: url(images/ic_ok.png);
                                background-repeat: no-repeat; background-position: center;" BackColor="White"
                                ForeColor="Black" Text="  Editar Contagem" ToolTip="Editar contagem" OnClick="btnEdita_Click" />
                        </div>
                        <asp:GridView ID="GridSeriais" Width="855px" runat="server" AutoGenerateColumns="False"
                            AlternatingRowStyle-BackColor="#B4EEB4" CellPadding="4" ForeColor="#333333" GridLines="None"
                            DataKeyNames="Id" PageSize="15">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="novoCarrie.aspx?id={0}"
                                    DataTextField="serial" HeaderText="Serial">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="produto" HeaderText="Produto">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="data_inicio" HeaderText="Data/Hora Inicio" DataFormatString=""
                                    NullDisplayText="-">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <%-- <asp:BoundField DataField="usado" HeaderText="Usado">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Usado">
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lbSerial" runat="server" Text='<%# Eval("serial") %>'></asp:Label>--%>
                                        <asp:TextBox ID="txtUsado" runat="server" Text='<%# Eval("usado") %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="97px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Selecione">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSelecione" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serial" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSerial" runat="server" Text='<%# Eval("serial") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Usado" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbUsado" runat="server" Text='<%# Eval("usado") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <EmptyDataTemplate>
                                <label>
                                    Nenhum registro econtrado.</label>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
