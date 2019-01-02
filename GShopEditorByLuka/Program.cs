using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace GShopEditorByLuka
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            UIExceptionHandlerWinForms.UIException.Start("SmtpServer",
    26,
    "Password",
    "User",
    "bugreportaccountlb",
    "user@gmail.com",
    "Exception",
    "Gshop Editor"
    );
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
            {
                Application.Run(new Form1(args[0]));
            }
            else
            {
                Application.Run(new Form1());
            }
        }

    }
}


