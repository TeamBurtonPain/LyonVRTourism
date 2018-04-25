using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Badge
{
    private static long instanceCounter = 0;
    private long id;
    private string name;
    private string description;
    private long value;

    public Badge(string n, string d, long v)
    {
        id = instanceCounter++;
        name = n;
        description = d;
        value = v;
    }

    public long Id
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

    public long Value
    {
        get { return value; }
    }
}