using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class StateQuest
{
    private Quest quest;
    private bool done;
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
        checkpoints = new List<StateCheckPoint>(quest.Checkpoints.Count);
        for (int i = 0; i < quest.Checkpoints.Count; ++i)
        {
            checkpoints[i] = new StateCheckPoint(q.Checkpoints[i]);
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

    public Quest Quest
    {
        get { return quest; }
    }

    public bool Done
    {
        get { return done; }
        set { done = value; }
    }

    public List<StateCheckPoint> Checkpoints
    {
        get { return checkpoints; }
    }
}