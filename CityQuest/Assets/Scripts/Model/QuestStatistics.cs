using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class QuestStatistics
{
    private List<QuestStatisticsUnit> marks;

    public QuestStatistics()
    {
        marks = new List<QuestStatisticsUnit>();
    }

    public List<QuestStatisticsUnit> Marks
    {
        get { return marks; }
    }
}