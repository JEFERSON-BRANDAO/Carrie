using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Classes
{
    public class Serial
    {
        //protected string GetProduto(string produto)
        //{
        //    #region PRODUTOS

        //    string modelo = string.Empty;
        //    //
        //    try
        //    {
        //        string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIG\produtos.txt";
        //        string linha;              
        //        //
        //        if (System.IO.File.Exists(caminho))
        //        {
        //            System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
        //            //                 
        //            while ((linha = arqTXT.ReadLine()) != null)
        //            {
        //                if (linha.Trim().ToUpper().Equals(produto))
        //                    modelo = linha.Trim().ToUpper();
        //            }
        //            //
        //            arqTXT.Close();
        //        }

        //    }
        //    catch
        //    {
        //        //
        //    }
        //    //
        //    return modelo;

        //    #endregion
        //}

        public string GetSerialNumber(string produto, string ultimoSerial)
        {
            string serial = string.Empty;
            //
            if (!string.IsNullOrEmpty(ultimoSerial))//quando já existir registro na tabela  C7290112
            {
                string ultimosDigitos = ultimoSerial;
                //
                if (ultimoSerial.Length > 4)
                {
                    if (ultimoSerial.Contains("TG"))
                    {
                        ultimosDigitos = ultimoSerial.Remove(0, 6);
                    }
                    else
                    {
                        ultimosDigitos = ultimoSerial.Remove(0, 4);
                    }
                }
                //
                serial = (int.Parse(ultimosDigitos) + 1).ToString();
                //
                if (serial.Length == 1)
                {
                    serial = "00" + serial.ToString();
                }
                else if (serial.Length == 2)
                {
                    serial = "0" + serial.ToString();
                }
                else if (serial.Length == 3)
                {
                    serial =  serial.ToString();
                }
                //
                serial = produto + serial;//GetProduto(produto) + serial;

            }
            else
            {
                serial = produto + serial + "0001";//GetProduto(produto) + "0001";

            }
            //
            return serial;
        }
    }
}