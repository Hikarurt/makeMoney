using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;

namespace zj
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MDIMain mdimain = new MDIMain();
            //atest mdimain = new atest();
            Application.Run(mdimain);
        }
    }
}
