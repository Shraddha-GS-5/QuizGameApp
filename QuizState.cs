using QuizGameApp.Models;

namespace QuizGameApp;

public class QuizState
{
    public QuizState(int userId, string userName, string category, string difficulty, List<Question> questions)
    {
        UserId = userId;
        UserName = userName;
        Category = category;
        Difficulty = difficulty;
        Questions = questions;
    }

    public int UserId { get; }

    public string UserName { get; }

    public string Category { get; }

    public string Difficulty { get; }

    public List<Question> Questions { get; }

    public int CurrentQuestionIndex { get; set; }

    public int Score { get; set; }
}
