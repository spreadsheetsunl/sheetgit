using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using LibGit2Sharp;
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

        protected internal static string GenerateFilePath(string fileName = null)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sheetGit");
            if (!string.IsNullOrEmpty(fileName)) return Path.Combine(path, $"{fileName.Split('.')[0]}");
            return path;
        }

        public static Commit GetMergeBase(Commit a, Commit b)
        {
            var baseCommit = ThisAddIn.Repo.ObjectDatabase.FindMergeBase(a, b);
            return baseCommit;


        }

        protected internal static bool WaitForFile(string fullPath)
        {
            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(fullPath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(
                       "WaitForFile {0} failed to get an exclusive lock: {1}",
                        fullPath, ex.ToString());

                    if (numTries > 10)
                    {
                        Debug.WriteLine(
                            "WaitForFile {0} giving up after 10 tries",
                            fullPath);
                        return false;
                    }

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }

            Debug.WriteLine("WaitForFile {0} returning true after {1} tries",
                fullPath, numTries);
            return true;
        }


    }
}
