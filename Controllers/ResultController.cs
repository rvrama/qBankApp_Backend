using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstQnAAPI.Data;
using Microsoft.AspNetCore.Cors;
using NuGet.Packaging;

namespace FirstQnAAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class QResultController : ControllerBase
    {
        private readonly FirstQnAAPIContext _context;

        public QResultController(FirstQnAAPIContext context)
        {
            _context = context;
        }

        // GET: api/Result?userId={userId}
        [HttpGet]
        public ActionResult<List<UserResult>> GetQResult(string userId)
        {

            // _context.QResult.Join(_context.QWiseResult, 
            //     q => q.UserId, qwr => qwr.UserId, (q, qwr) => new { q, qwr })
            //     .Where(a => (a.q.UserId == userId && a.q.GroupId == a.qwr.GroupId && a.q.AttemptId == a.qwr.AttemptId))
            //     .Select(a => new { a.q.UserId, a.q.GroupId, a.q.Score, a.q.TimeSpent, a.qwr.QuestionId, a.qwr.AnswerChoiceId, a.qwr.SelectedChoiceId })
            //     .ToList();

            List<QResult> qrList = [.. _context.QResult.Where<QResult>(a => a.UserId == userId)];

            List<UserResult> userResultsArray = new List<UserResult>();

            if (qrList.Count > 0)
            {
                foreach (var qr in qrList)
                {
                    userResultsArray.Add(new UserResult()
                    {
                        UserId = qr.UserId,
                        GroupId = qr.GroupId,
                        Score = qr.Score,
                        TimeSpent = qr.TimeSpent,
                        AttemptId = qr.AttemptId,
                        Results = GetQuestionWiseResults(userId, qr.GroupId, qr.AttemptId).ToArray()
                    });

                }
            };

            return userResultsArray;

        }

        private List<QuestionWiseResult> GetQuestionWiseResults(string userId, int groupId, int attemptId)
        {
            List<QWiseResult> res = _context.QWiseResult.Where<QWiseResult>(q => q.UserId == userId
                                             && q.GroupId == groupId && q.AttemptId == attemptId).ToList();
            List<QuestionWiseResult> qw = new List<QuestionWiseResult>();
            foreach (var n in res)
            {
                var x = new QuestionWiseResult()
                {
                    id = n.QuestionId,
                    answer = n.AnswerChoiceId,
                    selected = n.SelectedChoiceId
                };
                qw.Add(x);
            }

            return qw;
        }

        // POST: api/QResult
        [HttpPost]
        public ActionResult<UserResult> PostQResult(UserResult  results)
        {
            var local_userId = results.UserId;
            var local_groupId = results.GroupId;
            var local_attemptId = _context.QResult.Where(a => a.UserId == results.UserId
                                          && a.GroupId== results.GroupId).Count() + 1;

                    QResult qr = new QResult() {
                        UserId = results.UserId,
                        AttemptId = local_attemptId,
                        GroupId = results.GroupId,
                        Score = results.Score,
                        TimeSpent = results.TimeSpent
                    };

                    _context.QResult.Add(qr);

                    List<QWiseResult> qw = new List<QWiseResult>();
                    
                    foreach (QuestionWiseResult q in results.Results) {
                        qw.Add(new QWiseResult() {
                            UserId = local_userId,  //not used in response payload
                            GroupId = local_groupId, //not used in response payload
                            AttemptId = local_attemptId, //not used in response payload
                            QuestionId = q.id,
                            AnswerChoiceId = q.answer,
                            SelectedChoiceId = q.selected
                    });
                    }

                _context.QWiseResult.AddRange(qw);

                _context.SaveChanges();
             //   }

            // else {

            //         QuestionWiseResult[] qwResult = new QuestionWiseResult[qWiseRessultExist.Count];
            //         var index = 0;
            //         foreach (var a in qWiseRessultExist){
            //             qwResult[index] = 
            //             new QuestionWiseResult {
            //                 id=a.QuestionId,
            //                 answer=a.AnswerChoiceId,
            //                 selected=a.SelectedChoiceId
            //             };
            //             index++;
            //         }

            //         results = new UserResult() {
            //             GroupId = qResultExist.GroupId,
            //             //id = 1, //attemptId hardcoded for now
            //             UserId = qResultExist.UserId,
            //             Score = qResultExist.Score,
            //             TimeSpent = qResultExist.TimeSpent,
            //             Results = qwResult
            //         };             

            // }
        
          return CreatedAtAction("GetQResult", new { userId = results.UserId }, results);
        }

        //// DELETE: api/Questions/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteQuestion(int id)
        //{
        //    var question = await _context.Question.FindAsync(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Question.Remove(question);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
