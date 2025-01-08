using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using QuestionBankApp.Data;

namespace QuestionBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class QuestionsListController(QuestionBankAppContext context) : ControllerBase
    {
        private readonly QuestionBankAppContext _context = context;

        // GET: api/QuestionsList
        [HttpGet]
        public ActionResult<List<QuestionList>> GetQuestionsList()
        {
            //Instead of pulling the data from the Question and Choices table, 
            //we are pulling the data from the SQL View called "QuestionAndChoicesView"
            var qmList = _context.QuestionLists.ToList();
            return qmList;

        }

    
    }
}
