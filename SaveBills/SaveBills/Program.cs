using Dal.Classes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveBills
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    string path1 = @"E:\תכנות\final project\bills\bill examples\PDF\";
                    string path2 = @"E:\תכנות\final project\bills\bill examples\IMG\";
                    //string txt = BillOCR.GetBillTextFromPDF("3.pdf", path1);
                    //Bill b = new Bill(path2+"1.jpg");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
