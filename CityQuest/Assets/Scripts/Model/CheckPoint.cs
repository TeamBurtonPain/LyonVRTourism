using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CheckPoint
{
    private long id;
    private static long instanceCounter = 0;
    private string picture;
    private string text;
    private List<string> choices;
    private string answer;

    CheckPoint(string pic, string text, List<string> choices, string answer)
    {
        this.id = instanceCounter++;
        this.picture = pic;
        this.text = text;
        this.choices = choices;
        this.answer = answer;
    }

    public long Id
    {
        get { return id; }
    }


    public string Picture
    {
        get { return picture; }
        set { picture = value; }
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public List<string> Choices
    {
        get { return choices; }
    }

    public string Answer
    {
        get { return answer; }
        set { answer = value; }
    }
}