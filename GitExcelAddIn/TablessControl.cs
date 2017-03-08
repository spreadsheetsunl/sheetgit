using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class TablessControl : TabControl
{

    public TablessControl()
    {
        bool designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
        if (!designMode) Multiline = true;
    }
    protected override void WndProc(ref Message m)
    {
        // Hide tabs by trapping the TCM_ADJUSTRECT message
        if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
        else base.WndProc(ref m);
    }
}
