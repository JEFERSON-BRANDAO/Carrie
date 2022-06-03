<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlterarSenha.aspx.cs" Inherits="Compras.AlterarSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 32%;
        }
        .style2
        {
            width: 151px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="style1" bgcolor="Silver">
        <tr>
            <td class="style2">
                Usuário:
            </td>
            <td>
                <asp:TextBox ID="txtLogin" Width="200px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Senha antiga
            </td>
            <td>
                <asp:TextBox ID="txtSenhaAntiga" Width="130px" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Nova senha
            </td>
            <td>
                <asp:TextBox ID="txtNovaSenha" Width="130px" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Confirmar senha
            </td>
            <td>
                <asp:TextBox ID="txtConfirmarSenha" Width="130px" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
            </td>
            <td>
                <asp:Label ID="lbAviso" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
