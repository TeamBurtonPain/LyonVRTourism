using System.Collections.Generic;



public class CheckPoint
{

    /// <summary>
    /// The picture in Base64 ! Can be large.
    /// </summary>
    private string picture;

    /// <summary>
    /// The unique picture name
    /// </summary>
    private string pictureName;
    private string text;
    private string question;
    private List<string> choices;
    private string answer;
    private int difficulty;
    private string idBadge;

    public CheckPoint()
    {
        choices = new List<string>();
    }

    public CheckPoint(string pic, string picName, string text, List<string> choices, string answer, int difficulty)

    {
        this.picture = pic;
        this.pictureName = picName;
        this.text = text;
        this.choices = choices;
        this.answer = answer;
        this.difficulty = difficulty;
    }


    public string Picture
    {
        get { return picture; }
        set { picture = value; }
    }

    public string PictureName
    {
        get { return pictureName; }
        set { pictureName = value; }
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public string Question
    {
        get { return question; }
        set { question = value; }
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

    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

    public override string ToString()
    {
        return "Checkpoint : text : " + text + ", answer : " + answer;
    }
}