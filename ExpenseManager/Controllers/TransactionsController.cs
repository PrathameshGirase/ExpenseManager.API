using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.Data;
using ExpenseManager.Contracts;
using AutoMapper;
using ExpenseManager.Models.Transaction;

namespace ExpenseManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
       
        private readonly ITransactionRepository transactioRepository;
        private readonly IMapper mapper;

        public TransactionsController(ITransactionRepository transactionRepository, IMapper mapper)
        {
            
            this.transactioRepository = transactionRepository;
            this.mapper = mapper;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var transaction = await transactioRepository.GetAllAsync();
            return Ok(mapper.Map<List<TransactionDto>>(transaction));
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await transactioRepository.GetAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TransactionDto>(transaction));
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionDto transactionDto)
        {
            if (id != transactionDto.Id)
            {
                return BadRequest();
            }

            var transaction = await transactioRepository.GetAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            mapper.Map(transactionDto, transaction);

            try
            {
                await transactioRepository.UpdateAsync(transaction);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(CreateTransactionDto transactionDto)
        {
            var transaction = mapper.Map<Transaction>(transactionDto);
            await transactioRepository.AddAsync(transaction);

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await transactioRepository.GetAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            await transactioRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool>  TransactionExists(int id)
        {
            return await transactioRepository.Exists(id);
        }
    }
}
