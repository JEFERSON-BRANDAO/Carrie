using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
// ===============================
// AUTHOR       : JEFFERSON BRANDÃO DA COSTA - ANALISTA/PROGRAMADOR
// CREATE DATE  : 13/07/2017 dd/MM/yyyy
// DESCRIPTION  : Sistema para cadastro de CARRIES
// SPECIAL NOTES:
// ===============================
// Change History: Adicionar botão  exportar grid para excel
// Date:  07/02/2018 dd/MM/yyyy
//==================================

namespace Compras
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);//limpa cache das paginas     
            //
            int anoCriacao = 2017;
            int anoAtual = DateTime.Now.Year;
            string texto = anoCriacao == anoAtual ? " Foxconn CNSBG All Rights Reserved." : "-" + anoAtual + " Foxconn CNSBG All Rights Reserved.";
            //
            lb_Rodape.Text = "Copyright © " + anoCriacao + texto + " v1.0.0.3";

            string saudacao = "";

            if (DateTime.Now.Hour >= 00 && DateTime.Now.Hour <= 11)
            {
                saudacao = "Bom dia ";
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 17)
            {
                saudacao = "Boa tarde ";
            }
            else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour <= 23)
            {
                saudacao = "Boa noite ";

            }
            //
            LbUsuario.Text = saudacao + " " + getUsuarioLogado();
            //
            if (!IsPostBack)
            {
                lbData.Text = DateTime.Now.ToString("D");

            }
            //
            MontarMenu();

        }

        public void MontarMenu()
        {
            #region CADASTROS

            //NavigationMenu.Items.Add(new MenuItem("Cadastro", "cadastro", null, null));
            //
            //NavigationMenu.FindItem("cadastro").ChildItems.Add(new MenuItem("Novo usuário", "", null, "listaUsuario.aspx"));
            //NavigationMenu.FindItem("cadastro").ChildItems.Add(new MenuItem("Novo tipo usuário", "", null, "listaTipoUsuario.aspx"));
            /// NavigationMenu.FindItem("cadastro").ChildItems.Add(new MenuItem("Novo Centro de custo", "", null, "listaCentroCusto.aspx"));
            /// 

            int Id = 0;

            try
            {
                Id = Convert.ToInt32(Session["id"]);
            }
            catch (Exception)
            {
                Session["id"] = 0;
                Response.Redirect("Login.aspx");
            }

            Permissao acessoMenu = new Permissao();
            //
            for (int i = 0; i < acessoMenu.ListaMenu(Id.ToString()).Count; i++)
            {
                string menu = acessoMenu.ListaMenu(Id.ToString())[i].menu.ToString();
                string menuItem = acessoMenu.ListaMenu(Id.ToString())[i].menuItem.ToString();
                string pagina = acessoMenu.ListaMenu(Id.ToString())[i].Pagina.ToString();
                string titulo = acessoMenu.ListaMenu(Id.ToString())[i].Titulo.ToString();

                string MenuPrincipal = string.Empty;
                //
                if (NavigationMenu.Items.Count == 0)//se menu principal ainda nao foi criado
                {
                    NavigationMenu.Items.Add(new MenuItem(menu, menuItem, null, null));
                }
                else
                {
                    try
                    {
                        MenuPrincipal = NavigationMenu.FindItem(menuItem).Text;
                    }
                    catch
                    {
                        NavigationMenu.Items.Add(new MenuItem(menu, menuItem, null, null));
                        MenuPrincipal = NavigationMenu.FindItem(menuItem).Text;
                    }

                    if (MenuPrincipal.ToUpper() != menu.ToUpper())// se menu principal ja existe, nao deixa criar
                    {
                        NavigationMenu.Items.Add(new MenuItem(menu, menuItem, null, null));
                    }
                }

                //item menu
                NavigationMenu.FindItem(menuItem).ChildItems.Add(new MenuItem(titulo, "", null, pagina));
            }          


            #endregion

        }

        protected string getUsuarioLogado()
        {
            int Id = 0;

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
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            Objconn.Conectar();
            Objconn.Parametros.Clear();
            Objconn.SetarSQL("select nome AS LOGIN from carrie.usuario WHERE idusuario = " + Id);
            Objconn.Executar();
            Objconn.Desconectar();
            //
            if (Objconn.Tabela.Rows.Count > 0)
            {
                return Objconn.Tabela.Rows[0]["LOGIN"].ToString();
            }

            Session["id"] = 0;
            Response.Redirect("Login.aspx");
            return "Não Logado.";

        }

        protected bool UsuarioLogado()
        {
            string Id = "0";

            try
            {
                Id = Session["id"].ToString();
            }
            catch (Exception)
            {
                Session["id"] = "0";
                Response.Redirect("Login.aspx");
            }

            //verifica se usuario esta logado
            if (Id == "1")
            {
                return true;
            }
            else
            {
                Session["id"] = "0";
                Response.Redirect("Login.aspx");
                return false;
            }

        }
    }
}
