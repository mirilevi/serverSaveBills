using Bal.Interfaces;
using Dal.Classes;
using Dal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Bal.Classes
{
    public class BillBL : IBillBL
    {
        IBillDL billDL;
        public BillBL(IBillDL _billDL)
        {
            billDL = _billDL;
        }
        public async Task AddBill(Bill bill)
        {
            await billDL.AddBillAsync(bill);
        }

        public async Task DeleteBill(int id)
        {
            await billDL.DeleteBillAsync(id);
        }

        public async Task<List<Bill>> GetAllBiils(int userId)
        {
            return await billDL.GetAllBiilsAsync(userId);
        }
       
        public async Task<List<Bill>> GetBillsByKategory(int CategoryId)
        {
            return await billDL.GetBillsByKategoryAsync(CategoryId);
        }

        public async Task UpdateBill(int id, Bill bill)
        {
            await billDL.UpdateBillAsync(id,bill);
        }
    }
}
