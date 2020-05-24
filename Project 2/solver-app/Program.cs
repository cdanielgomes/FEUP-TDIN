using System;
using Gtk;

namespace Solver
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            Application.Init();

            var app = new Application("org.solver_app.solver_app", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            SolverApp.Init(app);

            var win = new LoginWindow();
            app.AddWindow(win);
            
            win.Show();
            Application.Run();
        }
    }
}
