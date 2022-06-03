<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Compras.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 63px;
        }
        .style3
        {
            color: #990000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td class="style2">
                Usuário<span class="style3">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtLogin" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Senha<span class="style3">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtSenha" runat="server" Width="130px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnEntrar" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
            </td>
            <td>
                <div>
                    <asp:Label ID="lbAviso" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                    <asp:HyperLink ID="HplTrocarSenha" runat="server" ForeColor="Red" NavigateUrl="~/AlterarSenha.aspx">Alterar senha</asp:HyperLink>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
