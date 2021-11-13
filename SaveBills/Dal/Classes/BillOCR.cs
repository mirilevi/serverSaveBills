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
            bool netfreeException = true;
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.English;
            Ocr.AddSecondaryLanguage(OcrLanguage.HebrewAlphabetBest);

            while (netfreeException)
            {
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

                                //Input.DeNoise(); // fixes digital noise and poor scanning
                                Input.Deskew();
                                var Result = Ocr.Read(Input);
                                File.Delete(fileName);
                                return Result.Text;
                            }
                        }
               
                    }catch (Exception e1)
                    {
                        //if(e1.Message)
                        throw e1;
                    }
                }
                catch (Exception e2)
                {
                    if (e2.Message == "The remote server returned an error: (418) Blocked by NetFree.")
                    {
                        //if its netfree error - try again and again
                        netfreeException = true;
                        //System.Threading.Thread.Sleep(60 * 60 * 60);
                    }
                    else
                    {
                    throw e2;

                    }
                    
                }
            }
            
            return "";
        }
    }
    
}
