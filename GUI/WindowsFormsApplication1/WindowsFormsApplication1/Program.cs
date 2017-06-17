using System;
using System.Windows.Forms;

namespace GUI_Namespace
{
    
    static class Program
    {
        static public MainWindow mainWindow;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainWindow = new MainWindow();
            Application.Run(mainWindow);
        }
    }
}
