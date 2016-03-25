using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitExcelAddIn
{
    class Branch
    {
        private List<Commit> commits;
        private string name;

        public Branch(List<Commit> commits, string name)
        {
            this.commits = commits;
            this.name = name;
        }

        private void addCommit(Commit c)
        {
            commits.Add(c);
        }
    }
}
