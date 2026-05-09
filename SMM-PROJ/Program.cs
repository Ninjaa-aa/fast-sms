using SMM_PROJ.Config;
using SMM_PROJ.Forms.Auth;

namespace SMM_PROJ
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Loads environment configuration and launches the login form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            EnvConfig.Load();

            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}
