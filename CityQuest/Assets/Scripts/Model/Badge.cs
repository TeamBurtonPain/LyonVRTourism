using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Badge
{
    private string name;
    private string description;
    private long value;

    public Badge(string n, string d, long v)
    {
        name = n;
        description = d;
        value = v;
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