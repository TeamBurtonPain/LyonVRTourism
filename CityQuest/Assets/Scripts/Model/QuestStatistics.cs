using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


public class QuestStatistics
{
    private Quest quest;

    private HashSet<QuestStatisticsUnit> marks;

    public QuestStatistics(Quest q)
    {
        quest = q;
        marks = new HashSet<QuestStatisticsUnit>();
    }

    /// <summary>
    /// Adds the comment.
    /// If a comment from the same <see cref="User"/> was already in Collection, previous is erased by the new one.
    /// </summary>
    /// <param name="qsu">The qsu.</param>
    /// <returns>
    /// true if the size of marks increases
    /// false if the size of marks remains
    /// </returns>
    public bool AddComment(QuestStatisticsUnit qsu)
    {
        if (marks.Contains(qsu))
        {
            marks.Remove(qsu);
            marks.Add(qsu);
            return false;
        }
        else
        {
            marks.Add(qsu);
            return true;
        }
    }

    /// <summary>
    /// Computs the mean of this quest.
    /// Complexity O(n), n = number of comments
    /// </summary>
    /// <returns>
    /// The mean.
    /// </returns>
    public double ComputeMean()
    {
        double sum = 0;
        foreach (var mark in marks)
        {
            sum += mark.Mark;
        }
        sum /= marks.Count;
        return sum;
    }

    public Quest Quest
    {
        get { return quest; }
    }
    public HashSet<QuestStatisticsUnit> Marks
    {
        get { return marks; }
    }
}