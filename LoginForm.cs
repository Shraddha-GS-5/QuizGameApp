using QuizGameApp.Data;

namespace QuizGameApp;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    private void btnStart_Click(object? sender, EventArgs e)
    {
        string userName = txtName.Text.Trim();

        if (string.IsNullOrWhiteSpace(userName))
        {
            MessageBox.Show("Please enter your name before continuing.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtName.Focus();
            return;
        }

        try
        {
            int userId = DatabaseHelper.SaveUser(userName);
            CategoryForm categoryForm = new(userId, userName);
            categoryForm.Show();
            Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to save user details.\n\n{ex.Message}", "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoginForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }
}
