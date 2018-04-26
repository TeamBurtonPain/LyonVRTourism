using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;


public enum RoleAccount
{
    USER = 0,
    CREATOR,
    ADMIN
}

public class Account : User
{
    private string mail;
    private string password;
    private string firstName;
    private string lastName;
    private DateTime dateBirth;
    private RoleAccount role;
    private DateTime creationDate;
    private DateTime updateDate;
    private long elapsedTime;


    public Account()
    {
        dateBirth = DateTime.Now;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class based on a <see cref="User"/> instance.
    /// </summary>
    /// <param name="u">The user.</param>
    /// <param name="mail">The mail.</param>
    /// <param name="password">The hash of password.</param>
    /// <param name="r">The role.</param>   
    public Account(User u, string mail, string password, string firstName, string lastName, DateTime dateBirth, RoleAccount r) 
        : base(u)
    {
        this.mail = mail;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateBirth = dateBirth;
        this.role = r;
    }

    public string Mail
    {
        get { return mail; }
        set { mail = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public DateTime DateBirth
    {
        get { return dateBirth; }
        set { dateBirth = value; }
    }

    public RoleAccount Role
    {
        get { return role; }
        set { role = value; }
    }

    public DateTime CreationDate
    {
        get { return creationDate; }
    }

    public DateTime UpdateDate
    {
        get { return updateDate; }
    }

    public long ElapsedTime
    {
        get { return elapsedTime; }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}