using AutoMapper;
using ExpenseManager.Contracts;
using ExpenseManager.Data;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using MailKit.Net.Smtp;

namespace ExpenseManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        
        private readonly ITransactionRepository transactioRepository;
        private readonly IMapper mapper;



        public HangfireController(ITransactionRepository transactioRepository, IMapper mapper)
        {

            this.transactioRepository = transactioRepository;
            this.mapper = mapper;
        }



        [HttpPost]
        [Route("Invoice")]
        public async Task<IActionResult> Invoice(String Username, String Password, String Host, int Port_No, String ToEmail, String Subject, String Body)
        {
            


            
            DateTime today = DateTime.Today;
            string i_today = today.ToString("yyyy-MM-dd");
            string[] arr_today = i_today.Split('-');
            int n_month = Int32.Parse(arr_today[1]);
            n_month = n_month - 1;
            string i_year = arr_today[0];
            string i_month = "0"+n_month.ToString();
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Username));
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = Subject;
            var builder = new BodyBuilder();
            var document = new PdfDocument();

            
            Dictionary<string, string> monthList = new Dictionary<string, string>();
            monthList.Add("01","january");
            monthList.Add("02","february");
            monthList.Add("03","march");
            monthList.Add("04", "april");
            monthList.Add("05", "may");
            monthList.Add("06", "june");
            monthList.Add("07", "july");
            monthList.Add("08", "august");
            monthList.Add("09", "sepetember");
            monthList.Add("10", "october");
            monthList.Add("11", "november");
            monthList.Add("12", "december");


            string[] copies = { "Igmite Solutions" };
            for (int i = 0; i < copies.Length; i++)
            {
                var transaction = await transactioRepository.GetAllAsync();

                string htmlcontent = "<div style='width:100%; text-align:center'>";

                htmlcontent += "<h2>" + copies[i] + "</h2>";
                htmlcontent += "<h2>TrackME Monthly Report</h2>";



                if (transaction != null)
                {
                    htmlcontent += "<h2>Month: " + monthList[i_month].ToUpper() + " " + i_year + "</h2>";
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

                String[] category = { "Food And Beverages", "Pantry", "Stationary", "Travel", "Staff", "Others", };
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
                        if (item.CategoryId != 7 && mnth == i_month && yr == i_year && item.TransactionTypeId!=2)
                        {

                            htmlcontent += "<tr>";
                            htmlcontent += "<td>" + item.Date + "</td>";
                            htmlcontent += "<td>" + item.Name + "</td>";
                            htmlcontent += "<td>" + category[item.CategoryId - 1] + "</td >";
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
                htmlcontent += "<h3>" + "Total Due Amount: &#x20b9;" + due_amt + " </h3>";
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
            string Filename = "Invoice_" + monthList[i_month] + i_year + ".pdf";
            builder.Attachments.Add(Filename, response);
            builder.HtmlBody = Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(Host, Port_No, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(Username, Password);
            smtp.Send(email);
            smtp.Disconnect(true);

            RecurringJob.AddOrUpdate(() => Invoice(Username,Password,Host,Port_No,ToEmail,Subject,Body), Cron.Minutely());
            return File(response, "application/pdf", Filename);

        }

        /*[HttpPost]
        [Route("sendmail")]
        public IActionResult SendEmail(String Username, String Password, String Host, int Port_No, String ToEmail, String Subject, String Body)
        {




            DateTime today = DateTime.Today;
            string i_today = today.ToString("yyyy-MM-dd");
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Username));
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = Subject;
            var builder = new BodyBuilder();
            
            email.Body = builder.ToMessageBody();
            builder.HtmlBody = Body;
            using var smtp = new SmtpClient();
            smtp.Connect(Host, Port_No, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(Username, Password);
            smtp.Send(email);
            smtp.Disconnect(true);

            RecurringJob.AddOrUpdate(() => Invoice(Username, Password, Host, Port_No, ToEmail, Subject, Body), Cron.Minutely());
            return Ok();

        }*/


    }
    }
