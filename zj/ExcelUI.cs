using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Reflection;

namespace zj
{
    class ExcelUI
    {
        public static void ExportExcel(System.Data.DataTable dt, string strExcelFileName)
        {
            try
            {
                object objOpt = Missing.Value;

                //System.Windows.Forms.Application excel = new System.Windows.Forms.Application();
                Microsoft.Office.Interop.Excel.ApplicationClass excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                excel.Visible = true;
                _Workbook wkb = excel.Workbooks.Add(objOpt);
                _Worksheet wks = (_Worksheet)wkb.ActiveSheet;

                wks.Visible = XlSheetVisibility.xlSheetVisible;

                int rowIndex = 1;
                int colIndex = 0;

                System.Data.DataTable table = dt;
                foreach (DataColumn col in table.Columns)
                {
                    colIndex++;
                    excel.Cells[1, colIndex] = col.ColumnName;
                }

                foreach (DataRow row in table.Rows)
                {
                    rowIndex++;
                    colIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;
                        excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                    }
                }
                excel.Columns.AutoFit();//设置自动列宽
                                        //excel.Sheets[0] = "sss";
                wkb.SaveAs(strExcelFileName, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
                wkb.Close(false, objOpt, objOpt);
                excel.Quit();
            }
            catch 
            {

            }
           
        }

        public static void ExportExcelFormat(System.Data.DataTable dt, string strExcelFileName)
        {
            object objOpt = Missing.Value;

            //System.Windows.Forms.Application excel = new System.Windows.Forms.Application();
            Microsoft.Office.Interop.Excel.ApplicationClass excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excel.Visible = true;
            _Workbook wkb = excel.Workbooks.Add(objOpt);
            _Worksheet wks = (_Worksheet)wkb.ActiveSheet;

            wks.Visible = XlSheetVisibility.xlSheetVisible;

            int rowIndex = 1;
            int colIndex = 0;

            System.Data.DataTable table = dt;
            foreach (DataColumn col in table.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
            }

            excel.Cells.NumberFormat = "@";//设置文本格式
            foreach (DataRow row in table.Rows)
            {
                rowIndex++;
                colIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    colIndex++;
                    excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                }
            }
            excel.Columns.AutoFit();//设置自动列宽
            //excel.Sheets[0] = "sss";
            //wkb.SaveAs(strExcelFileName, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            //wkb.Close(false, objOpt, objOpt);
            //excel.Quit();
        }

        public static void OpenExcel_A4_3(System.Data.DataTable dt, string strExcelFileName,String strExcelSave,int rowIndex,int colIndex,int ls)
        {
            try
            {
                object objOpt = Missing.Value;
                Microsoft.Office.Interop.Excel._Application xApp = new ApplicationClass();
             //   xApp.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(strExcelFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];

                System.Data.DataTable table = dt;

                foreach (DataRow row in table.Rows)
                {
                    rowIndex++;
                    colIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;

                        xApp.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                    }
                }
                int Rowcount = table.Rows.Count + 1;
                Range range = xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[Rowcount, ls]);//15指的是列数
                range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                range.Font.Size = 11;
                range.Cells.RowHeight = 24.9;

                ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:1", System.Type.Missing]).RowHeight = 39.9;
                ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:1", System.Type.Missing]).Font.Size = 22;

                ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:2", System.Type.Missing]).Borders.LineStyle = XlLineStyle.xlLineStyleNone;
                // ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:2", System.Type.Missing]).Font.Size = 22;

