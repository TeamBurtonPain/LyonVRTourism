using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    enum RoleAccount
    {
        USER,
        CREATOR,
        ADMIN
    }
    class Account : User
    {
        private string mail;
        private string password;
        private RoleAccount role;
    }
}
