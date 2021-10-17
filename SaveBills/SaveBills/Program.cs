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

                    string path1 = @"E:\�����\final project\SERVER\bill examples\IMG\";
                    string path2 = @"E:\�����\final project\SERVER\bill examples\PDF\";
                    //string txt = BillOCR.GetBillTextFromPDF("3.pdf", path1);
                    //Bill b = new Bill(path2 + "2.pdf");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
