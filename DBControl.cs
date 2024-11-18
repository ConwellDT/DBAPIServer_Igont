using Microsoft.Extensions.Configuration;
using CW.Database;

namespace DBAPIServer
{
    internal static class DBControl
    {
        private static DBHelper _DB;

        public static IConfiguration Configuration;
        public static void Init(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public static DBHelper DB
        {
            get
            {
                if (_DB == null)
                {
                    //Config config = Configuration.GetConnectionString("DbCon");

                    string connectString = Configuration.GetConnectionString("DbCon");

                    _DB = DBHelper.CreateConnection(ProviderKind.MSSQL, connectString);
                    
                    //_DB.IsEncrypted = true;
                }
                return _DB;
            }

        }

        //public static T AppValue<T>(string SettingName)
        //{
        //    if (string.IsNullOrWhiteSpace(App[SettingName])) return default(T);

        //    T result = default(T);

        //    string tempValue = App[SettingName];
        //    string tempUpperValue = tempValue.ToUpper();

        //    switch (typeof(T).Name)
        //    {
        //        case "Boolean": //N, F, false 가 아니면 참
        //            return (T)(object)!(tempUpperValue == "N" || tempUpperValue == "FALSE" || tempUpperValue == "F");

        //        case "Double":
        //            return (T)(object)double.Parse(tempValue);

        //        default:
        //            return (T)(object)tempValue;
        //    }
        //}

        
    }
}