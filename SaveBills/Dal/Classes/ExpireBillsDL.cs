using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

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
        public async Task DeleteExpireAsync(int id)
        {
            db.ExpiredBills.Remove(db.ExpiredBills.FirstOrDefault(b => b.BillId == id));
            await db.SaveChangesAsync();
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
            if (expireBills.Count > 0)
            {
                List<ExpiredBill> expiredBillToAdd = new List<ExpiredBill>();
                foreach (var bill in expireBills)
                {
                    expiredBillToAdd.Add(new ExpiredBill() { BillId = bill.BillId });
                    MailMessage mail = new MailMessage() {
                        From = new MailAddress("savebillsforyou@gmail.com"),
                        Subject = "your bill expired soon",
                        Body="<font>if you want to keep this bill for more time you can do it in the site</font>"
                    };
                    mail.IsBodyHtml = true;
                    mail.To.Add(db2.Users.Where( u=>u.UserId==bill.UserId).Select(u=>u.Email).FirstOrDefault());
                    SmtpClient SmtpServer = new SmtpClient() {
                        Port=587,
                        Host= "smtp.gmail.com",
                        DeliveryMethod= SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("savebillsforyou@gmail.com", "save1bills"),
                        EnableSsl = true,
                    };
                    var tempFileName = bill.ImgBill.Substring(0, bill.ImgBill.IndexOf("?")).Remove(0, 82);
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(bill.ImgBill, tempFileName);
                    mail.Attachments.Add(new Attachment(tempFileName));

                    SmtpServer.Send(mail);
                }
                await db2.ExpiredBills.AddRangeAsync(expiredBillToAdd);
                await db2.SaveChangesAsync();
            }
        }

       
    }
}
