using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Data;

namespace Compras
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbAviso.Visible = false;
            txtLogin.Focus();
            //
            if (!IsPostBack)
            {
                string Id = "0";
                //
                try
                {
                    Id = Session["id"].ToString();
                }
                catch (Exception)
                {
                    Session["id"] = "0";
                    Response.Redirect("Login.aspx");
                }
                //
                txtLogin.Focus();

            }

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                Session["id"] = "0";
            IniciaSessao();

        }

        protected void IniciaSessao()
        {
            try
            {
                string id = "0";
                id = Session["id"].ToString();
                if (id == "0")
                {
                }
            }
            catch (Exception msgERRO)
            {
                Session["id"] = "0";
                lbAviso.Text = msgERRO.Message; //mostra erro de excessao
            }

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {

            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string sql = @"SELECT idusuario AS ID,
                                  nome AS LOGIN, 
                                  senha AS SENHA,
                                  status                                     
                              FROM carrie.usuario   
                              WHERE nome = '" + txtLogin.Text.Trim().ToUpper() + "' AND senha = '" + txtSenha.Text.Trim() + "'";
                //
                Objconn.SetarSQL(sql);               
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    int Id = Convert.ToInt32(Objconn.Tabela.Rows[0][0]);
                    Session["id"] = Id;
                    string status = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["status"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["status"].ToString();
                    //
                    if (status.Equals("1"))
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "Usuário desativado.";
                    }
                }
                else
                {
                    lbAviso.Visible = true;
                    lbAviso.Text = "Usuário ou senha inválido.";
                }
            }
            finally
            {
                Objconn.Desconectar();
            }
        }
    }
}