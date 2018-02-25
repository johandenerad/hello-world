using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Loans.Models;
using System.Web.Http.Cors;

namespace Loans.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Produces("application/json")]
    [Route("api/Loans/")]
    public class LoansController : Controller
    {
        // GET: api/Loans
        public ApiContext _context;

        public LoansController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Loan> Get()
        {
            return _context.Loans.ToList();
        }


        // GET: api/Loans/5
        [HttpGet("{id}", Name = "Get")]
        public Loan Get(int id)
        {
            return _context.Loans.FirstOrDefault(x => x.Id == id);
        }
        
        // POST: api/Loans
        [HttpPost]
        public void Post([FromBody]Loan loan)
        {
            _context.Loans.Add(loan);
            _context.SaveChanges();
        }
        
        // PUT: api/Loans/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var toGo = _context.Loans.FirstOrDefault(x => x.Id == id);
            _context.Loans.Remove(toGo);
            _context.SaveChanges();
        }
    }
}
