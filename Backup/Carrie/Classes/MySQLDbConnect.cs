using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Classes
{
    public class MySQLDbConnect
    {
        #region Attributes

        private bool _isvalid;
        private string _message;
        private string _stringConnection;
        private MySqlConnection _connection;
        private DataTable _tabela;
        private IList _parametros = new ArrayList();
        private MySqlTransaction _transaction;
        private MySqlCommand _command;

        #endregion

        #region Properties

        public string StringConnection
        {
            get { return _stringConnection; }
            set { _stringConnection = value; }
        }

        public MySqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public DataTable Tabela
        {
            get { return _tabela; }
            set { _tabela = value; }
        }

        public IList Parametros
        {
            get { return _parametros; }
            set { _parametros = value; }
        }

        public MySqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        public bool Isvalid
        {
            get { return _isvalid; }
            set { _isvalid = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        #endregion

        #region Methods

        public bool Conectar()
        {
            try
            {

                string _connectionString = "server=" + ConfigurationManager.AppSettings["IP_SERVER"].ToString() + ";uid=root;pwd=" + ConfigurationManager.AppSettings["pwd"].ToString() + ";database=" + ConfigurationManager.AppSettings["database"].ToString();

                _connection = new MySqlConnection(_connectionString);

                _connection.Open();
                return true;
            }
            catch (Exception erro)
            {
                Message = erro.Message;
                return false;
            }
        }

        public void Desconectar()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception) { }
        }

        public void AdicionarParametro(string nome, object valor, SqlDbType tipo)
        {
            MySqlParameter parametro = new MySqlParameter(nome, tipo);
            parametro.Direction = ParameterDirection.Input;
            parametro.Value = valor;

            _parametros.Add(parametro);
        }

        public void AdicionarParametroSaida(string nome, SqlDbType tipo)
        {
            MySqlParameter parametro = new MySqlParameter(nome, tipo);
            parametro.Direction = ParameterDirection.Output;

            _parametros.Add(parametro);
        }

        public void SetarSQL(string SQL)
        {
            _command = new MySqlCommand();
            _command.CommandType = CommandType.Text;
            _command.CommandText = SQL;
            _command.Connection = _connection;
        }

        public void SetarSP(string nomeSP)
        {
            _command = new MySqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = nomeSP;
            _command.Connection = _connection;
        }

        public bool Executar()
        {

            try
            {
                //_command.Parameters.Clear();

                foreach (MySqlParameter parametro in _parametros)
                {
                    _command.Parameters.Add(parametro);

                    //_command.Parameters.AddWithValue(parametro.ToString(), "");
                }

                //_parametros = new ArrayList();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(_command);
                Tabela = new DataTable();
                dataAdapter.Fill(Tabela);

                _isvalid = true;
                _message = "";

                return true;
            }
            catch (Exception erro)
            {
                _isvalid = false;
                _message = erro.Message;

                return false;
            }
        }

        #endregion
    }
}
