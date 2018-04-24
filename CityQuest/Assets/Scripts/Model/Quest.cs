using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class Coordonates
    {
        private Decimal x;
        private Decimal y;
    }
    public class Quest
    {
        private long id;
        private static long instanceCounter = 0;
        private Coordonates geolocalisation;
        private string description;
        private StatsQuest statistics;
        private bool open;
        private Creator creator;
        private List<CheckPoint> checkpoints;

        Quest(){
            this.id = instanceCounter++;
        }

        public Coordonates Geolocalisation
        {
            get { return geolocalisation; }
        }

        public string Description
        {
            get { return description; }
        }

        public bool Open
        {
            get { return open; }
        }

        public StatsQuest Statistics
        {
            get { return statistics; }
        }

        public Creator Creator
        {
            get { return creator; }
        }

        public List<CheckPoint> Checkpoints
        {
            get { return checkpoints; }
        }
    }
}
