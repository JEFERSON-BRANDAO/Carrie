<%@ Page Title="Novo Carrie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="novoCarrie.aspx.cs" Inherits="Carrie.novoCarrie" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <div class="blocoGrupoCampos" style="margin-left: 18px; width: 900px;">
        <fieldset class="bordaFieldset" style="width: 856px;">
            <legend>Cadastrar/Editar Carrie</legend>
            <div class="blocoGrupoCampos">
                <fieldset class="bordaFieldset" style="width: 240px; margin: 5px; padding: 17px;
                    padding-top: 2px">
                    <legend>Adicionar quantidade</legend>
                    <div class="blocoeditor" style="margin-left: 0px">
                        <label style="margin-left: 0px">
                            Qtd</label>
                        <br />
                        <asp:TextBox ID="txtQtd" runat="server" Width="32px" Font-Bold="True">0</asp:TextBox>
                    </div>
                </fieldset>
            </div>
            <div class="blocoGrupoCampos">
                <div class="blocoeditor" style="margin-left: 0px">
                    <label style="margin-left: 0px">
                        Serial</label>
                    <br />
                    <asp:TextBox ID="txtSerial" runat="server" Width="210px" BackColor="Silver" Font-Bold="True"
                        ReadOnly="True" ForeColor="Blue"></asp:TextBox>
                </div>
                <div runat="server" id="divData" class="blocoeditor" style="margin-left: 0px">
                    <label style="margin-left: 0px">
                        Data/Hora</label>
                    <br />
                    <asp:TextBox ID="txtData" runat="server" BackColor="Silver" ToolTip="" Font-Bold="True"
                        ReadOnly="True"></asp:TextBox>
                </div>
                <div class="blocoeditor" style="margin-left: 0px">
                    <label style="margin-left: 0px">
                        Produto</label>
                    <br />
                    <asp:DropDownList ID="cboProduto" Width="140" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="btnSalvar"
                        Text="*" ControlToValidate="cboProduto" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="blocoGrupoCampos">
                <div class="blocoeditor">
                    <asp:Label ID="lbAviso" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="blocoGrupoCampos" style="margin-left: 18px;">
        <div class="blocoeditor" style="margin-left: 0px;">
            <asp:Button ID="btnCancelar" runat="server" Width="120px" Height="40px" Style="background-image: url(images/icon/back.png);
                background-repeat: no-repeat; background-position: center;" BackColor="White"
                ForeColor="Black" Text="          Voltar " ToolTip="Voltar" OnClick="btnCancelar_Click" />
        </div>
        <div class="blocoeditor" style="margin-left: 0px;">
            <asp:Button ID="btnDeletar" runat="server" Width="120px" Height="40px" Style="background-image: url(images/icon/delete.png);
                background-repeat: no-repeat; background-position: center;" BackColor="White"
                ForeColor="Black" OnClientClick='return confirm("Tem certeza que deseja EXCLUIR?");'
                Text="          Excluir " ToolTip="Excluir" OnClick="btnDeletar_Click" />
        </div>
        <div class="blocoeditor" style="margin-left: 8px;">
            <asp:Button ID="btnSalvar" ValidationGroup="btnSalvar" runat="server" Width="120px"
                Height="40px" Style="background-image: url(images/ic_ok.png); background-repeat: no-repeat;
                background-position: center;" BackColor="White" ForeColor="Black" OnClientClick='return confirm("Tem certeza que deseja SALVAR?");'
                Text="        Salvar" ToolTip="Salvar" OnClick="btnSalvar_Click" />
        </div>
    </div>
</asp:Content>
