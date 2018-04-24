using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class Coordinates
    {
        public float x;
        public float y;
    }
    public class Quest
    {
        private long id;
        private static long instanceCounter = 0;
        private Coordinates geolocalisation;
        private string description;
        private StatsQuest statistics;
        private bool open;
        private Creator creator;
        private List<CheckPoint> checkpoints;

        Quest(Coordinates geolocalisation,string description,StatsQuest statistics,Creator creator,List<CheckPoint> checkpoints){
            this.id = instanceCounter++;
            this.geolocalisation = geolocalisation;
            this.description = description;
            this.statistics = statistics;
            this.creator = creator;
            this.checkpoints = checkpoints;
        }

        public Coordinates Geolocalisation
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
