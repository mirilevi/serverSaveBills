using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable

namespace Dal.Classes
{
    public partial class Produt
    {
       
    public const string PRICE_PATTERN = @"(\d{1,3},)?(\d{1,3})\.\d{1,2}";
    public const string CODE_PATTERN = @"([A-Za-z\d]+-?){4}";
    public const string COUNT_PATTERN = @"[xX]?(כ.)? *\t?[1-9]{1} ";
    public const string ITEM_NAME_PATTERN = @"[א-תA-Za-z\d\\/+-. ]*[א-תA-Za-z]{3}[א-תA-Za-z\d\\/+-. ]*";
    public Produt()
        {
                
        }
        public Produt(string productTxt)
        {
            Regex priceRgx = new Regex(Produt.PRICE_PATTERN, RegexOptions.IgnorePatternWhitespace);
            Regex codeRgx = new Regex(Produt.CODE_PATTERN, RegexOptions.IgnorePatternWhitespace);
            Regex countRgx = new Regex(Produt.COUNT_PATTERN);
            Regex itemNameRgx = new Regex(Produt.ITEM_NAME_PATTERN);

            
            var prices = priceRgx.Matches(productTxt);
            
            if (prices?.Count > 0)
            {
                double price;
                //if there is more than 1 price - it can be the sum of price * count
                //find the max price :
                double max = 0;
                foreach (Match p in prices)
                {
                    if( double.TryParse(p.Value, out price))
                    {
                        max = Math.Max(max, price);
                        productTxt = productTxt.RemovePattern(p.Value);
                    }
                }
                Price = max;
            }
            Name = itemNameRgx.Match(productTxt)?.Value;
            productTxt = productTxt.RemovePattern(ITEM_NAME_PATTERN);
            Barcode = codeRgx.Match(productTxt)?.Value;
            productTxt = productTxt.RemovePattern(Barcode);
            if (countRgx.IsMatch(productTxt))
            {
                int co;
                //convert product count
                int.TryParse(new Regex(@"\d+").Match(countRgx.Match(productTxt).Value).Value, out co);
                Count = co;
            }

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public double? Price { get; set; }
        public int BillId { get; set; }
        public int? Count { get; set; }


        public virtual Bill Bill { get; set; }
    }
}
