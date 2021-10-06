using Dal.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bal.Interfaces
{
    public interface IBillBL
    {
        //הצגת רשימת כל החשבוניות 
        Task<List<Bill>> GetAllBiils(int userId);
        //הוספה
        Task AddBill(Bill bill);
        //עדכון
        Task UpdateBill(int id,Bill bill);
        //מחיקה
        Task DeleteBill(int id);
        //הצגת רשימת חשבוניות לפי קטגוריה מסוימת 
        Task<List<Bill>> GetBillsByKategory(int CategoryId);
    }
}
