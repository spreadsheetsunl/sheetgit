using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Gma.System.MouseKeyHook;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace GitExcelAddIn
{
    public partial class ThisAddIn
    {

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Plugin initiated");
            this.Application.WorkbookOpen += new Excel.AppEvents_WorkbookOpenEventHandler(Application_WorkbookStart);
            ((Excel.AppEvents_Event)this.Application).NewWorkbook += new Excel.AppEvents_NewWorkbookEventHandler(Application_WorkbookStart);
            
            var myUserControl1 = new TaskPane();
            var myCustomTaskPane = this.CustomTaskPanes.Add(myUserControl1, "SheetGit");
            myCustomTaskPane.Visible = true;



        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Plugin Shutdown");
        }

        void Application_WorkbookStart(Excel.Workbook wb)
        {
            System.Diagnostics.Debug.WriteLine("Workbook Opened or Created");
            this.Application.SheetChange += new Excel.AppEvents_SheetChangeEventHandler(Application_SheetChange);
            this.Application.SheetCalculate += new Excel.AppEvents_SheetCalculateEventHandler(Application_SheetCalculate);
        }

        void Application_SheetChange(object sh, Excel.Range target)
        {
            System.Diagnostics.Debug.WriteLine("Worksheet Changed");
            Excel.Worksheet sheet = (Excel.Worksheet)sh;
            /*Globals.ThisAddIn.Application.ActiveWorkbook.SaveAs();*/


            /* string changedRange = target.get_Address(
                Excel.XlReferenceStyle.xlA1);

            MessageBox.Show("The value of " + sheet.Name + ":" +
                changedRange + " was changed.");*/
        }

        void Application_SheetCalculate(object sh)
        {
            System.Diagnostics.Debug.WriteLine("Worksheet Recalculated");
        }

        private int getExcelColumnNumber(string colAdress)
        {
            int[] digits = new int[colAdress.Length];
            for (int i = 0; i < colAdress.Length; ++i)
            {
                digits[i] = Convert.ToInt32(colAdress[i]) - 64;
            }
            int mul = 1; int res = 0;
            for (int pos = digits.Length - 1; pos >= 0; --pos)
            {
                res += digits[pos] * mul;
                mul *= 26;
            }
            return res;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
