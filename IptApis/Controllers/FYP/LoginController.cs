using IptApis.Shared;
using Newtonsoft.Json;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IptApis.Controllers.FYP
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage login([FromUri] string rollNo, string password)
        {
            string _StudentID = Convert.ToString(rollNo);
            string _StudentPassword = Convert.ToString(password);

            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;

            response = db.Query("Student").Where(new
            {
                RollNumber = _StudentID,
                SPassword = _StudentPassword,
            })
            .AsCount().Get().Cast<IDictionary<string, object>>();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

