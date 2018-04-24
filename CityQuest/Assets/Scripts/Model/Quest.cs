using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Coordinates
{
    public float x;
    public float y;
}

public class Quest
{
    private long id;
    private static long instanceCounter = 0;
    private Coordinates geolocalisation;
    private string description;
    private QuestStatistics statistics;
    private bool open;
    private Creator creator;
    private List<CheckPoint> checkpoints;

    Quest(Coordinates geolocalisation, string description, QuestStatistics statistics, Creator creator,
        List<CheckPoint> checkpoints)
    {
        this.id = instanceCounter++;
        this.geolocalisation = geolocalisation;
        this.description = description;
        this.statistics = statistics;
        this.creator = creator;
        this.checkpoints = checkpoints;
    }

    public Coordinates Geolocalisation
    {
        get { return geolocalisation; }
    }

    public string Description
    {
        get { return description; }
    }

    public bool Open
    {
        get { return open; }
        set { open = value; }
    }

    public QuestStatistics Statistics
    {
        get { return statistics; }
    }

    public Creator Creator
    {
        get { return creator; }
    }

    public List<CheckPoint> Checkpoints
    {
        get { return checkpoints; }
    }

    protected bool Equals(Quest other)
    {
        return id == other.id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Quest) obj);
    }


    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}