using Newtonsoft.Json;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Transactions;
using IptApis.Shared;
using SqlKata;
using SqlKata.Compilers;
using System.Linq;

namespace IptApis.Controllers.FYP
{
    public class Fyp2PostController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddMidEvaluationJury(Object MidEvaluation)
        {

            var test = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(Convert.ToString(MidEvaluation));

            object FypID;
            test.TryGetValue("FypID", out FypID);
            int _FypID = Convert.ToInt32(FypID);

            object FormID;
            test.TryGetValue("FormID", out FormID);
            int _FormID = Convert.ToInt32(FormID);
            
            object ProjectProgress;
            test.TryGetValue("ProjectProgress", out ProjectProgress);
            int _ProjectProgress = Convert.ToInt32(ProjectProgress);

            object DocumentationStatus;
            test.TryGetValue("DocumentationStatus", out DocumentationStatus);
            int _DocumentationStatus = Convert.ToInt32(DocumentationStatus);

            object ProgressComments;
            test.TryGetValue("ProgressComments", out ProgressComments);
            string _ProgressComments = Convert.ToString(ProgressComments);

            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    var res = db.Query("FypEvaluation").InsertGetId<int>(new
                    {
                        FypID = _FypID,
                        FormID = _FormID,

                        ProjectProgress = _ProjectProgress,
                        DocumentationStatus = _DocumentationStatus,
                        ProgressComments = _ProgressComments
                    });
                    
                    scope.Complete();
                    db.Connection.Close();
                    return Request.CreateResponse(HttpStatusCode.Created, new Dictionary<string, object>() { { "LastInsertedId", res } });
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

            }
        }

        [HttpPost]
        public HttpResponseMessage AddFinalEvaluationJury(Object FinalEvaluation)
        {

            var test = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(Convert.ToString(FinalEvaluation));

            object FypID;
            test.TryGetValue("FypID", out FypID);
            int _FypID = Convert.ToInt32(FypID);

            object FormID;
            test.TryGetValue("FormID", out FormID);
            int _FormID = Convert.ToInt32(FormID);
            
            var db = DbUtils.GetDBConnection();
            db.Connection.Open();

            IEnumerable<IDictionary<string, object>> response;
            response = db.Query("FypConfig").Select("MaxStudent").Get().Cast<IDictionary<string, object>>();

            var strResponse = response.ElementAt(0).ToString().Replace("DapperRow,", "").Replace("=", "").Replace("MaxStudent", "").Replace("{", "").Replace("}", "").Replace("'", "");
            var result = Convert.ToInt32(strResponse);


            object[] array = new object[result];
            double[] Marks = new double[result];

            for (int i = 0; i < array.Length; i++)
            {
                test.TryGetValue("Marks" + i, out array[i]);
                Marks[i] = Convert.ToInt32(array[i]);
            }

            object[] array1 = new object[result];
            int[] ID = new int[result];

            for (int i = 0; i < array1.Length; i++)
            {
                test.TryGetValue("Student" + i + "ID", out array[i]);
                ID[i] = Convert.ToInt32(array[i]);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    for (int i = 0; i < Marks.Length; i++)
                    {
                        db.Query("FypMarks").Insert(new
                        {
                            Marks = Marks[i],
                            FormID = _FormID,
                            StudentID = ID[i]

                        });
                    }


                    scope.Complete();
                    db.Connection.Close();
                    return Request.CreateResponse(HttpStatusCode.Created, new Dictionary<string, object>() { { "Marks Updated", 0 } });
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

            }
        }
    }
}