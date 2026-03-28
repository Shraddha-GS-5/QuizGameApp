using QuizGameApp.Data;

namespace QuizGameApp;

public partial class ResultForm : Form
{
    private readonly QuizState _quizState;

    public ResultForm(QuizState quizState)
    {
        _quizState = quizState;
        InitializeComponent();
        ApplyLeaderboardTheme();
    }

    private void ResultForm_Load(object? sender, EventArgs e)
    {
        lblResult.Text = $"{_quizState.UserName}, your final score is {_quizState.Score} out of {_quizState.Questions.Count}.";

        try
        {
            DatabaseHelper.SaveScore(_quizState.UserId, _quizState.Score);
            lblSaveStatus.Text = "Your score has been saved successfully.";
        }
        catch (Exception ex)
        {
            lblSaveStatus.Text = "Score could not be saved.";
            MessageBox.Show($"Unable to save score.\n\n{ex.Message}", "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        try
        {
            dgvLeaderboard.DataSource = DatabaseHelper.GetTopScores(5);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to load leaderboard.\n\n{ex.Message}", "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnPlayAgain_Click(object? sender, EventArgs e)
    {
        LoginForm loginForm = new();
        loginForm.Show();
        Hide();
    }

    private void btnExit_Click(object? sender, EventArgs e)
    {
        Application.Exit();
    }

    private void ApplyLeaderboardTheme()
    {
        dgvLeaderboard.EnableHeadersVisualStyles = false;
        dgvLeaderboard.BackgroundColor = Color.White;
        dgvLeaderboard.BorderStyle = BorderStyle.None;
        dgvLeaderboard.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
        dgvLeaderboard.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvLeaderboard.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dgvLeaderboard.DefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        dgvLeaderboard.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        dgvLeaderboard.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dgvLeaderboard.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
        dgvLeaderboard.GridColor = Color.FromArgb(226, 232, 240);
    }
}
