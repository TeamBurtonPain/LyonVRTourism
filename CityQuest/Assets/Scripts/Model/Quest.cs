using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Coordinates
{
    public float x;
    public float y;
}

[Serializable]
public class Quest
{
    private static long instanceCounter = 0;
    [SerializeField]
    private long id;
    [SerializeField]
    private Coordinates geolocalisation;
    [SerializeField]
    private string title;
    [SerializeField]
    private string description;
    [SerializeField]
    private long value;
    [SerializeField]
    private double timeLength;

    [NonSerialized]
    private QuestStatistics statistics;
    [SerializeField]
    private bool open;

    [NonSerialized]
    private Creator creator;

    [NonSerialized]
    private List<CheckPoint> checkpoints;


    public Quest()
    {
        timeLength = -1;
        geolocalisation = new Coordinates();
        statistics = new QuestStatistics(this);
        checkpoints = new List<CheckPoint>();
        creator = new Creator();
        
    }
    /// <summary>
    /// Initializes a new fully initialized instance of the <see cref="Quest"/> class.
    /// </summary>
    /// <param name="geolocalisation">The geolocalisation.</param>
    /// <param name="description">The description.</param>
    /// <param name="creator">The creator.</param>
    /// <param name="checkpoints">The checkpoints.</param>
    public Quest(Coordinates geolocalisation, string title, string description, long value, 
         Creator creator, List<CheckPoint> checkpoints)
    {
        this.title = title;
        this.id = instanceCounter++;
        this.geolocalisation = geolocalisation;
        this.description = description;
        this.value = value;
        timeLength = -1;
        statistics = new QuestStatistics(this);
        this.creator = creator;
        this.checkpoints = checkpoints;

    }

    //TODO : Request API time length
    public void EstimateTimeLength()
    {

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

    public string Title
    {
        get { return title; }
        set { title = value; }
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

    public double TimeLength
    {
        get { return timeLength; }
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
        set { creator = value; }
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

    public override string ToString()
    {
        return "Quest : id : " + id + ", title : " + title + ", at (" + geolocalisation.x + ", " + geolocalisation.y + "), description : " +
               description + "\n\t" +  string.Join(",\n\t", checkpoints.Select(x => x.ToString()).ToArray());
    }

    
}
