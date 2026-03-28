using QuizGameApp.Data;

namespace QuizGameApp;

public partial class LeaderboardForm : Form
{
    public LeaderboardForm()
    {
        InitializeComponent();
        ApplyGridTheme();
    }

    private void LeaderboardForm_Load(object? sender, EventArgs e)
    {
        try
        {
            dgvLeaderboard.DataSource = DatabaseHelper.GetTopScores(10);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to load leaderboard.\n\n{ex.Message}", "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyGridTheme()
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
