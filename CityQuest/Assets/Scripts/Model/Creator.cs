using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Creator : Account
{
    private List<Quest> creations;

    public Creator(Account a) : base(a.User, a.Mail, a.Password, RoleAccount.CREATOR)
    {
        this.creations = new List<Quest>();
    }

    public List<Quest> Creations
    {
        get { return creations; }
    }

    public void create(Quest q)
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