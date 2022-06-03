using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace Compras
{
    public partial class AlterarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbAviso.Visible = false;
            txtLogin.Focus();

        }

        public bool UsuarioExistente()
        {
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            Objconn.Conectar();
            Objconn.Parametros.Clear();
            //
            string sql = @"SELECT idusuario AS ID,
                                                  nome AS LOGIN, 
                                                  senha AS SENHA
                                           FROM carrie.usuario   
                                           WHERE nome = '" + txtLogin.Text.Trim().ToUpper() + "' AND senha = '" + txtSenhaAntiga.Text.Trim() + "'";

            //
            Objconn.SetarSQL(sql);
            //
            Objconn.Executar();
            Objconn.Desconectar();
            //
            if (Objconn.Tabela.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string UsuarioAtivo()
        {
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            Objconn.Conectar();
            Objconn.Parametros.Clear();
            //
            string sql = @"SELECT idusuario AS ID,                                                 
                                            status
                                           FROM carrie.usuario   
                                           WHERE nome = '" + txtLogin.Text.Trim().ToUpper() + "' AND senha = '" + txtSenhaAntiga.Text.Trim() + "'";

            //
            Objconn.SetarSQL(sql);
            //
            Objconn.Executar();
            Objconn.Desconectar();
            //
            if (Objconn.Tabela.Rows.Count > 0)
            {
                return Objconn.Tabela.Rows[0]["status"].ToString();
            }
            else
            {
                return "0";
            }
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            if (UsuarioExistente())
            {
                if (txtNovaSenha.Text.Equals(txtConfirmarSenha.Text))
                {

                    if (UsuarioAtivo().Equals("1"))
                    {                        

                        MySQLDbConnect Objconn = new MySQLDbConnect();
                        //
                        try
                        {
                            try
                            {
                                Objconn.Conectar();
                                Objconn.Parametros.Clear();
                                //
                                string sql = @"UPDATE carrie.usuario SET  senha = '" + txtNovaSenha.Text.Trim() + "' WHERE nome = '" + txtLogin.Text.Trim() + "'  AND senha = '" + txtSenhaAntiga.Text.Trim() + "'";
                                //
                                Objconn.SetarSQL(sql);
                                //
                                Objconn.Executar();

                            }
                            finally
                            {
                                Objconn.Desconectar();
                            }

                            //
                            if (Objconn.Isvalid)
                            {
                                Response.Redirect("login.aspx");
                            }
                            else
                            {
                                lbAviso.Visible = true;
                                lbAviso.Text = "ERRO:: " + Objconn.Message;
                            }
                        }
                        catch (Exception erro)
                        {
                            lbAviso.Visible = true;
                            lbAviso.Text = "ERRO:: " + erro.Message;
                        }

                    }
                    else 
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "ERRO:: Usuário desativado.";
                    }


                }
                else
                {
                    lbAviso.Visible = true;
                    lbAviso.Text = "Confirmação da senha diferente.";
                }

            }
            else
            {
                lbAviso.Visible = true;
                lbAviso.Text = "Usuário ou senha inválido.";
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}