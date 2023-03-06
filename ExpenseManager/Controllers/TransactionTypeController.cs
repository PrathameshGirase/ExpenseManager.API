using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.Data;
using ExpenseManager.Models.Transaction_Type;
using AutoMapper;
using ExpenseManager.Models.Category;
using Microsoft.AspNetCore.Http.HttpResults;
using ExpenseManager.Contracts;

namespace ExpenseManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly ITransactionTypeRepository transactionTypeRepository;

        public TransactionTypeController(IMapper mapper, ITransactionTypeRepository transactionTypeRepository )
        {
           
            this._mapper = mapper;
            this.transactionTypeRepository = transactionTypeRepository;
        }

        // GET: api/Transaction_Type
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTransactionTypeDto>>> GetTransactionTypes()
        {
            var result = await transactionTypeRepository.GetAllAsync();
            var record = _mapper.Map<List<GetTransactionTypeDto>>(result);
            return Ok(record);
        }

        // GET: api/Transaction_Type/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionTypeDto>> GetTransaction_Type(int id)
        {
            var TransactionType = await transactionTypeRepository.GetDetails(id);

            if (TransactionType == null)
            {
                return NotFound();
            }
            var record = _mapper.Map<TransactionTypeDto>(TransactionType);
            return Ok(record);
        }
        /*
        // PUT: api/Transaction_Type/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction_Type(int id, Transaction_Type transaction_Type)
        {
            if (id != transaction_Type.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaction_Type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Transaction_TypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transaction_Type
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction_Type>> PostTransaction_Type(Transaction_Type transaction_Type)
        {
            _context.TransactionTypes.Add(transaction_Type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction_Type", new { id = transaction_Type.Id }, transaction_Type);
        }

        // DELETE: api/Transaction_Type/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction_Type(int id)
        {
            var transaction_Type = await _context.TransactionTypes.FindAsync(id);
            if (transaction_Type == null)
            {
                return NotFound();
            }

            _context.TransactionTypes.Remove(transaction_Type);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
        /*private bool Transaction_TypeExists(int id)
        {
            return _context.TransactionTypes.Any(e => e.Id == id);
        }*/
    }
}
