using Dal.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bal.Interfaces
{
    public interface IBillBL
    {
        //הצגת רשימת כל החשבוניות 
        Task<List<Bill>> GetAllBiils(int userId);
        //הצגת רשימת חשבוניות לפי קטגוריה מסוימת 
        Task<List<Bill>> GetBillsByCategory(int userId,int CategoryId);
        //הצגת רשימת חשבוניות לפי חנות מסוימת 
        Task<List<Bill>> GetBillsByStoreName(int userId, string storeName);
        //הוספה
        Task AddBill(Bill bill);
        //עדכון
        Task UpdateBill(int id,Bill bill);
        //מחיקה
        Task DeleteBill(int id);
        
    }
}