                xApp.DisplayAlerts = false;
                xBook.SaveAs(strExcelSave, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
                xBook.Close(false, objOpt, objOpt);
                xApp.Quit();
            }
            catch
            {
            }
           
        }

        public static void OpenExcel_WPFBQKTJ(System.Data.DataTable dt, string strExcelFileName, String strExcelSave, int rowIndex, int colIndex, int ls)
        {
            try
            {
                object objOpt = Missing.Value;
                Microsoft.Office.Interop.Excel._Application xApp = new ApplicationClass();
                //   xApp.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(strExcelFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];

                System.Data.DataTable table = dt;

                foreach (DataRow row in table.Rows)
                {
                    rowIndex++;
                    colIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;

                        xApp.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                    }
                }
                int Rowcount = table.Rows.Count + 1;
                Range range = xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[Rowcount, ls]);//15指的是列数
                range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                range.Font.Size = 11;
                range.Cells.RowHeight = 24.6;

                ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:1", System.Type.Missing]).RowHeight = 43.2;
                ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:1", System.Type.Missing]).Font.Size = 22;

              //  ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:2", System.Type.Missing]).Borders.LineStyle = XlLineStyle.xlLineStyleNone;
                // ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:2", System.Type.Missing]).Font.Size = 22;

                xApp.DisplayAlerts = false;
                xBook.SaveAs(strExcelSave, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
                xBook.Close(false, objOpt, objOpt);
                xApp.Quit();
            }
            catch
            {
            }

        }


        public static void OpenExcel_A4_cover(string strExcelFileName, String strExcelSave)
        {
            object objOpt = Missing.Value;
            Microsoft.Office.Interop.Excel._Application xApp = new ApplicationClass();
            xApp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(strExcelFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];
            xApp.DisplayAlerts = false;
            xBook.SaveAs(strExcelSave, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            //xBook.Close(false, objOpt, objOpt);
            //xApp.Quit();
        }

        public static void OpenExcel_DRMB(System.Data.DataTable dt, string strExcelFileName, String strExcelSave, int rowIndex, int colIndex)
        {
            try
            {
                object objOpt = Missing.Value;
                Microsoft.Office.Interop.Excel._Application xApp = new ApplicationClass();
               // xApp.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(strExcelFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];

                //System.Data.DataTable table = dt;

                //foreach (DataRow row in table.Rows)
                //{
                //    rowIndex++;
                //    colIndex = 0;
                //    foreach (DataColumn col in table.Columns)
                //    {
                //        colIndex++;
                //        xApp.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                //    }
                //}
                xApp.DisplayAlerts = true;
                xBook.SaveAs(strExcelSave, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
                xBook.Close(false, objOpt, objOpt);
                xApp.Quit();
            }
            catch (Exception)
            {

                return;
            }
            
           
        }



        public static void OpenExcel_WPXHJHB(System.Data.DataTable dt, string strExcelFileName, String strExcelSave, int rowIndex, int colIndex, int ls,string dwmc)
        {
            try
            {
                object objOpt = Missing.Value;
                Microsoft.Office.Interop.Excel._Application xApp = new ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(strExcelFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];
                xSheet.Cells[2, 1] = "编制单位：" + dwmc;
                xSheet.Cells[2, 25] = DateTime.Now.ToLongDateString().ToString();
                System.Data.DataTable table = dt;

                foreach (DataRow row in table.Rows)
                {
                    rowIndex++;
                    colIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;

                        xApp.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                    }
                }
                int Rowcount = table.Rows.Count + 1;
                //Range range = xSheet.get_Range(xSheet.Cells[2, 1], xSheet.Cells[Rowcount, ls]);//15指的是列数
             

                //range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //range.Font.Size = 11;
                //range.Cells.RowHeight = 24.6;

                //((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:1", System.Type.Missing]).RowHeight = 43.2;
                //((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:1", System.Type.Missing]).Font.Size = 22;

                //  ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:2", System.Type.Missing]).Borders.LineStyle = XlLineStyle.xlLineStyleNone;
                // ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:2", System.Type.Missing]).Font.Size = 22;

                xApp.DisplayAlerts = false;
                xBook.SaveAs(strExcelSave, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
                xBook.Close(false, objOpt, objOpt);
                xApp.Quit();
            }
            catch
            {
            }

        }

        public static void OpenExcel_WPYJJHB(System.Data.DataTable dt, string strExcelFileName, String strExcelSave, int rowIndex, int colIndex, int ls, string dwmc)
        {
            try
            {
                object objOpt = Missing.Value;
                Microsoft.Office.Interop.Excel._Application xApp = new ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(strExcelFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];
                xSheet.Cells[2, 1] = "编制单位：" + dwmc;
                xSheet.Cells[2, 26] = DateTime.Now.ToLongDateString().ToString();

                
                System.Data.DataTable table = dt;

                foreach (DataRow row in table.Rows)
                {
                    rowIndex++;
                    colIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;

                        xApp.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                    }
                }
                
                int Rowcount = table.Rows.Count + 1;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if(i<= table.Rows.Count - 2)
                    {
                        xApp.DisplayAlerts = false;
                        if (table.Rows[i][0].ToString() == table.Rows[i + 1][0].ToString())
                        {
                            int start = 5 + i;
                            int end = 5+ i + 1;
                            xSheet.Range["A" + start, "A" + end].Merge(0);
                            
                        }
                        if (table.Rows[i][1].ToString() == table.Rows[i + 1][1].ToString())
                        {
                            int start =5 + i;
                            int end = 5 + i + 1;
                            xSheet.Range["B" + start, "B" + end].Merge(0);
                        }
                        if (table.Rows[i][2].ToString() == table.Rows[i + 1][2].ToString())
                        {
                            int start = 5 + i;
                            int end = 5 + i + 1;
                            xSheet.Range["C" + start, "C" + end].Merge(0);
                        }
                        if (table.Rows[i][3].ToString() == table.Rows[i][4].ToString()&& table.Rows[i][4].ToString()== table.Rows[i][5].ToString() && table.Rows[i][5].ToString() == table.Rows[i][6].ToString() && table.Rows[i][6].ToString() == table.Rows[i][7].ToString() && table.Rows[i][7].ToString() == table.Rows[i][8].ToString())
                        {
                            int start = 5 + i;
                            xSheet.Range["D" + start, "I" + start].Merge(0);
                        }
                        if (table.Rows[i][0].ToString() == table.Rows[i][1].ToString() && table.Rows[i][1].ToString() == table.Rows[i][2].ToString() && table.Rows[i][2].ToString() == table.Rows[i][3].ToString() && table.Rows[i][3].ToString() == table.Rows[i][4].ToString() && table.Rows[i][4].ToString() == table.Rows[i][5].ToString() && table.Rows[i][5].ToString() == table.Rows[i][6].ToString() && table.Rows[i][6].ToString() == table.Rows[i][7].ToString() && table.Rows[i][7].ToString() == table.Rows[i][8].ToString())
                        {
                            int start = 5 + i;
                            xSheet.Range["A" + start, "I" + start].Merge(0);
                        }
                    }
                }

                //Range range = xSheet.get_Range(xSheet.Cells[2, 1], xSheet.Cells[Rowcount, ls]);//15指的是列数


                //range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //range.Font.Size = 11;
                //range.Cells.RowHeight = 24.6;

                //((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:1", System.Type.Missing]).RowHeight = 43.2;
                //((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:1", System.Type.Missing]).Font.Size = 22;

                //  ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:2", System.Type.Missing]).Borders.LineStyle = XlLineStyle.xlLineStyleNone;
                // ((Microsoft.Office.Interop.Excel.Range)xSheet.Rows["1:2", System.Type.Missing]).Font.Size = 22;

                xApp.DisplayAlerts = false;
                xBook.SaveAs(strExcelSave, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
               
                xBook.Close(false, objOpt, objOpt);
                xApp.Quit();
            }
            catch
            {
            }

        }

        public static void OpenExcel_WPHZTJB(System.Data.DataTable dt, string strExcelFileName, String strExcelSave, int rowIndex, int colIndex, int ls)
        {
            try
            {
                object objOpt = Missing.Value;
                Microsoft.Office.Interop.Excel._Application xApp = new ApplicationClass();
                //   xApp.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(strExcelFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];

                System.Data.DataTable table = dt;

                foreach (DataRow row in table.Rows)
                {
                    rowIndex++;
                    colIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;

                        xApp.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                    }
                }
                int Rowcount = table.Rows.Count + 1;

                xApp.DisplayAlerts = false;
                xBook.SaveAs(strExcelSave, objOpt, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
                xBook.Close(false, objOpt, objOpt);
                xApp.Quit();
            }
            catch
            {
            }

        }
    }
}
