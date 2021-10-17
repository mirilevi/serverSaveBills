﻿using System;
using IronOcr;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dal.Interfaces;

#nullable disable

namespace Dal.Classes
{
    public partial class Bill
    {
        
        //private ICategoryDL categoryDL;
        //public Bill(ICategoryDL _categoryDL)
        //{
        //    this.categoryDL = _categoryDL;
        //    BillCategories = new HashSet<BillCategory>();
        //    Produts = new HashSet<Produt>();
        //}
        public Bill()
        {
            BillCategories = new HashSet<BillCategory>();
            Produts = new HashSet<Product>();
        }

        public Bill(string filePath, string fileName):this()
        {
            string billText = BillOCR.GetBillTextFromPDF(filePath,fileName);
            this.BillTxt = billText;
            billText = SetCategories(billText);
            billText = SetDates(billText);
            billText = SetStoreName(billText);
            billText = SetTotalSum(billText);
            billText = SetProducts(billText);

        }

        public int BillId { get; set; }
        public int UserId { get; set; }
        public string StoreName { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public double? TotalSum { get; set; }
        public string ImgBiil { get; set; }
        public string BillTxt { get; set; }

        public virtual ICollection<BillCategory> BillCategories { get; set; }
        public virtual ICollection<Product> Produts { get; set; }


        private string SetDates(string billTxt)
        {
            Regex dateRxgs = new Regex(@"(\d{2}/\d{2}/\d{4}\s*\d{2}:\d{2}:\d{2}) (\d{2}\s?/\s?\d{2}\s?/\s?\d{4}\s*\d{2}\s?:\s?\d{2}\s?:\s?\d{2}) | (\d{2}/\d{2}/\d{4}) | (\d{2}\s/\s\d{2}\s/\s\d{4}) ", RegexOptions.IgnorePatternWhitespace);
            MatchCollection datesTexts = dateRxgs.Matches(billTxt);
            List<DateTime> dates;
            DateTime dHelp;
            if (datesTexts != null)
            {
                dates = new List<DateTime>();
                foreach (Match d in datesTexts)
                {
                    bool succeed = DateTime.TryParse(d.ToString(), out dHelp);
                    if (succeed)
                    {
                        if (!dates.Exists(da => da.Date.Equals(dHelp.Date)))
                        {
                            if (DateTime.Compare(DateTime.Today, dHelp) >= 0)//if this is date of today or date in the past
                            {
                                if (IssueDate == null) {
                                    IssueDate = dHelp;
                                }
                                else
                                {//if the issue date already found - put the min date as issue date and the max as expiryDate
                                    if (DateTime.Compare(dHelp, (DateTime)IssueDate) > 0)//if dHelp is bigger
                                    {
                                        ExpiryDate = dHelp;
                                    }
                                    else//IssueDate is bigger
                                    {
                                        ExpiryDate = IssueDate;
                                        IssueDate = dHelp;
                                    }
                                }
                            }
                            else //date in the future
                            {
                                ExpiryDate = dHelp;
                            }

                            dates.Add(dHelp);
                        }
                        int ind = billTxt.IndexOf(d.Value);
                        int begin = billTxt.LastIndexOf('\n', ind);
                        int end = billTxt.IndexOf('\n', ind);
                        billTxt = billTxt.Substring(0, begin) + billTxt.Substring(end, billTxt.Length - end);
                    }

                }

            }
            return billTxt;
        }

        private string SetStoreName(string billText)
        {
            //remove the concat details from the bill 
            billText = billText.RemoveLinesContains(@"((טלפון) | (פקס) | (טל) | (נייד) [\r\s:-]*\d{2,3}-?\d*)| (מייל)");
            string billNum = @"(חשבונית\s*מס'?\s*/\s*) | ('מספר\s*(קבלה)|(חשבונית)) ";
            string privateCompanyOrNot = @"((ח[\. ']?פ[\. '] ?) | (ע[\. ']?מ[\. '] ?) | (ע[\. ']?פ[\. '] ?))[\s\.:]*\d{4}";
            Regex billNumOrCompanyNumRgx = new Regex("(" + billNum + ") | (" + privateCompanyOrNot + ")", RegexOptions.IgnorePatternWhitespace);
            Match match = billNumOrCompanyNumRgx.Match(billText);
            if (match != null && match.Success)
            {
                //the store name and details is before the bill number
                int lineBegin = billText.LastIndexOf('\n', match.Index);
                
                int lineEnd = billText.IndexOf('\n', match.Index);
                //if there is match and the match is less then 4 lines
                if (lineBegin > 0 && (new Regex("\n").Matches(billText.Substring(0, lineBegin)))?.Count <= 4)
                {
                    this.StoreName = billText.Substring(0, lineBegin);
                    billText = billText.Substring(lineEnd);
                }
                else
                {
                    //the first line is the store name
                    int firstLineEndInd = billText.IndexOf("\n");
                    if (firstLineEndInd != -1)
                    {
                        StoreName = billText.Substring(0, firstLineEndInd);
                        billText = billText.Substring(firstLineEndInd + 1);
                    }
                }

            }
            else
            {
                //the first line is the store name
                int firstLineEndInd = billText.IndexOf("\n");
                if (firstLineEndInd != -1)
                {
                    StoreName = billText.Substring(0, firstLineEndInd);
                    billText = billText.Substring(firstLineEndInd + 1);
                }
            }
            billText = billText.RemoveLinesContains(privateCompanyOrNot);
            billText = billText.RemoveLinesContains(billNum);
            return billText;
        }

        private string SetTotalSum(string billText)
        {
            double sum = 0;
            string pricePattern = @"(\d{1,3},)?(\d{1,3})\.\d{2}";
            Regex price = new Regex(pricePattern, RegexOptions.IgnorePatternWhitespace);
            var allPricesRes = price.Matches(billText);
            if (allPricesRes == null)
            {
                return billText;
            }
            Regex sumForPayRgx = new Regex("תשלום *:? *\r?\n?" + price);
            var sumResult = sumForPayRgx.Match(billText);
            if (sumResult != null && double.TryParse(price.Match(sumResult.Value).Value, out sum))
            {
                billText = billText.RemoveLinesContains(sumResult.Value);
            }
            else
            {
                double temp;
                //if there is no result or the corvertion failed
                List<double> allPrices = new List<double>();
                foreach (Match p in allPricesRes)
                {
                    //find the max sum and save it as total sum
                    if (double.TryParse(p.Value, out temp))
                    {
                        if (temp > sum)
                        {
                            sum = temp;
                        }
                    }
                }
            }
            TotalSum = sum;
            return billText;
        }

        private string SetProducts(string billText)
        {
            //paterns of items: code, item name, price , count , sum(=count*price)


            Regex priceRgx = new Regex(Product.PRICE_PATTERN, RegexOptions.IgnorePatternWhitespace);
            Regex codeRgx = new Regex(Product.CODE_PATTERN, RegexOptions.IgnorePatternWhitespace);
            Regex countRgx = new Regex(Product.COUNT_PATTERN);
            Regex itemNameRgx = new Regex(Product.ITEM_NAME_PATTERN);
            //remove lines with  discount:
            //TODO: check it
            Regex discountRgx = new Regex(@"(הנחה)?.*-" + Product.PRICE_PATTERN, RegexOptions.IgnorePatternWhitespace);
            foreach (Match d in discountRgx.Matches(billText))
            {
                billText = billText.RemoveLinesContains(d.Value);
            }
            billText = billText.RemoveLinesContains(@"((מע?.מ) | ( סה.?כ ) | ( תשלום ) | ( סכום)){1}[:\s\n\t\r₪]*" + Product.PRICE_PATTERN);//במעמ \r\n2,564.11 \r
            //TODO: find why line 2(commented) not deleted !!!
            string[] sentenses = billText.Split('\n');
            //TODO: remove empty lines
            var codeMatches = codeRgx.Matches(billText);
            var priceMatches = priceRgx.Matches(billText);
            var itemsNamesMatches = itemNameRgx.Matches(billText);
            bool[] hasCode = new bool[sentenses.Length];
            bool[] hasPrice = new bool[sentenses.Length];
            bool[] hasCount = new bool[sentenses.Length];
            bool[] hasItemName = new bool[sentenses.Length];
            Product p;
            this.Produts = new List<Product>();
            for (int i = 0; i < sentenses.Length; i++)
            {
                hasCode[i] = codeRgx.IsMatch(sentenses[i]);
                hasPrice[i] = priceRgx.IsMatch(sentenses[i]);
                hasCount[i] = countRgx.IsMatch(sentenses[i]);
                hasItemName[i] = itemNameRgx.IsMatch(sentenses[i]);

            }
            bool[] used = new bool[sentenses.Length]; //if this sentense has been used already
            for (int i = 0; i < sentenses.Length; i++)
            {
                if (hasPrice[i] && hasCode[i] && hasItemName[i] && hasCount[i])
                {
                    //all details in 1 line
                    p = new Product(sentenses[i]);
                    used[i] = true;
                    Produts.Add(p);
                }
                else
                {
                    if (i > 0 && !used[i - 1])
                    {//if the product details in 2 lines
                        if ((hasPrice[i] | hasPrice[i - 1]) && (hasCode[i] | hasCode[i - 1])
                            && (hasItemName[i] | hasItemName[i - 1]) && (hasCount[i] | hasCount[i]))
                        {
                            //all details in 2 lines
                            p = new Product(sentenses[i - 1] + "\n" + sentenses[i]);
                            used[i] = used[i - 1] = true;
                            Produts.Add(p);

                        }
                        else
                        {
                            if (hasPrice[i - 1] && (hasCode[i - 1] | hasItemName[i - 1]) && hasCount[i - 1])
                            {
                                //3 from 4 details in 1 lines
                                p = new Product(sentenses[i - 1]);
                                used[i - 1] = true;
                                Produts.Add(p);
                            }
                            else
                            {
                                if ((hasPrice[i - 1] | hasPrice[i]) &&
                                    ((hasCode[i - 1] | hasCode[i]) | (hasItemName[i - 1] | hasItemName[i]))
                                    && (hasCount[i - 1] | hasCount[i]))
                                {
                                    //3 from 4 details in 2 lines
                                    p = new Product(sentenses[i - 1] + "\n" + sentenses[i]);
                                    used[i - 1] = used[i] = true;
                                    Produts.Add(p);
                                }
                            }
                        }
                    }
                }

            }

            //if no products have been found:
            //trying find products in low confidense levels
            if(Produts.Count == 0)
            {
                for(int i = 0; i < 0; i++)
                {
                    //2 from 4 details in 1 line
                    if(hasPrice[i]&& (hasItemName[i] | hasCode[i])){
                        p = new Product(sentenses[i]);
                        used[i] = true;
                        Produts.Add(p);
                    }
                    else
                    {
                        //2 from 4 details in 2 lines
                        if(i>0 && ! used[i-1] &&(hasPrice[i]|hasPrice[i-1]) && ((hasItemName[i]|hasItemName[i-1]) | (hasCode[i]|hasCode[i-1])))
                        {
                            p = new Product(sentenses[i - 1] + "\n" + sentenses[i]);
                            used[i - 1] = used[i] = true;
                            Produts.Add(p);
                        }
                    }
                }
            }


            return billText;

        }

        private string SetCategories(string billText)
        {
            //TODO:
            //search if the bill contains some of the categories
            //list<category> categories = 
            return billText;
        }
    }
}
