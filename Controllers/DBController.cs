using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using Newtonsoft.Json.Linq;
using RESTfulAPIClient;
using System.Net;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace DBAPIServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DBController : BaseCtrl
    {

        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static object lockObj = new();

        [HttpGet("{spName}/{req_seq}")]
        public string Get(string spName, string req_seq)
        {
            log.Info($"Get API Called. Procedure : {spName}. Parameters : {req_seq}");

            JObject result = new();
            try
            {
                DataSet ds = GetData(spName, req_seq);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result.Add("RESULT", "OK");
                    result.Add("VALUES", JArray.FromObject(ds.Tables[0]));
                }
                else
                {
                    result.Add("RESULT", "NG");
                    result.Add("VALUES", "No Data");
                }
            }
            catch (Exception ex)
            {
                result.Add("RESULT", "NG");
                result.Add("VALUES", ex.ToString());
                log.Error($"Get API Exception", ex);
            }

            log.Info($"Procedure {spName} Result : {result}");
            return result.ToString();
        }

        [HttpPost("{spName}")]
        public string Post(string spName, [FromBody] string value)
        {
            log.Info($"Post API Called. Procedure : {spName}. Parameters : {value}");

            DataSet bResult = GetData(spName, value);

            string result = @"{'RESULT' : '" + bResult.Tables[0].Rows[0].ItemArray[0] + "'}";

            log.Info($"Put API Called. Procedure : {spName}. Parameters : {value}. RESULT : {result}");
            return result;
        }

   
        [HttpPut("{spName}")]
        public string Put(string spName, [FromBody] string value)
        {
            log.Info($"Put API Called. Procedure : {spName}. Parameters : {value}");

            //DataSet bResult = DBData.MergeData(spName, value);
            DataSet bResult = GetData(spName, value);

            string result = @"{'RESULT' : '" + bResult.Tables[0].Rows[0].ItemArray[0] + "'}";

            log.Info($"Put API Called. Procedure : {spName}. Parameters : {value}. RESULT : {result}");
            return result;
        }

        [HttpDelete("{spName}")]
        public string Delete(string spName, [FromBody] string value)
        {
            log.Info($"Delete API Called. Procedure : {spName}. Parameters : {value}");

            DataSet bResult = GetData(spName, value);

            string result = @"{'RESULT' : '" + bResult.Tables[0].Rows[0].ItemArray[0] + "'}";

            log.Info($"Put API Called. Procedure : {spName}. Parameters : {value}. RESULT : {result}");
            return result;
        }
    }
}
