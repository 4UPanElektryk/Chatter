using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatter.Client
{
    internal static class Program
    {
        public static SimpleTcpClient _Client;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _Client = new SimpleTcpClient
            {
                StringEncoder = Encoding.UTF8,
                AutoTrimStrings = true,
                Delimiter = (byte)'\n',
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
