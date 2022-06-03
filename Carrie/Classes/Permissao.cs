using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data;
using System.Collections;
using Classes;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Classes
{
    public class Permissao
    {
        public IList<Menu> ListaMenu(string IdUsuario)
        {
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {              
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string Sql = @" select    m.idmenu as Id,
                                          m.menu as Menu,
                                          m.menuItem as MenuItem,
                                          m.titulo as Titulo,
                                          m.pagina as Pagina,
                                          m.idgrupo,
                                          m.status,
                                          g.nome as Grupo,
                                          u.idusuario as idUsuario
                                          from carrie.menu m
                                          inner join carrie.usuario u on m.idgrupo = u.idgrupo
                                          inner join carrie.grupo g on m.idgrupo = g.idgrupo
                                          where m.status = 1 and u.idusuario = " + IdUsuario;
                //
                Objconn.SetarSQL(Sql);
                Objconn.Executar();
            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            List<Menu> Lista = new List<Menu>();
            //
            if (Objconn.Tabela.Rows.Count > 0)
            {
                for (int i = 0; i < Objconn.Tabela.Rows.Count; i++)
                {
                    Menu objMenu = new Menu();
                    objMenu.menu = Objconn.Tabela.Rows[i]["Menu"].ToString();
                    objMenu.menuItem = Objconn.Tabela.Rows[i]["MenuItem"].ToString();
                    objMenu.Titulo = Objconn.Tabela.Rows[i]["Titulo"].ToString();
                    objMenu.Pagina = Objconn.Tabela.Rows[i]["Pagina"].ToString();
                    //
                    Lista.Add(objMenu);
                }

            }
            //
            return Lista;
        }

        public bool PermiteAcessoMenu(string IdUsuario, string pagina)
        {

            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string Sql = @" select    m.idmenu as Id,
                                      m.menu as Menu,
                                      m.menuItem as MenuItem,
                                      m.titulo as Titulo,
                                      m.pagina as Pagina,
                                      m.idgrupo,
                                      m.status,
                                      g.nome as Grupo,
                                      u.idusuario as idUsuario
                                      from carrie.menu m
                                      inner join carrie.usuario u on m.idgrupo = u.idgrupo
                                      inner join carrie.grupo g on m.idgrupo = g.idgrupo
                                      where m.status = 1 and u.idusuario = " + IdUsuario + " and m.pagina = '" + pagina + "'";
                    //
                Objconn.SetarSQL(Sql);
                Objconn.Executar();
            }
            finally
            {
                Objconn.Desconectar();
            }
            //  
            return Objconn.Tabela.Rows.Count > 0 ? true : false;

        }

    }
}