using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Office.Interop.Excel;
using Point = Microsoft.Office.Interop.Excel.Point;

namespace GitExcelAddIn
{
    class Utils
    {
        protected internal void insertColumn()
        {
            Worksheet s = Globals.ThisAddIn.Application.Worksheets[0];

            Range rng = s.Range["A2"];
            rng.EntireColumn.Insert(XlInsertShiftDirection.xlShiftToRight, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
        }

        protected static internal void editCell(string cell, string value)
        {
            Worksheet s = Globals.ThisAddIn.Application.ActiveSheet;
            Range rng = s.Range[cell];
            rng.Value2 = value;
        }

        protected internal void initGraph()
        {
            Branch master = new Branch(null, "master");
        }

        protected internal static string GenerateFilePath(string fileName)
        {
            return Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sheetGit"), $"{fileName.Split('.')[0]}");
        }


    }
}
