
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Web;
using CW.Database;
using System.Data;


namespace DBAPIServer
{
    internal static class DBData
    {
        public static DataSet GetData(string _spName, string values) //Select Procedure , return DataSet
        {


            string query = @"Exec " + _spName + " ";

            if (!string.IsNullOrWhiteSpace(values))
            {
                query += values;
            }
            
            DataSet ds = DBControl.DB.ExecuteDataSet(_spName);

            return ds;
        }

        //나중에 return 형식 변경하거나 dataset형식으로 만들어서 완료 보내주는걸로 변경
        public static DataSet MergeData(string _spName, string values) //Insert, Update, Delete Procedure , reutrn OK/NG
        {
            string query = @"Exec " + _spName + " ";

            if (!string.IsNullOrWhiteSpace(values))
            {
                query += values;
            }
            DataSet ds = DBControl.DB.ExecuteDataSet(query);

            string result = ds.Tables[0].Rows[0].ToString();

            return ds;
        }



    }
}