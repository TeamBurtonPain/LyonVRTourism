using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Creator : Account
{
    private List<Quest> creations;

    public Creator(Account a) 
        : base(new User(a.Username, a.Id, a.Xp, a.Badges, a.Quests), a.Mail, a.Password, RoleAccount.CREATOR)
    {
        this.creations = new List<Quest>();
    }

    public List<Quest> Creations
    {
        get { return creations; }
    }

    public void Create(Quest q)
    {
        if (Equals(q.Creator))
        {
            creations.Add(q);
        }
        else
        {
            //TODO : Gestion erreur.
        }
    }
}