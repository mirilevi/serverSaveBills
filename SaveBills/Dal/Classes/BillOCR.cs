using Dal.Interfaces;
using IronOcr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Dal.Classes
{
    public  class BillOCR
    {
        public static string GetBillTextFromPDF(string filePath,string fileName)
        {
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.English;
            Ocr.AddSecondaryLanguage(OcrLanguage.HebrewAlphabetBest);
            
            try
            {
                //download the file from remote server
                WebClient Client = new WebClient();
                Client.DownloadFile(filePath,fileName);
                try
                {
                    if (File.Exists(fileName))
                    {
                        //read ocr file
                        using (var Input = new OcrInput(fileName)) 
                        {
                            var Result = Ocr.Read(Input);
                            File.Delete(fileName);
                            return Result.Text;
                        }
                    }
               
                }catch (Exception e1)
                {
                    throw e1;
                }
                    //Input.DeNoise(); // fixes digital noise and poor scanning
                    //Input.Deskew();
            }
            catch (Exception e2)
            {

                throw e2;
            }
            return "";
        }
    }
    
}
