using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Dal.Classes
{
    public class ExpireBillsDL : IExpireBillsDL
    {
        SaveBillsContext db;
        public ExpireBillsDL(SaveBillsContext _db)
        {
            db = _db;
        }
        public async Task<List<Bill>> GetAllExpireBillsAsync(int userId)
        {
            return await db.ExpiredBills.Select(e => e.Bill).Where(b => b.UserId == userId).ToListAsync();
        }
        public static async Task addBillsToExpireBills()
        {
            SaveBillsContext db2 = new SaveBillsContext();
            DateTime currentTime = DateTime.Now;
            List<Bill> bills = await db2.Bills.ToListAsync();
            List<Bill> expireBills = new List<Bill>();
            foreach (var bill in bills)
            {
                if(((int)(bill.ExpiryDate - currentTime).TotalDays) == 7){
                    expireBills.Add(bill);
                }
            }
            //DateTime dateTime = new DateTime(2021, 11, 11);
            //int t = ((int)(dateTime - currentTime).TotalDays);
            //try
            //{
            //     expireBills =  db2.Bills.Where(b => ((int)(b.ExpiryDate - currentTime).TotalDays)==7).ToList();
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            

            if (expireBills.Count > 0)
            {
                List<ExpiredBill> expiredBillToAdd = new List<ExpiredBill>();
                foreach (var bill in expireBills)
                {
                    expiredBillToAdd.Add(new ExpiredBill() { BillId = bill.BillId });
                    MailMessage mail = new MailMessage() {
                        From = new MailAddress("savebillsforyou@gmail.com"),
                        Subject = "your bill expired soon",
                    };
                    mail.To.Add(db2.Users.Where( u=>u.UserId==bill.UserId).Select(u=>u.Email).FirstOrDefault());
                    SmtpClient SmtpServer = new SmtpClient() {
                        Port=587,
                        Host= "smtp.gmail.com",
                        DeliveryMethod= SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("savebillsforyou@gmail.com", "save1bills"),
                        EnableSsl = true,
                    };
                    SmtpServer.Send(mail);
                }
                await db2.ExpiredBills.AddRangeAsync(expiredBillToAdd);
                await db2.SaveChangesAsync();
             
                /*MailMessage mail = new MailMessage();
                mail.From = new MailAddress("savebillsforyou@gmail.com");
                mail.To.Add("levi9741683@gmail.com");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("savebillsforyou@gmail.com", "save1bills");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);*/
               
            }
        }

       
    }
}
