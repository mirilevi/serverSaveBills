using Dal.Interfaces;
using IronOcr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Classes
{
    public  class BillOCR
    {
        public static string GetBillTextFromPDF(string filePath)
        {
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.English;
            Ocr.AddSecondaryLanguage(OcrLanguage.HebrewAlphabetBest);
            try
            {
                using (var Input = new OcrInput(filePath)) 
                {
                    var Result = Ocr.Read(Input);
                    return Result.Text;
                }
            }catch (Exception e)
            {
                throw new Exception();
            }
            
        }
    }
    
}
