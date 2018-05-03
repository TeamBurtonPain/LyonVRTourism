


public class Badge
{
    private string id;
    private string name;
    private string description;
    private string iconPath;

    private long xp;

    public Badge(string n, string d, long v)
    {
        name = n;
        description = d;
        xp = v;
    }public Badge(string id, string n, string d, long v)
    {
        this.id = id;
        name = n;
        description = d;
        xp = v;
    }

    public string Id
    {
        get { return id; }
    }

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public string IconPath
    {
        get { return iconPath;  }
    }

    public long Xp
    {
        get { return xp; }
    }
    public override string ToString()
    {
        return "Badge : id : " + id + ", name : " + name + ", description : " + description + ", xp : " + xp;
    }

}