using QuizGameApp.Data;

namespace QuizGameApp;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        try
        {
            DatabaseHelper.EnsureDatabaseSetup();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Database setup failed.\n\n{ex.Message}",
                "Startup Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        Application.Run(new LoginForm());
    }
}
