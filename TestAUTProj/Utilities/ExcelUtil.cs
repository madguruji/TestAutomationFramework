using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAUTProj.Utilities
{
    public class ExcelUtil
    {
        private static IWorkbook ExcelWBook;
        private static ISheet ExcelWSheet;
        /// <summary>
        /// Takes path of the file
        /// Takes the sheet name 
        /// </summary>
        /// <param name="path">Path of the excel file</param>
        /// <param name="sheetName">Name of the sheet</param>
        public static void setExcelFile(string path, string sheetName)
        {
            try
            {
                FileStream ExcelFile = new FileStream(path, FileMode.Open, FileAccess.Read);
                ExcelWBook = new XSSFWorkbook(ExcelFile);
                 ExcelWSheet = ExcelWBook.GetSheet(sheetName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }
        public static string[,] getTestData(string tableName)
        {
            string[,] testData = null;
            try
            {
                //Handles numbers and strings
                DataFormatter dataFormatter = new DataFormatter();
                //Boundary cells are the first and the last column
                //We need to find first and last column, so that we know thich rows to read for the data
                XSSFCell[] boundaryCells = findTableNameCells(tableName);
                //First cell to start with
                XSSFCell startCell = boundaryCells[0];
                // Last cell where data rading should stop 
                XSSFCell endCell = boundaryCells[1];

                //Find the start row based on the start cell
                int startRow = startCell.RowIndex + 1;

                //Find the end row based on end cell
                int endRow=endCell.RowIndex - 1;
                //Find the start column based on the start cell
                int startCol=startCell.ColumnIndex + 1;
                //Find the end column based on the end cell
                int endCol = endCell.ColumnIndex - 1;
                //Declare multi dimensional array to capture the data from the table
                testData = new string[endRow - startRow + 1,endCol - startCol + 1];
                for (int i = startRow; i < endRow+1; i++)
                {
                    //For every cloumn in every row, fetch the value of the cell
                    for (int j = startCol; j < endCol+1; j++)
                    {
                        ICell cell = ExcelWSheet.GetRow(i).GetCell(j);
                        //Capture the value of the cell in the multi-dimensional array
                        testData[i-startRow,j-startCol]= dataFormatter.FormatCellValue(cell);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
            return testData;
        }
        public static XSSFCell[] findTableNameCells(string tableName)
        {
            DataFormatter dataFormatter = new DataFormatter();
            //declare begin position
            string pos = "begin";
            XSSFCell[] cells = new XSSFCell[2];
            foreach (IRow row in ExcelWSheet)
            {
                foreach (ICell cell in row)
                {
                    if (tableName.Equals(dataFormatter.FormatCellValue(cell)))
                    {
                        if (pos.Equals("begin",StringComparison.OrdinalIgnoreCase))
                        {
                            //Finds the begin cells, this is used for boundary cells
                            cells[0] = (XSSFCell)cell;
                            pos = "end";
                        }
                        else
                        {
                            //Finds the boundary cells, this is used for boundary cells
                            cells[1]= (XSSFCell)cell;
                        }

                    }
                }
            }
            return cells;
        }
    }
}
