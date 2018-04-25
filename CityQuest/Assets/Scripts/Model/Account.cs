using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;


public enum RoleAccount
{
    USER,
    CREATOR,
    ADMIN
}

public class Account : User
{
    private string mail;
    private string password;
    private RoleAccount role;

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class based on a <see cref="User"/> instance.
    /// </summary>
    /// <param name="u">The user.</param>
    /// <param name="mail">The mail.</param>
    /// <param name="password">The hash of password.</param>
    /// <param name="r">The role.</param>
    public Account(User u, string mail, string password, RoleAccount r) 
        : base(u)
    {
        this.mail = mail;
        this.password = password;
        this.role = r;
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