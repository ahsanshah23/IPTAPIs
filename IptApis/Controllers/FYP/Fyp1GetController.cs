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
    public class Fyp1GetController : ApiController

        //Proposal
    {
        public HttpResponseMessage GetProposalsName()
        {
            var db = DbUtils.GetDBConnection();   
            db.Connection.Open();
            
            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypProposal").Select("ProjectTitle", "ProposalID").Where("SupervisorID" , 123).OrWhere("CoSupervisorID", 1234).Get().Cast<IDictionary<string, object>>();
            
            
            return Request.CreateResponse(HttpStatusCode.OK, response);   
        }

        public HttpResponseMessage GetProposalDetailsByTitle()
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypProposal").Select("ProposalID", "ProjectTitle", "ProjectType", "Abstract", "SupervisorID", "CoSupervisorID").Where("ProjectTitle", "EasySafar").Get().Cast<IDictionary<string, object>>();


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public HttpResponseMessage GetProposalDetails()
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;

            response = db.Query("FypProposal").Select("ProjectTitle", "ProjectType", "Abstract", "SupervisorID", "CoSupervisorID", "LeaderID", "Member1ID", "Member2ID")
               .Where("LeaderID", 163942).OrWhere("Member1ID", 163942).OrWhere("Member2ID", 163942)
               .Get()
               .Cast<IDictionary<string, object>>();

         
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public HttpResponseMessage GetProposalsNameSupervisor()
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;

            response = db.Query("FypProposal").Select("ProjectTitle", "ProjectType", "Abstract", "SupervisorID", "CoSupervisorID", "LeaderID", "Member1ID", "Member2ID")
               .Where("SupervisorID", 123).OrWhere("CoSupervisorID", 123)
               .Get()
               .Cast<IDictionary<string, object>>();


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //General

        public HttpResponseMessage GetMaxStudents()
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypConfig").Select("MaxStudent", "MinStudent").Get().Cast<IDictionary<string, object>>();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public HttpResponseMessage GetFypNames()
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("Fyp").Select("Fyp.ProjectName", "Fyp.FypID").Where("FypJury.EmpID", 6466).Join("FypJury", "Fyp.FypID", "FypJury.FypID").Get().Cast<IDictionary<string, object>>();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public HttpResponseMessage GetFypDetailsByTitle()
        {
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("Fyp").Select("Fyp.CoSuperVisorID", "Fyp.SupervisorEmpID", "FypMembers.StudentID").Where("FypMembers.FypID", 1).Join("FypMembers", "Fyp.FypID", "FypMembers.FypID").Get().Cast<IDictionary<string, object>>();
            
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        //ProposalEvaluation

        public HttpResponseMessage GetProposalEvaluations()
        {
            var db = DbUtils.GetDBConnection();   
            db.Connection.Open();
           
            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypEvaluation").Where("FormID",1).Get().Cast<IDictionary<string, object>>();   
            
            return Request.CreateResponse(HttpStatusCode.OK, response); 
        }


        //FinalEvaluation

        public HttpResponseMessage GetFinalEvaluations()
        {
            var db = DbUtils.GetDBConnection(); 
            db.Connection.Open();
          
            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypEvaluation").Where("FormID", 2).Get().Cast<IDictionary<string, object>>();  
            
            return Request.CreateResponse(HttpStatusCode.OK, response);   
        }
    }
}