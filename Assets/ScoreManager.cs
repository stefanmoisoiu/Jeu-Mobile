public static class ScoreManager
{
    public delegate void OnScoreChange(int newScoreValue);
    public static event OnScoreChange onScoreChange;
    public static int score { get; private set; }

    public static void AddScore(int amount)
    {
        score += amount;
        onScoreChange?.Invoke(score);
    }
    public static void ResetScore()
    {
        score = 0;
        onScoreChange?.Invoke(score);
    }
}
