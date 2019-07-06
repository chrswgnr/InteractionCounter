using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractionCounter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var keyInterceptor = new KeyInterceptor())
            using (var mouseInterceptor = new MouseInterceptor())
            {
                Application.Run(new InteractionCounterForm(keyInterceptor, mouseInterceptor));
            }
        }
    }
}
