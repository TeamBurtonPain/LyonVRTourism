using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class StatsQuest
    {
        private List<StatsQuestUnit> marks;

        public StatsQuest()
        {
            marks = new List<StatsQuestUnit>();
        }

        public List<StatsQuestUnit> Marks
        {
            get { return marks; }
        }
    }
}
