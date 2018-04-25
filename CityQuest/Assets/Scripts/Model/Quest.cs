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

    private static long instanceCounter = 0;
    private long id;
    private Coordinates geolocalisation;
    private string description;
    private long value;
    private QuestStatistics statistics;
    private bool open;
    private Creator creator;
    private List<CheckPoint> checkpoints;

    /// <summary>
    /// Initializes a new fully initialized instance of the <see cref="Quest"/> class.
    /// </summary>
    /// <param name="geolocalisation">The geolocalisation.</param>
    /// <param name="description">The description.</param>
    /// <param name="statistics">The statistics.</param>
    /// <param name="creator">The creator.</param>
    /// <param name="checkpoints">The checkpoints.</param>
    Quest(Coordinates geolocalisation, string description, long value, QuestStatistics statistics, 
        Creator creator, List<CheckPoint> checkpoints)
    {
        this.id = instanceCounter++;
        this.geolocalisation = geolocalisation;
        this.description = description;
        this.value = value;
        this.statistics = statistics;
        this.creator = creator;
        this.checkpoints = checkpoints;
    }

    public long Id
    {
        get { return id; }
    }

    public Coordinates Geolocalisation
    {
        get { return geolocalisation; }
        set { geolocalisation = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    //BEWARE : if the value changes after constructor -> user xp may be inconsistent !
    public long Value
    {
        get { return this.value; }
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