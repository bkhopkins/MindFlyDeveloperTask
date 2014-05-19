using System;
using System.Linq;
using Umbraco.Core;
using Microsoft.Office.Interop.Excel;

namespace MindFlyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Product Import");
            Console.WriteLine("-----------------------");
           
            // initialize the app
            var application = new ConsoleApplicationBase();
            application.Start(application, new EventArgs());

            // get a reference to the service context
            var context = ApplicationContext.Current;
            var serviceContext = context.Services;
            var contentService = serviceContext.ContentService;

            
            // get a reference to the root content 
            var rootContent = contentService.GetRootContent();
            
            // reference the product list node            
            var productListNode = rootContent.FirstOrDefault();

            // remove all of the descendants in case this example app is ran more than once
            var descendants = contentService.GetDescendants(productListNode);

            if (descendants.Count() > 0)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("* Cleaning Up Descendants *");

                foreach (var content in descendants)
                {
                    
                    contentService.MoveToRecycleBin(content);
                    Console.WriteLine("Removed - " + content.Name);
                }

                Console.WriteLine("* Emptying Recycle Bin *");
                contentService.EmptyRecycleBin();
                Console.WriteLine("* Recycle Bin Empty *");
            }


            Console.WriteLine("-----------------------");
            Console.WriteLine("* Opening Import File *");

            // open the excel document for import
            Application excelApp = new Application();
            Workbook excelWorkBook = excelApp.Workbooks.Open(@"C:\Users\bryan\Desktop\MindFly\MindFlyDemo\product_export.xls");
            Worksheet excelWorkSheet = excelWorkBook.Sheets[1];
            Range rowRange = excelWorkSheet.UsedRange;
            int rowCount = rowRange.Rows.Count;
            int colCount = rowRange.Columns.Count;

            Console.WriteLine("* Importing Data *");
            // iterate through the rows, skipping the header row
            for (int row = 2; row <= rowCount; row++)
            {
                // create the content using the product name as the content name
                var content = contentService.CreateContent(rowRange.Cells[row, 2].Value2.ToString(), rootContent.FirstOrDefault().Id, "Product");

                // iterate properties of the content and populate the value
                foreach (var property in content.Properties)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        if (rowRange.Cells[1, col].Value2.ToString().ToLower() == property.Alias)
                        {
                            property.Value = rowRange.Cells[row, col].Value2;
                            break;
                        }
                    }
                }
                
                contentService.Save(content);
                Console.WriteLine( "Created - " + content.Name);
            }

            Console.WriteLine("* Import Complete *");
            Console.ReadLine();

        }


    }



}
