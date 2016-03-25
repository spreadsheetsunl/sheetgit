using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitExcelAddIn
{
    class Commit
    {
        private string message;
        private string action;
        private string cell;
        private int x;
        private int y;
        private Commit parentCommit;
        public Commit(string message, string action, string cell, int x, int y, Commit parentCommit)
        {
            this.message = message;
            this.action = action;
            this.x = x;
            this.cell = cell;
            this.y = y;
            this.parentCommit = parentCommit;
        }
    }
}
