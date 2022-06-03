using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace Carrie
{
    public partial class novoCarrie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtQtd.Attributes.Add("onkeypress", "return isNumberKey(event);");
            //
            btnDeletar.Enabled = false;
            lbAviso.Visible = false;
            //
            if (!IsPostBack)
            {
                int Id = 0;
                //
                try
                {
                    Id = Convert.ToInt32(Session["id"]);
                }
                catch (Exception)
                {
                    Session["id"] = 0;
                    Response.Redirect("Login.aspx");
                }
                //
                getComboProdutos();
                //
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    btnDeletar.Enabled = true;
                    btnSalvar.Visible = false;
                    cboProduto.Enabled = false;
                    //
                    txtQtd.Enabled = false;
                    txtQtd.ToolTip = "Não permitido para edição";
                    //
                    divData.Visible = true;                    
                    btnDeletar.Visible = true;
                    //
                    int idcarrie = 0;
                    Int32.TryParse(Request.QueryString["id"], out idcarrie);                                  
                    
                    MySQLDbConnect Objconn = new MySQLDbConnect();
                    //
                    try
                    {
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        //                  
                        string Sql = @" SELECT c.idcarrie Id,
                                   c.serial, 
		                           c.usado,
                                   c.data_inicio,
                                   p.nome AS produto,
                                   p.idproduto                           
                           FROM carrie.carrie c                         
                           INNER JOIN carrie.produto p ON c.idproduto = p.idproduto 
                           WHERE c.idcarrie =" + idcarrie;
                        //
                        Objconn.SetarSQL(Sql);
                        Objconn.Executar();
                        //
                        if (Objconn.Tabela.Rows.Count > 0)
                        {
                            txtSerial.Text = Objconn.Tabela.Rows[0]["serial"].ToString();
                            txtData.Text = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["data_inicio"].ToString()) ? "" : Convert.ToDateTime(Objconn.Tabela.Rows[0]["data_inicio"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                            cboProduto.SelectedValue = Objconn.Tabela.Rows[0]["idproduto"].ToString();//descricao 
                        }
                    }
                    finally
                    {
                        Objconn.Desconectar();
                    }

                }
                else
                {
                    divData.Visible = false;
                    //
                    txtQtd.ToolTip = "Adicione quantidade de etiqueta que deseja gerar para o modelo selecionado";
                    txtQtd.Enabled = true;
                }
            }
        }

        private void getComboProdutos()
        {
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                Objconn.SetarSQL(@"SELECT  p.idproduto AS Id,
                                       p.nome AS Descricao                                     
                                  FROM carrie.produto p");
                Objconn.Executar();
                //
                cboProduto.DataSource = Objconn.Tabela;
                cboProduto.DataTextField = "Descricao";
                cboProduto.DataValueField = "Id";
                cboProduto.DataBind();
                cboProduto.Items.Insert(0, new ListItem("[Selecione]", string.Empty));
            }
            finally
            {
                Objconn.Desconectar();
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int idcarrie = 0;
                Int32.TryParse(Request.QueryString["id"], out idcarrie);
                //
                MySQLDbConnect Objconn = new MySQLDbConnect();
                //
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"DELETE FROM carrie.carrie WHERE idcarrie = " + idcarrie;
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Isvalid)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "Erro::" + Objconn.Message;
                    }
                }
                finally
                {
                    Objconn.Desconectar();
                }
            }

        }

        private bool existeCampoVazio()
        {
            if (cboProduto.SelectedValue.ToString().Equals(string.Empty))
            {
                return true;
            }

            return false;
        }

        public string GetUltimoSerial()
        {

            MySQLDbConnect Objconn = new MySQLDbConnect();
            string serial = string.Empty;
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //               
                //string sql = @"SELECT MAX(serial) AS serial FROM carrie.carrie WHERE idproduto = " + IdProduto;
                string sql = @"SELECT MAX(serial) AS serial FROM carrie.carrie";
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Isvalid)
                {
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        serial = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["serial"].ToString()) ? "00" : Objconn.Tabela.Rows[0]["serial"].ToString();
                    }
                    else
                    {
                        serial = "00";
                    }
                }
                else
                {
                    lbAviso.Visible = true;
                    lbAviso.Text = "Erro::" + Objconn.Message;
                    //
                    serial = "Erro::";
                }
            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            return serial;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!existeCampoVazio())
                {
                    #region UPDATE

                    int Id = 0;
                    Int32.TryParse(Request.QueryString["id"], out Id);
                    //
                    MySQLDbConnect Objconn = new MySQLDbConnect();
                    //
                    try
                    {
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        //
                        string idProduto = cboProduto.SelectedValue;
                        //
                        string Sql = @"UPDATE carrie.carrie  SET   idproduto   = '" + idProduto + @"'                                                                 
                                                             WHERE idcarrie    = " + Id;
                        Objconn.SetarSQL(Sql);
                        Objconn.Executar();
                        //
                        if (Objconn.Isvalid)
                        {
                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            lbAviso.Visible = true;
                            lbAviso.Text = "Erro::" + Objconn.Message;
                        }
                    }
                    finally
                    {
                        Objconn.Desconectar();
                    }

                    #endregion
                }
            }
            else
            {
                if (!existeCampoVazio())
                {
                    if (!string.IsNullOrEmpty(txtQtd.Text))
                    {
                        if (int.Parse(txtQtd.Text.Trim()) > 0)
                        {
                            #region INSERT

                            MySQLDbConnect Objconn = new MySQLDbConnect();
                            //                         
                            int Isvalid = 0;
                            string dataAtual = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            string idProduto = cboProduto.SelectedValue;
                            string produto = "PTH-";//cboProduto.SelectedItem.Text;
                            //
                            for (int quantidade = 1; quantidade <= int.Parse(txtQtd.Text.Trim()); quantidade++)
                            {
                                try
                                {
                                    Objconn.Conectar();
                                    Objconn.Parametros.Clear();

                                    string UltimoSerial = GetUltimoSerial();
                                    // 
                                    if (UltimoSerial != "Erro::")
                                    {
                                        Serial s = new Serial();
                                        string NovoSerial = s.GetSerialNumber(produto, UltimoSerial);
                                        txtSerial.Text = NovoSerial;
                                        //
                                        string Sql = @"INSERT INTO carrie.carrie (serial,
                                                                      idproduto,
                                                                      usado)
                                              VALUES('" + NovoSerial + "','" + idProduto + "','0')";

                                        //
                                        Objconn.SetarSQL(Sql);
                                        Objconn.Executar();
                                        //
                                        if (Objconn.Isvalid)
                                        {
                                            Isvalid++;
                                        }
                                        else
                                        {
                                            lbAviso.Visible = true;
                                            lbAviso.Text = "Erro::" + Objconn.Message;
                                        }
                                    }
                                }
                                finally
                                {
                                    Objconn.Desconectar();
                                }
                            }
                            //
                            if (Isvalid > 0)
                                Response.Redirect("Default.aspx");

                            #endregion
                        }
                        else
                        {
                            lbAviso.Visible = true;
                            lbAviso.Text = "Quantidade dever ser > 0";
                        }
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "Informe a quantidade de etiqueta(s) que deseja imprimir";
                    }
                }
            }
        }
    }
}