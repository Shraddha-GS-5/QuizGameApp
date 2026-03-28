namespace QuizGameApp;

internal static class RecentQuestionTracker
{
    private const int MaxRememberedQuestions = 12;
    private static readonly Dictionary<string, Queue<int>> RecentQuestions = [];

    public static List<int> GetRecentQuestionIds(string category, string difficulty)
    {
        string key = BuildKey(category, difficulty);

        if (!RecentQuestions.TryGetValue(key, out Queue<int>? queue))
        {
            return [];
        }

        return queue.ToList();
    }

    public static void RememberQuestions(string category, string difficulty, IEnumerable<int> questionIds)
    {
        string key = BuildKey(category, difficulty);

        if (!RecentQuestions.TryGetValue(key, out Queue<int>? queue))
        {
            queue = new Queue<int>();
            RecentQuestions[key] = queue;
        }

        foreach (int questionId in questionIds)
        {
            if (queue.Contains(questionId))
            {
                continue;
            }

            queue.Enqueue(questionId);

            while (queue.Count > MaxRememberedQuestions)
            {
                queue.Dequeue();
            }
        }
    }

    private static string BuildKey(string category, string difficulty)
    {
        return $"{category}|{difficulty}";
    }
}
