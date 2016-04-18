using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace GitExcelAddIn
{
    [ComVisible(true)]
    public class JavaScript
    {

        public void ExecuteMacro(string myResponse)
        {
            Thread t = new System.Threading.Thread(delegate ()
            {
                string[] trueComm = myResponse.Split(new string[] {"***"}, StringSplitOptions.None);

                foreach (string command in trueComm)
                {
                    if (command == "") break;
                    string[] strArr = command.Split(new string[] { "|" }, StringSplitOptions.None);
                    //"R|Edit Cell|F31|404,96|400"

                    switch (strArr[1])
                    {
                        case "Insert Column":
                            if (strArr[0] == "A") insertColumn();
                            else deleteColumn();
                            break;
                        case "Insert Row":
                            if (strArr[0] == "A") insertRow();
                            else deleteRow();
                            break;
                        case "Delete Row":
                            if (strArr[0] == "R") insertRow();
                            else deleteRow();
                            break;
                        case "Delete Column":
                            if (strArr[0] == "R") insertColumn();
                            else deleteColumn();
                            break;
                        case "Edit Cell":
                            if (strArr[0] == "R") editCell(strArr[2], strArr[3]);
                            else editCell(strArr[2], strArr[4]);
                            break;
                        case "Format Cell":
                            if (strArr[0] == "R") formatCell(strArr[2], strArr[3]);
                            else formatCell(strArr[2], strArr[4]);
                            break;
                    }
                }

            });
            t.Start();
        }

   

        private void insertColumn()
        {
            VirtualMouse.MoveMouse(309, 388, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(92, 47, 15);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(1479, 116, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(1492, 193, 10);
            VirtualMouse.ClickLeftMouseButton();
        }
        private void insertRow()
        {
            VirtualMouse.MoveMouse(255, 833, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(92, 47, 15);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(1479, 116, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(1492, 169, 10);
            VirtualMouse.ClickLeftMouseButton();
        }
        private void deleteColumn()
        {
            VirtualMouse.MoveMouse(309, 388, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(92, 47, 15);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(1519, 156, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(1519, 194, 10);
            VirtualMouse.ClickLeftMouseButton();
        }
        private void deleteRow()
        {
            VirtualMouse.MoveMouse(255, 833, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(92, 47, 15);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(1519, 116, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(1519, 170, 10);
            VirtualMouse.ClickLeftMouseButton();
        }
        private void editCell(string cell, string value)
        {
            switch (cell)
            {
                case "D31":
                    VirtualMouse.MoveMouse(344, 899, 20);
                    break;
                case "F31":
                    VirtualMouse.MoveMouse(537, 894, 20);
                    break;
                case "C28":
                    VirtualMouse.MoveMouse(251, 831, 20);
                    break;
                case "D28":
                    VirtualMouse.MoveMouse(352, 831, 20);
                    break;
                case "E28":
                    VirtualMouse.MoveMouse(458, 831, 20);
                    break;
                case "F28":
                    VirtualMouse.MoveMouse(543, 831, 20);
                    break;
                case "G28":
                    VirtualMouse.MoveMouse(621, 831, 20);
                    break;
            }
            VirtualMouse.ClickLeftMouseButton();
            Utils.editCell(cell, value);
        }
        private void formatCell(string cell, string format)
        {

            VirtualMouse.MoveMouse(344, 899, 20);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(92, 47, 15);
            VirtualMouse.ClickLeftMouseButton();
            VirtualMouse.MoveMouse(319, 112, 10);
            VirtualMouse.ClickLeftMouseButton();
            switch (format)
            {
                case "Yellow":
                    VirtualMouse.MoveMouse(420, 173, 10);
                    break;
                case "":
                    VirtualMouse.MoveMouse(331,289,10);
                    break;
            }
            VirtualMouse.ClickLeftMouseButton();


        }

        public string GetGitLog()
        {
            return "holymoly";
        }
    }

}
