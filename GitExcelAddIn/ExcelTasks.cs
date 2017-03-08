using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace GitExcelAddIn
{
    class ExcelTasks
    {

        //change cell "A1->3", insert column "E3", change formula "E3, =1+1"
        //change cell "A1->6"


        public static void doTask(string task, string cell, string value)
        {
            switch (task)
            {
                case "Change Cell":
                    changeCell(cell, value);
                    break;
                case "Change Formula":
                    break;
                case "Insert Row":
                    break;
                case "Insert Column":
                    break;
                case "Remove Row":
                    break;
                case "Remove Column":
                    break;

            }
        }

        private static void changeCell(string cell, string value)
        {
            
        }

    }
}
