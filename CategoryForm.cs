using QuizGameApp.Data;
using QuizGameApp.Models;

namespace QuizGameApp;

public partial class CategoryForm : Form
{
    private readonly int _userId;
    private readonly string _userName;

    public CategoryForm(int userId, string userName)
    {
        _userId = userId;
        _userName = userName;
        InitializeComponent();
        lblWelcome.Text = $"Welcome, {_userName}";
    }

    private void btnStartQuiz_Click(object? sender, EventArgs e)
    {
        string? selectedCategory = GetSelectedCategory();
        string? selectedDifficulty = GetSelectedDifficulty();

        if (string.IsNullOrWhiteSpace(selectedCategory))
        {
            MessageBox.Show("Please select a category.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (string.IsNullOrWhiteSpace(selectedDifficulty))
        {
            MessageBox.Show("Please select a difficulty level.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            if (!DatabaseHelper.HasEnoughQuestions(selectedCategory, selectedDifficulty, 5))
            {
                MessageBox.Show("This category and difficulty do not have at least 5 questions yet.",
                    "Not Enough Questions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> recentQuestionIds = RecentQuestionTracker.GetRecentQuestionIds(selectedCategory, selectedDifficulty);
            List<Question> questions = DatabaseHelper.GetRandomQuestions(selectedCategory, selectedDifficulty, 5, recentQuestionIds);
            RecentQuestionTracker.RememberQuestions(selectedCategory, selectedDifficulty, questions.Select(question => question.QuestionId));
            QuizState quizState = new(_userId, _userName, selectedCategory, selectedDifficulty, questions);
            QuizForm quizForm = new(quizState);
            quizForm.Show();
            Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to load quiz questions.\n\n{ex.Message}", "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string? GetSelectedCategory()
    {
        if (rbProgramming.Checked)
        {
            return "Programming";
        }

        if (rbGK.Checked)
        {
            return "GK";
        }

        if (rbAptitude.Checked)
        {
            return "Aptitude";
        }

        return null;
    }

    private string? GetSelectedDifficulty()
    {
        if (rbEasy.Checked)
        {
            return "Easy";
        }

        if (rbModerate.Checked)
        {
            return "Moderate";
        }

        if (rbHard.Checked)
        {
            return "Hard";
        }

        return null;
    }

    private void btnLeaderboard_Click(object? sender, EventArgs e)
    {
        using LeaderboardForm leaderboardForm = new();
        leaderboardForm.ShowDialog();
    }

    private void CategoryForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }
}
