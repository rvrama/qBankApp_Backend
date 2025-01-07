using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using FirstQnAAPI;
using FirstQnAAPI.Data;

namespace FirstQnAAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class QuestionsListController(FirstQnAAPIContext context) : ControllerBase
    {
        private readonly FirstQnAAPIContext _context = context;

        // GET: api/QuestionsList
        [HttpGet]
        public ActionResult<List<QuestionList>> GetQuestionsList()
        {
            List<QuestionList> qmList = [];
            var questionList = _context.QBMaster.ToList();
            foreach (QBMaster a in questionList)
            {
                List<string> choices = [.. _context.ChoicesMaster.Where<ChoicesMaster>(
                                        b => (b.QuestionId==a.QuestionId)
                                        ).Select(a => a.ChoiceText)];

                string choiceText=string.Join(",", choices);
                QuestionList qm = new()
                 {
                     questionId = a.QuestionId,
                     questionTxt = a.QuestionText,
                     groupId = a.GroupId,
                     choices = choiceText,
                     choiceType = a.ChoiceType, 
                     answerChoiceId = a.AnswerChoiceId 
                 };
                 qmList.Add(qm);
            }
            return qmList;

        }

    
    }
}
