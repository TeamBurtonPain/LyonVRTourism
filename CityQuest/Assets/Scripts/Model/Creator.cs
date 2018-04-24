using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class Creator : Account
    {
        private List<Quest> creations;

        public Creator(Account a) : base(a.User,a.Mail,a.Password)
        {
            this.creations = new List<Quest>();
        }

        public List<Quest> Creations
        {
            get { return creations; }
        }
    }
}
