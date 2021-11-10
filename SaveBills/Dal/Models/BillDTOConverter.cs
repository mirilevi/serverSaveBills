using Dal.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public static class BillDTOConverter
    {
        public static BillDTO ConvertToDTO(this Bill b)
        {
            return new BillDTO() {
                BillCategories = b.BillCategories.Select(it => it.Category).ToList(),
                BillId = b.BillId,
                BillTxt = b.BillTxt,
                ExpiryDate = b.ExpiryDate,
                ImgBill = b.ImgBill,
                IssueDate = b.IssueDate,
                Products = b.Products,
                StoreName = b.StoreName,
                TotalSum = b.TotalSum,
                UserId = b.UserId
            };
        }
        public static Bill ConvertFromDTO(this BillDTO b)
        {
            return new Bill()
            {
                BillCategories = b.BillCategories.Select(c => new BillCategory() {  BillId = b.BillId,CategoryId=c.CategoryId}).ToList(),
                BillId = b.BillId,
                BillTxt = b.BillTxt,
                ExpiryDate =(DateTime) b.ExpiryDate,
                ImgBill = b.ImgBill,
                IssueDate = b.IssueDate,
                Products = b.Products,
                StoreName = b.StoreName,
                TotalSum = b.TotalSum,
                UserId = b.UserId
            };
        }

        public static List<BillDTO> convertListToDTO(this List<Bill> bills)
        {
            return bills.Select(b => b.ConvertToDTO()).ToList();
        }

        public static List<Bill> convertListFormDTO(this List<BillDTO> bills)
        {
            return bills.Select(b => b.ConvertFromDTO()).ToList();
        }
    }
}
