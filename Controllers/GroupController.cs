using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionBankApp.Data;
using Microsoft.AspNetCore.Cors;
using System.Text.RegularExpressions;

namespace QuestionBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class GroupController : ControllerBase
    {
        private readonly QuestionBankAppContext _context;

        public GroupController(QuestionBankAppContext context)
        {
            _context = context;
        }

        // GET: api/Group
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupMaster>>> GetGroup()
        {
            return await _context.GroupMaster.ToListAsync();
        }

        // GET: api/Group/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupMaster>> GetGroup(int id)
        {
            var group = await _context.GroupMaster.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

       
        private bool GroupExists(int id)
        {
            return _context.GroupMaster.Any(e => e.Id == id);
        }
    }
}
