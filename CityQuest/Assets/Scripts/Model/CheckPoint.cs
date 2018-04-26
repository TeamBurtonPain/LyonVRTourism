using System.Collections.Generic;



public class CheckPoint
{
    private static long instanceCounter = 0;
    private long id;
    private string picture;
    private string text;
    private List<string> choices;
    private string answer;

    public CheckPoint()
    {
        choices = new List<string>();
    }

    public CheckPoint(string pic, string text, List<string> choices, string answer)
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
        set { id = value; }
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

    public override string ToString()
    {
        return "Checkpoint : id : " + id + ", text : " + text + ", answer : " + answer;
    }
}