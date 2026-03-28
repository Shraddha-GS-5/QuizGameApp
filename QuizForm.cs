using QuizGameApp.Models;

namespace QuizGameApp;

public partial class QuizForm : Form
{
    private readonly QuizState _quizState;
    private readonly Random _random = new();
    private readonly System.Windows.Forms.Timer _questionTimer = new();
    private readonly int _questionTimeLimit;
    private int _timeLeft;

    public QuizForm(QuizState quizState)
    {
        _quizState = quizState;
        InitializeComponent();
        lblCategory.Text = $"Category: {_quizState.Category}";
        lblDifficulty.Text = $"Difficulty: {_quizState.Difficulty}";
        _questionTimeLimit = GetTimeLimitForDifficulty(_quizState.Difficulty);
        _questionTimer.Interval = 1000;
        _questionTimer.Tick += QuestionTimer_Tick;
        ApplyTheme();
        StyleOptionButtons();
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        if (_quizState.CurrentQuestionIndex >= _quizState.Questions.Count)
        {
            _questionTimer.Stop();
            ResultForm resultForm = new(_quizState);
            resultForm.Show();
            Hide();
            return;
        }

        Question question = _quizState.Questions[_quizState.CurrentQuestionIndex];

        lblQuestionNumber.Text = $"Question {_quizState.CurrentQuestionIndex + 1} of {_quizState.Questions.Count}";
        lblQuestion.Text = question.QuestionText;
        lblScore.Text = $"Current Score: {_quizState.Score}";

        List<ShuffledOption> shuffledOptions =
        [
            new ShuffledOption { Label = "A", Text = question.OptionA },
            new ShuffledOption { Label = "B", Text = question.OptionB },
            new ShuffledOption { Label = "C", Text = question.OptionC },
            new ShuffledOption { Label = "D", Text = question.OptionD }
        ];

        shuffledOptions = shuffledOptions.OrderBy(_ => _random.Next()).ToList();

        rbOption1.Checked = false;
        rbOption2.Checked = false;
        rbOption3.Checked = false;
        rbOption4.Checked = false;

        rbOption1.Text = shuffledOptions[0].Text;
        rbOption1.Tag = shuffledOptions[0].Label;

        rbOption2.Text = shuffledOptions[1].Text;
        rbOption2.Tag = shuffledOptions[1].Label;

        rbOption3.Text = shuffledOptions[2].Text;
        rbOption3.Tag = shuffledOptions[2].Label;

        rbOption4.Text = shuffledOptions[3].Text;
        rbOption4.Tag = shuffledOptions[3].Label;

        ResetTimer();
    }

    private void ResetTimer()
    {
        _timeLeft = _questionTimeLimit;
        UpdateTimerDisplay();
        _questionTimer.Stop();
        _questionTimer.Start();
    }

    private void QuestionTimer_Tick(object? sender, EventArgs e)
    {
        _timeLeft--;
        UpdateTimerDisplay();

        if (_timeLeft <= 0)
        {
            _questionTimer.Stop();
            MessageBox.Show("Time is up for this question.", "Timer",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            EvaluateAndMoveNext(null);
        }
    }

    private void btnNext_Click(object? sender, EventArgs e)
    {
        string? selectedAnswer = GetSelectedAnswer();
        EvaluateAndMoveNext(selectedAnswer);
    }

    private string? GetSelectedAnswer()
    {
        if (rbOption1.Checked)
        {
            return rbOption1.Tag?.ToString();
        }

        if (rbOption2.Checked)
        {
            return rbOption2.Tag?.ToString();
        }

        if (rbOption3.Checked)
        {
            return rbOption3.Tag?.ToString();
        }

        if (rbOption4.Checked)
        {
            return rbOption4.Tag?.ToString();
        }

        return null;
    }

    private void EvaluateAndMoveNext(string? selectedAnswer)
    {
        Question currentQuestion = _quizState.Questions[_quizState.CurrentQuestionIndex];

        if (!string.IsNullOrWhiteSpace(selectedAnswer) &&
            string.Equals(selectedAnswer, currentQuestion.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
        {
            _quizState.Score++;
        }

        _quizState.CurrentQuestionIndex++;
        LoadQuestion();
    }

    private int GetTimeLimitForDifficulty(string difficulty)
    {
        return difficulty switch
        {
            "Easy" => 25,
            "Moderate" => 20,
            "Hard" => 15,
            _ => 20
        };
    }

    private void UpdateTimerDisplay()
    {
        lblTimer.Text = $"{_timeLeft}s";
        progressTimer.Maximum = _questionTimeLimit;
        progressTimer.Value = Math.Max(0, Math.Min(_timeLeft, _questionTimeLimit));

        if (_timeLeft <= 5)
        {
            pnlTimerCard.BackColor = Color.MistyRose;
            lblTimer.ForeColor = Color.Firebrick;
            lblTimerCaption.ForeColor = Color.Firebrick;
            lblClockIcon.ForeColor = Color.Firebrick;
        }
        else
        {
            pnlTimerCard.BackColor = Color.FromArgb(232, 245, 233);
            lblTimer.ForeColor = Color.FromArgb(27, 94, 32);
            lblTimerCaption.ForeColor = Color.FromArgb(46, 125, 50);
            lblClockIcon.ForeColor = Color.FromArgb(46, 125, 50);
        }
    }

    private void ApplyTheme()
    {
        BackColor = Color.FromArgb(248, 250, 252);
        pnlHeader.BackColor = Color.FromArgb(15, 23, 42);
        pnlQuestionCard.BackColor = Color.White;
        pnlTimerCard.BackColor = Color.FromArgb(232, 245, 233);
        btnNext.BackColor = Color.FromArgb(37, 99, 235);
        btnNext.ForeColor = Color.White;
        btnNext.FlatStyle = FlatStyle.Flat;
        btnNext.FlatAppearance.BorderSize = 0;
        btnNext.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
    }

    private void StyleOptionButtons()
    {
        foreach (RadioButton optionButton in GetOptionButtons())
        {
            optionButton.Padding = new Padding(16, 0, 0, 0);
            optionButton.BackColor = Color.White;
            optionButton.ForeColor = Color.FromArgb(30, 41, 59);
            optionButton.FlatAppearance.BorderSize = 1;
        }
    }

    private IEnumerable<RadioButton> GetOptionButtons()
    {
        return [rbOption1, rbOption2, rbOption3, rbOption4];
    }

    private void OptionRadioButton_CheckedChanged(object? sender, EventArgs e)
    {
        foreach (RadioButton optionButton in GetOptionButtons())
        {
            bool isSelected = optionButton.Checked;
            optionButton.BackColor = isSelected ? Color.FromArgb(219, 234, 254) : Color.White;
            optionButton.ForeColor = isSelected ? Color.FromArgb(30, 64, 175) : Color.FromArgb(30, 41, 59);
            optionButton.FlatAppearance.BorderColor = isSelected
                ? Color.FromArgb(59, 130, 246)
                : Color.FromArgb(203, 213, 225);
        }
    }

    private void QuizForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }
}
