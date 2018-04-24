using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class QuestStatisticsUnit
{
    
    private string comment;
    private int mark;
    private Quest quest;
    private User associatedUser;

    public QuestStatisticsUnit(Quest quest, User user)
    {
        this.quest = quest;
        this.associatedUser = user;
    }

    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }

    public int Mark
    {
        get { return mark; }
        set { mark = value; }
    }

    public Quest Quest
    {
        get { return quest; }
    }

    public User AssociatedUser
    {
        get { return associatedUser; }
    }

    protected bool Equals(QuestStatisticsUnit other)
    {
        return associatedUser.Equals(other.associatedUser) && quest.Equals(other.quest);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((QuestStatisticsUnit) obj);
    }

    public override int GetHashCode()
    {
        return (associatedUser != null ? associatedUser.GetHashCode() : 0);
    }


}