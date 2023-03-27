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
using System.Drawing.Printing;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using PdfSharpCore;

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

        [HttpGet("generatepdf")]
        public async Task<IActionResult> GeneratePDF(string i_month,  string i_year)
        {

            var document = new PdfDocument();

            String date = i_month.ToLower();
            Dictionary<string, string> monthList = new Dictionary<string, string>();
            monthList.Add("january", "01");
            monthList.Add("february", "02");
            monthList.Add("march", "03");
            monthList.Add("april", "04");
            monthList.Add("may", "05");
            monthList.Add("june", "06");
            monthList.Add("july", "07");
            monthList.Add("august", "08");
            monthList.Add("sepetember", "09");
            monthList.Add("october", "10");
            monthList.Add("november", "11");
            monthList.Add("december", "12");


            string[] copies = { "Igmite Solutions" };
            for (int i = 0; i < copies.Length; i++)
            {
                var transaction = await transactioRepository.GetAllAsync();
               
                string htmlcontent = "<div style='width:100%; text-align:center'>";
               
                htmlcontent += "<h2>" + copies[i] + "</h2>";
                htmlcontent += "<h2>TrackME Monthly Report</h2>";



                if (transaction != null)
                {
                    htmlcontent += "<h2>Month: "+date.ToUpper()+" "+i_year+"</h2>";              
                    htmlcontent += "<p>" + "Unit 102, Rajhans Annex, Thane(West), Mumbai- 400 602" + "</p>";
                    htmlcontent += "<h3> Contact : +91 9833163040 & Email : support@igmite.in </h3>";
                    htmlcontent += "<div>";
                }



                htmlcontent += "<table style ='width:100%; border: 1px solid #000'>";
                htmlcontent += "<thead style='font-weight:bold'>";
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border:1px solid #000'> Date </td>";
                htmlcontent += "<td style='border:1px solid #000'> Title </td>";
                htmlcontent += "<td style='border:1px solid #000'>Category</td>";
                htmlcontent += "<td style='border:1px solid #000'>Due Amount</td >";
               
                htmlcontent += "</tr>";
                htmlcontent += "</thead >";

                String[] category = { "Food And Beverages", "Pantry", "Stationary", "Travel", "Staff", "Others",};
                double due_amt = 0;

                htmlcontent += "<tbody>";
                if (transaction != null && transaction.Count > 0)
                {
                    transaction.ForEach(item =>
                    {
                        String curr_date = item.Date;
                        String[] date_s = curr_date.Split("-");
                        string mnth = date_s[1];
                        string yr = date_s[0];
                        if (item.CategoryId != 7 && mnth == monthList[date] && yr == i_year && item.TransactionTypeId != 2)
                        {
               
                            htmlcontent += "<tr>";
                            htmlcontent += "<td>" + item.Date+"</td>";
                            htmlcontent += "<td>" + item.Name + "</td>";
                            htmlcontent += "<td>" + category[item.CategoryId-1] + "</td >";
                            htmlcontent += "<td>" + item.Amount + "</td>";
                            htmlcontent += "</tr>";
                            due_amt += item.Amount;
                        }
                        
                    });
                }
                htmlcontent += "</tbody>";

                htmlcontent += "</table>";
                htmlcontent += "</div>";

                htmlcontent += "<div style='text-align:right'>";
                htmlcontent += "<h3>"+ "Total Due Amount: &#x20b9;" + due_amt+ " </h3>";
                htmlcontent += "</div>";
                htmlcontent += "</div>";

                PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);
            }
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string Filename = "Invoice_" + date+ i_year + ".pdf";
          
            return File(response, "application/pdf", Filename);
        }
    }
}
