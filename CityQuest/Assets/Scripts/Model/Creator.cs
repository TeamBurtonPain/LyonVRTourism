using System.Collections.Generic;



public class Creator : Account
{
    private List<Quest> creations;


    public Creator()
    {
        creations = new List<Quest>();
    }
    public Creator(Account a) 
        : base(new User(a.Username, a.Id, a.Xp, a.Badges, a.Quests), 
            a.Mail, a.Password, a.FirstName, a.LastName, RoleAccount.CREATOR)
    {
        this.creations = new List<Quest>();
    }

    public List<Quest> Creations
    {
        get { return creations; }
    }

    public void Create(Quest q)
    {
        if (Equals(q.IdCreator))
        {
            creations.Add(q);
        }
        else
        {
            //TODO : Gestion erreur.
        }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}