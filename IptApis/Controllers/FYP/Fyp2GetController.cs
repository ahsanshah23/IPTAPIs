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
        public HttpResponseMessage GetMidEvaluationByID([FromUri] int id)
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("Fyp").Select("Fyp.ProjectName", "FypMembers.LeaderID", "FypMembers.Member1ID", "FypMembers.Member2ID"
                , "Fyp.SuperVisorEmpID", "Fyp.CoSuperVisorID", "ProjectProgress", "DocumentationStatus", "ProgressComments")
                .Where("FypEvaluation.FormID", 3).Where("FypMembers.LeaderID", id).OrWhere("FypEvaluation.FormID", 3)
                .Where("FypMembers.Member1ID", id).OrWhere("FypEvaluation.FormID", 3).Where("FypMembers.Member2ID", id)
                .Join("FypEvaluation", "Fyp.FypID", "FypEvaluation.FypID")
                .Join("FypMembers", "Fyp.FypID", "FypMembers.FypID")
                .Get().Cast<IDictionary<string, object>>();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public HttpResponseMessage GetFinalEvaluationByID([FromUri] int id)
        {

            var db = DbUtils.GetDBConnection();
            db.Connection.Open();
            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypEvaluation").Where("FormID", 4).Where("LeaderID", id).OrWhere("FormID", 4)
                .Where("Member1ID", id).OrWhere("FormID", 4).Where("Member2ID", id)
                .Get().Cast<IDictionary<string, object>>();  
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

        public HttpResponseMessage GetFypNamesExternalJury([FromUri] string username)
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("Fyp").Select("Fyp.ProjectName", "Fyp.FypID").Where("FypExternalJuryAllocation.Username", username).Join("FypExternalJuryAllocation", "Fyp.FypID", "FypExternalJuryAllocation.FypID").Get().Cast<IDictionary<string, object>>();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage Fyp2MidStatus([FromUri] int id)
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypEvaluation").Where("FypID", id).Where("FormID", 3).AsCount()
                .Get().Cast<IDictionary<string, object>>();


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage Fyp2FinalInternalStatus([FromUri] int id)
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypMarks").Where("StudentID", id).Where("FormID", 4).AsCount()
                .Get().Cast<IDictionary<string, object>>();


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage Fyp2FinalExternalStatus([FromUri] int id)
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypMarks").Where("StudentID", id).Where("FormID", 5).AsCount()
                .Get().Cast<IDictionary<string, object>>();


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


    }
}