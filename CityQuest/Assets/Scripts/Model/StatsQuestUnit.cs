using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class StatsQuestUnit
    {
        private string comment;
        private int mark;
        private User associatedUser;

        public StatsQuestUnit(User user)
        {
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

        public User AssociatedUser
        {
            get { return associatedUser; }
        }
    }
}
