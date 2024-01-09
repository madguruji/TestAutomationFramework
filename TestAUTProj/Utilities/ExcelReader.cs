using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAUTProj.Utilities
{
    public class ExcelReader
    {
        public static void procExcel()
        {
            IWorkbook ExcelWBook;
            ISheet ExcelWSheet;
            ICell ExcelCell;
            string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString()+ @"\TestData\TestData.xlsx";
            string sheetName = "FirstTD";
            try
            {
                FileStream ExcelFile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                if(File.Exists(filePath))
                {
                    Console.WriteLine("File exist on location :: -----> "+filePath);
                }
                else
                {
                    Console.WriteLine("File not found inside the Test Data folder :: -----> " + filePath);
                }
                ExcelWBook = new XSSFWorkbook(ExcelFile);
                ExcelWSheet = ExcelWBook.GetSheet(sheetName);
                ExcelCell = ExcelWSheet.GetRow(0).GetCell(0);
                string cellData = ExcelCell.StringCellValue;
                Console.WriteLine("Cell data value is : "+cellData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }
    }
}
