using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Classes
{
    public class BillDL : IBillDL
    {
        SaveBillsContext db;
        public BillDL(SaveBillsContext _db)
        {
            db = _db;
        }
        public async Task AddBillAsync(Bill bill)
        {
            db.Add(bill);
            await db.SaveChangesAsync();
        }

        public async Task DeleteBillAsync(int id)
        {
            db.Bills.Remove(db.Bills.FirstOrDefault(b => b.BillId == id));
            await db.SaveChangesAsync();
        }
        //return all bills
        public async Task<List<Bill>> GetAllBiilsAsync(int userId)
        {
            return await db.Bills.Where(b=>b.UserId==userId).ToListAsync();
        }
        //return all bills by specific category
        public async Task<List<Bill>> GetBillsByKategoryAsync(int CategoryId)
        {
            return await db.Bills.Where(bill => bill.Category == CategoryId).ToListAsync();
        }
        //Update Bill
        public async Task UpdateBillAsync(int id,Bill bill)
        {
            Bill bToEdit = await db.Bills.FirstOrDefaultAsync(b =>b.BillId  == id);
            if (bToEdit != null)
            {
                bToEdit.UserId = bill.UserId;
                bToEdit.ProductName = bill.ProductName;
                bToEdit.StoreName = bill.StoreName;
                bToEdit.IssueDate = bill.IssueDate;
                bToEdit.ExpiryDate = bill.ExpiryDate;
                bToEdit.Category = bill.Category;
                bToEdit.ImgBiil = bill.ImgBiil;
                await db.SaveChangesAsync();
            }
        }
    }
}
