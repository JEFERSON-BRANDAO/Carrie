using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Data;
using System.Configuration;
using System.IO;

namespace Carrie
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Para corrigir erro  Sys.WebForms.PageRequestManagerParserErrorException: The message received from the server could not be parsed 
            //No Download exel
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExportarExcel);

            lbAviso.Visible = false;
            btnDeletar.Enabled = false;

            int Id = 0;
            //
            try
            {
                Id = Convert.ToInt32(Session["id"]);

                if (Id == 0)
                    Response.Redirect("Login.aspx");
            }
            catch (Exception)
            {
                Session["id"] = 0;
                Response.Redirect("Login.aspx");
            }
            //
            #region PERMISSÃO ACESSO

            Permissao Menu = new Permissao();
            //
            if (!Menu.PermiteAcessoMenu(Id.ToString(), "novoCarrie.aspx"))
            {
                Response.Redirect("Default.aspx");
            }

            #endregion
            //
            if (!IsPostBack)
            {
                btnSalvar.Enabled = false;
                //
                CarregaComboModelo();
                CarreGrid();
            }

        }

        public void CarregaComboModelo()
        {
            MySQLDbConnect Objconn = new MySQLDbConnect();
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                Objconn.SetarSQL(@" select p.idproduto as Id, p.nome as Descricao from carrie.produto as p");
                Objconn.Executar();
                //
                cboModelo.DataSource = Objconn.Tabela;
                cboModelo.DataTextField = "Descricao";
                cboModelo.DataValueField = "Id";
                cboModelo.DataBind();
                cboModelo.Items.Insert(0, new ListItem("[TODOS]", string.Empty));
            }
            finally
            {
                Objconn.Desconectar();
            }
        }

        public void CarreGrid()
        {

            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string modelo = cboModelo.SelectedValue;
                string SQLmodelo = string.IsNullOrEmpty(modelo) ? " " : "WHERE c.idproduto ='" + cboModelo.SelectedValue + "' ";
                string Sql = @" SELECT c.idcarrie Id,
                                   c.serial, 
		                           c.usado,
                                   data_inicio,
                                   p.nome AS produto                          
                           FROM carrie.carrie c                         
                           INNER JOIN carrie.produto p ON c.idproduto = p.idproduto "
                               + SQLmodelo +
                               " ORDER BY c.serial DESC";
                //
                Objconn.SetarSQL(Sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    btnExportarExcel.Enabled = true;
                    //
                    btnSalvar.Enabled = true;
                    btnDeletar.Enabled = true;
                    //
                    GridSeriais.DataSource = Objconn.Tabela;
                    GridSeriais.DataBind();
                    //
                    int index = 0;
                    int limiteEmAlerta = string.IsNullOrEmpty(ConfigurationManager.AppSettings["LIMITE_EM_ALERTA"].ToString()) ? 0 : int.Parse(ConfigurationManager.AppSettings["LIMITE_EM_ALERTA"].ToString());
                    int limiteUso = string.IsNullOrEmpty(ConfigurationManager.AppSettings["LIMITE_USO"].ToString()) ? 0 : int.Parse(ConfigurationManager.AppSettings["LIMITE_USO"].ToString());

                    //
                    foreach (GridViewRow gvRow in GridSeriais.Rows)
                    {
                        ((TextBox)gvRow.FindControl("txtUsado")).Attributes.Add("onkeypress", "return isNumberKey(event);");

                        //if (int.Parse(gvRow.Cells[3].Text) >= limiteUso)   

                        if ((int.Parse(((TextBox)gvRow.FindControl("txtUsado")).Text) >= limiteEmAlerta) && (int.Parse(((TextBox)gvRow.FindControl("txtUsado")).Text) < limiteUso))
                        {
                            GridSeriais.Rows[index].BackColor = System.Drawing.Color.Yellow;
                            GridSeriais.Rows[index].ToolTip = "ALERTA: Serial já atingiu " + limiteEmAlerta + "K";
                        }
                        else
                            if (int.Parse(((TextBox)gvRow.FindControl("txtUsado")).Text) >= limiteUso)
                            {
                                GridSeriais.Rows[index].BackColor = System.Drawing.Color.Red;
                                GridSeriais.Rows[index].ToolTip = "Serial já atingiu limite de uso " + limiteUso + "K";
                            }
                        //
                        index++;
                    }

                }
                else
                {
                    btnExportarExcel.Enabled = false;
                    //
                    btnSalvar.Enabled = false;
                    btnDeletar.Enabled = false;
                    //
                    GridSeriais.DataSource = null;
                    GridSeriais.DataBind();
                }
            }
            finally
            {
                Objconn.Desconectar();
            }
        }

        protected void btnPesquisa_Click(object sender, ImageClickEventArgs e)
        {
            CarreGrid();
        }

        public string ZerarQuantidade(string Serial)
        {
            string mensagem = string.Empty;
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //              
                string Sql = @"UPDATE carrie.carrie SET data_inicio= NULL, usado= 0 WHERE serial IN (" + Serial + ")";
                //
                Objconn.SetarSQL(Sql);
                Objconn.Executar();
                //
                if (Objconn.Isvalid)
                {
                    mensagem = "OK";
                }
                else
                {
                    mensagem = Objconn.Message;
                }

            }
            finally
            {
                Objconn.Desconectar();
            }

            //
            return mensagem;

        }

        public string Deletar(string Serial)
        {
            string mensagem = string.Empty;
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //              
                string Sql = @"DELETE FROM carrie.carrie  WHERE serial IN (" + Serial + ")";
                //
                Objconn.SetarSQL(Sql);
                Objconn.Executar();
                //
                if (Objconn.Isvalid)
                {
                    mensagem = "OK";
                }
                else
                {
                    mensagem = Objconn.Message;
                }

            }
            finally
            {
                Objconn.Desconectar();
            }

            //
            return mensagem;

        }

        public string EditarQuantidade(string Serial, string Quantidade)
        {
            string mensagem = string.Empty;
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //              
                string Sql = @"UPDATE carrie.carrie SET usado= '" + Quantidade + "' WHERE serial = " + Serial;
                //
                Objconn.SetarSQL(Sql);
                Objconn.Executar();
                //
                if (Objconn.Isvalid)
                {
                    mensagem = "OK";
                }
                else
                {
                    mensagem = Objconn.Message;
                }

            }
            finally
            {
                Objconn.Desconectar();
            }

            //
            return mensagem;

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (GridSeriais.Rows.Count > 0)
            {
                string sn = "";
                //
                foreach (GridViewRow gvRow in GridSeriais.Rows)
                {

                    if (((CheckBox)gvRow.FindControl("ChkSelecione")).Checked)
                    {
                        sn += string.IsNullOrEmpty(((Label)gvRow.FindControl("lbSerial")).Text) ? string.Empty : "'" + ((Label)gvRow.FindControl("lbSerial")).Text + "',";
                    }

                }

                //
                sn = string.IsNullOrEmpty(sn) ? sn : sn.Remove(sn.Length - 1);//remove último caractere ","

                if (!string.IsNullOrEmpty(sn))
                {
                    string mensagem = ZerarQuantidade(sn);
                    //
                    if (mensagem.Equals("OK"))
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "Alteração realizada com sucesso!";
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = mensagem;
                    }
                }
                else
                {
                    lbAviso.Visible = true;
                    lbAviso.Text = "Selecione um item!";
                }

            }

            //Refresh
            CarreGrid();

        }

        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            if (GridSeriais.Rows.Count > 0)
            {
                string sn = "";
                //
                foreach (GridViewRow gvRow in GridSeriais.Rows)
                {

                    if (((CheckBox)gvRow.FindControl("ChkSelecione")).Checked)
                    {
                        sn += string.IsNullOrEmpty(((Label)gvRow.FindControl("lbSerial")).Text) ? string.Empty : "'" + ((Label)gvRow.FindControl("lbSerial")).Text + "',";
                    }

                }

                //
                sn = string.IsNullOrEmpty(sn) ? sn : sn.Remove(sn.Length - 1);//remove último caractere ","

                if (!string.IsNullOrEmpty(sn))
                {
                    string mensagem = Deletar(sn);
                    //
                    if (mensagem.Equals("OK"))
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "Registro(s) deleteado(s) com sucesso!";
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = mensagem;
                    }
                }
                else
                {
                    lbAviso.Visible = true;
                    lbAviso.Text = "Selecione um item!";
                }

            }

            //Refresh
            CarreGrid();
        }

        protected void btnEdita_Click(object sender, EventArgs e)
        {
            if (GridSeriais.Rows.Count > 0)
            {
                string sn = "";
                int update = 0;
                string mensagem = "";

                int checked_ = 0;
                //
                foreach (GridViewRow gvRow in GridSeriais.Rows)
                {

                    if (((CheckBox)gvRow.FindControl("ChkSelecione")).Checked)
                    {
                        checked_++;

                        sn = string.IsNullOrEmpty(((Label)gvRow.FindControl("lbSerial")).Text) ? string.Empty : "'" + ((Label)gvRow.FindControl("lbSerial")).Text + "',";
                        sn = string.IsNullOrEmpty(sn) ? sn : sn.Remove(sn.Length - 1);//remove último caractere ","

                        //
                        if (!string.IsNullOrEmpty(sn))
                        {
                            string quantidade = ((TextBox)gvRow.FindControl("txtUsado")).Text.Trim();

                            if (!string.IsNullOrEmpty(quantidade))
                            {
                                mensagem = EditarQuantidade(sn, quantidade);
                                //
                                if (mensagem.Equals("OK"))
                                {
                                    update = 1;
                                }
                            }

                        }

                    }

                }

                if (checked_ > 0)
                {
                    //
                    if (update == 1)//update realizado com sucesso
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "Quantidade alterada com sucesso!";
                    }
                    else if (update == 0)//erro no update
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = mensagem;
                    }
                }
                else
                {
                    lbAviso.Visible = true;
                    lbAviso.Text = "Selecione um item!";
                }

            }

            //Refresh
            CarreGrid();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */

            //PARA NÃO OCORRER ERROR:
            //Control 'MainContent_GridSolicitacoes' of type 'GridView' must be placed inside a form tag with runat=server.           
        }

        public void ExportarExcel()
        {
            GridSeriais.Columns[3].Visible = false;
            GridSeriais.Columns[4].Visible = false;
            GridSeriais.Columns[6].Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Carrie.xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridSeriais.GridLines = GridLines.Both;//grid no excel com borda
            GridSeriais.HeaderStyle.Font.Bold = true;
            GridSeriais.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }

    }
}
