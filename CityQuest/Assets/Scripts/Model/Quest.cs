using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;


public class Coordinates
{
    public float x;
    public float y;

    public Coordinates(float x, float y)
    {
        this.x = x;
        this.y = y;
    }public Coordinates()
    {}
}

public class Quest
{
    private string id;
    private Coordinates geolocalisation;
    private string title;
    private string description;
    private int experienceEarned;
    private double timeLength;
    private QuestStatistics statistics;
    private bool open;
    private string idCreator;
    private List<CheckPoint> checkpoints;
    private DateTime creationDate;
    private DateTime updateDate;


    public Quest()
    {
        timeLength = -1;
        geolocalisation = new Coordinates();
        statistics = new QuestStatistics(this);
        checkpoints = new List<CheckPoint>();
        
    }
    /// <summary>
    /// Initializes a new fully initialized instance of the <see cref="Quest"/> class.
    /// </summary>
    /// <param name="geolocalisation">The geolocalisation.</param>
    /// <param name="description">The description.</param>
    /// <param name="creator">The creator.</param>
    /// <param name="checkpoints">The checkpoints.</param>
    public Quest(Coordinates geolocalisation, string title, string description, int experienceEarned, 
         string idCreator, List<CheckPoint> checkpoints)
    {
        this.title = title;
        this.geolocalisation = geolocalisation;
        this.description = description;
        this.experienceEarned = experienceEarned;
        timeLength = -1;
        statistics = new QuestStatistics(this);
        this.idCreator = idCreator;
        this.checkpoints = checkpoints;

    }

    public Quest(string id, Coordinates geolocalisation, string title, string description, int experienceEarned,
         string idCreator, List<CheckPoint> checkpoints)
    {
        this.id = id;
        this.title = title;
        this.geolocalisation = geolocalisation;
        this.description = description;
        this.experienceEarned = experienceEarned;
        timeLength = -1;
        statistics = new QuestStatistics(this);
        this.idCreator = idCreator;
        this.checkpoints = checkpoints;

    }

    //TODO : Request API time length
    public void EstimateTimeLength()
    {

    }

    public string Id
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
        get { return this.experienceEarned; }
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

    public string IdCreator
    {
        get { return idCreator; }
        set { idCreator = value; }
    }

    public List<CheckPoint> Checkpoints
    {
        get { return checkpoints; }
    }

    public DateTime CreationDate
    {
        get { return creationDate; }
    }

    public DateTime UpdateDate
    {
        get { return updateDate; }
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
