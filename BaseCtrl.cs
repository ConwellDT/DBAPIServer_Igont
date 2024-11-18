using CW.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DBAPIServer
{
    public class BaseCtrl : ControllerBase
    {
        private DBHelper _DB;
        public DBHelper DB
        {
            get
            {
                if (_DB == null)
                {
                    //Config config = Configuration.GetConnectionString("DbCon");

                    string connectString = DBControl.Configuration.GetConnectionString("DbCon");

                    _DB = DBHelper.CreateConnection(ProviderKind.MSSQL, connectString);



                    //_DB.IsEncrypted = true;
                }
                return _DB;
            }

        }

        protected DataSet GetData(string _spName, string values) //Select Procedure , return DataSet
        {
            string query = @"Exec " + _spName + " ";

            if (!string.IsNullOrWhiteSpace(values))
            {
                query += values;
            }

            DataSet ds = DB.ExecuteDataSet(query);

            return ds;
        }
    }
}
