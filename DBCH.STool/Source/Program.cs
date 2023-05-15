using DBCH.STool.GUI;
using System;
using System.Windows.Forms;

namespace DBCH.STool
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _ = Manager.ManagerProvider.UserManager;
            _ = Manager.ManagerProvider.ServerManager;
            _ = Manager.ManagerProvider.ServerManager;
            new AuthGUI().Show();
            Application.Run();
        }
    }
}
