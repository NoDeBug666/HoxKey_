using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HoxKey
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Mutex mutex = new Mutex(false, GUID))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("This application is running,You can't open it again at same time ob'_'ov","Oops",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                    return;
                }
                Form1 f = new Form1();
                Application.Run(f);
                mutex.ReleaseMutex();
            }
        }

        public const string GUID = "760AFBC1-78D0-49CC-AB4B-5AB6990299FF";
    }
}
