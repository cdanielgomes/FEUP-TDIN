using System;
using Gtk;

namespace Department
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            Application.Init();

            var app = new Application("org.department_app.department_app", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            DepartmentApp.Init(app);

            var win = new LoginWindow();
            app.AddWindow(win);
            
            win.Show();
            Application.Run();
        }
    }
}
