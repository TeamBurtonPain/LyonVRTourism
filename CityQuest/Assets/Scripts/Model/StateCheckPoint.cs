

public enum StatusCheckPoint
{
    FINISHED = 0,
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
        checkpoint = cp;
        status = StatusCheckPoint.UNINIT;
    }

    public StateCheckPoint(CheckPoint cp, StatusCheckPoint status, double timeElapsed)
    {
        checkpoint = cp;
        this.status = status;
        this.timeElapsed = timeElapsed;
    }

    public StateCheckPoint(CheckPoint cp, StatusCheckPoint status)
    {
        checkpoint = cp;
        this.status = status;
    }

    public CheckPoint Checkpoint
    {
        get { return checkpoint; }
        set { checkpoint = value; }
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