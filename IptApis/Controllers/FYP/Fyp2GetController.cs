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
    public class Fyp2GetController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage GetMidEvaluationByID()
        {

            var db = DbUtils.GetDBConnection();
            db.Connection.Open();
            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypEvaluation").Where("FormID", 3).Get().Cast<IDictionary<string, object>>();  
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public HttpResponseMessage GetFinalEvaluationByID()
        {

            var db = DbUtils.GetDBConnection();
            db.Connection.Open();
            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypEvaluation").Where("FormID", 4).Get().Cast<IDictionary<string, object>>();  
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public HttpResponseMessage GetFinalExternalEvaluationByID()
        {

            var db = DbUtils.GetDBConnection();
            db.Connection.Open();
            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypEvaluation").Where("FormID", 5).Get().Cast<IDictionary<string, object>>();  
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}