using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using NUnit.Framework.Constraints;


public enum RoleAccount
{
    USER,
    CREATOR,
    ADMIN
}

public class Account : User
{
    private User user;
    private string mail;
    private string password;
    private RoleAccount role;

    public Account(User u, string mail, string password, RoleAccount r) : base(u.Username, u.Id, u.Xp, u.Badges,
        u.Quests)
    {
        this.user = u;
        this.mail = mail;
        this.password = password;
        this.role = r;
    }

    public User User
    {
        get { return user; }
    }

    public string Mail
    {
        get { return mail; }
    }

    public string Password
    {
        get { return password; }
    }

    public RoleAccount Role
    {
        get { return role; }
    }
}