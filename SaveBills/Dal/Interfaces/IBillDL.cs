using Dal.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IBillDL
    {
        //הצגת רשימת כל החשבוניות 
        Task<List<Bill>> GetAllBillsAsync(int userId);
        //הצגת רשימת חשבוניות לפי קטגוריה מסוימת 
        Task<List<Bill>> GetBillsByCategoryAsync(int userId,int CategoryId);
        //הצגת רשימת חשבוניות לפי חנות מסוימת 
        Task<List<Bill>> GetBillsByStoreName(int userId, string storeName);

        Task<List<string>> GetAllStoresNames();
        //הוספה
        Task AddBillAsync(Bill bill);
        //עדכון
        Task UpdateBillAsync(int id,Bill bill);
        //מחיקה
        Task DeleteBillAsync(int id);
       

    }
}
