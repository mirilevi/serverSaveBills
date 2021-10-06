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
        Task<List<Bill>> GetAllBiilsAsync(int userId);
        //הוספה
        Task AddBillAsync(Bill bill);
        //עדכון
        Task UpdateBillAsync(int id,Bill bill);
        //מחיקה
        Task DeleteBillAsync(int id);
        //הצגת רשימת חשבוניות לפי קטגוריה מסוימת 
        Task<List<Bill>> GetBillsByKategoryAsync(int CategoryId);

    }
}
