using System;

[Serializable]
public class HighScoreElement
{
    public int Points;

    public HighScoreElement(int points)
    {
        this.Points = points;
    }
}
