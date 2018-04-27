using System.Collections.Generic;



public class StateQuest
{
    private Quest quest;
    private bool done;
    private double score;
    private double timeElapsed;
    private List<StateCheckPoint> checkpoints;


    /// <summary>
    /// Initializes a new instance of the <see cref="StateQuest"/> class.
    /// The quest is 0% initialized
    /// </summary>
    /// <param name="q">The q.</param>
    public StateQuest(Quest q)
    {
        quest = q;
        done = false;

        //init checkpoints state to 0%
        checkpoints = new List<StateCheckPoint>();
        for (int i = 0; i < quest.Checkpoints.Count; ++i)
        {
            checkpoints.Add(new StateCheckPoint(q.Checkpoints[i]));
        }
    }

    /// <summary>
    /// Checks the quest and set the attribute donce to true if the quest is finished.
    /// </summary>
    /// <returns>
    /// true if the quest is finished, false else.
    /// </returns>
    public bool CheckQuest()
    {
        foreach (var checkpoint in Checkpoints)
        {
            if (checkpoint.Status != StatusCheckPoint.FINISHED)
            {
                done = false;
                return false;
            }
        }
        done = true;
        return true;
    }

    public void refreshTime()
    {
        double sum = 0;
        foreach (var checkPoint in Checkpoints)
        {
            sum += checkPoint.TimeElapsed;
        }

        if (sum != timeElapsed)
        {
            //TODO ? log
        }
        timeElapsed = sum;

    }

    public Quest Quest
    {
        get { return quest; }
    }

    public bool Done
    {
        get { return done; }
        set { done = value; }
    }

    public double Score
    {
        get { return score; }
        set { score = value; }
    }

    public double TimeElapsed
    {
        get { return timeElapsed; }
        set { timeElapsed = value; }
    }

    public List<StateCheckPoint> Checkpoints
    {
        get { return checkpoints; }
    }
}