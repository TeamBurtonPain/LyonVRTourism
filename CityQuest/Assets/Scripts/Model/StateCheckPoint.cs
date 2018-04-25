using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum StatusCheckPoint
{
    FINISHED,
    BEGUN,
    UNINIT
}

public class StateCheckPoint
{
    private CheckPoint checkpoint;
    private StatusCheckPoint status;
    private double timeElapsed;

    public StateCheckPoint(CheckPoint cp)
    {
        this.checkpoint = cp;
        status = StatusCheckPoint.UNINIT;
    }

    public StateCheckPoint(CheckPoint cp, StatusCheckPoint status)
    {
        this.checkpoint = cp;
        this.status = status;
    }

    public CheckPoint Checkpoint
    {
        get { return checkpoint; }
    }

    public StatusCheckPoint Status
    {
        get { return status; }
        set { status = value; }
    }

    public double TimeElapsed
    {
        get { return timeElapsed; }
        set { timeElapsed = value; }
    }
}