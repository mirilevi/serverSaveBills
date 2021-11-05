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
        //TODO
        public async Task AddBillAsync(Bill bill)
        {
            db.Bills.Add(bill);
            await db.SaveChangesAsync();
        }

        public async Task DeleteBillAsync(int id)
        {
            db.Bills.Remove(db.Bills.FirstOrDefault(b => b.BillId == id));
            await db.SaveChangesAsync();
        }
        //return all bills
        public async Task<List<Bill>> GetAllBillsAsync(int userId)
        {
            return await db.Bills.Where(b=>b.UserId==userId).ToListAsync();
        }

        public async Task<List<string>> GetAllStoresNames()
        {
            return await db.Bills.GroupBy(b => b.StoreName).Select(s => s.Key).ToListAsync();
        }

        //return all bills by specific category
        public async Task<List<Bill>> GetBillsByCategoryAsync(int userId,int CategoryId)
        {
            return await db.Bills.Where(b=> b.UserId == userId && b.BillCategories.Any(c=>c.CategoryId==CategoryId)).ToListAsync();
        }
        //return all bills by storeName
        public async Task<List<Bill>> GetBillsByStoreName(int userId, string storeName)
        {
            return await db.Bills.Where(b => b.UserId == userId && b.StoreName==storeName).ToListAsync();
        }
        //Update Bill
        public async Task UpdateBillAsync(int id,Bill bill)
        {
            Bill bToEdit = await db.Bills.FirstOrDefaultAsync(b =>b.BillId  == id);
            if (bToEdit != null)
            {
                bToEdit.UserId = bill.UserId;
                bToEdit.Products = bill.Products;
                bToEdit.StoreName = bill.StoreName;
                bToEdit.IssueDate = bill.IssueDate;
                bToEdit.ExpiryDate = bill.ExpiryDate;
                bToEdit.ImgBill = bill.ImgBill;
                await db.SaveChangesAsync();
            }
        }



    }
}
