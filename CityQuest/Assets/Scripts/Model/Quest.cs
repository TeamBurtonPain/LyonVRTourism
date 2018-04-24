using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    class Coordonates
    {
        private Decimal x;
        private Decimal y;
    }
    class Quest
    {
        private long id;
        private Coordonates geolocalisation;
        private string description;
        private StatsQuest statistics;
        private Boolean open;
        private string creator;
        private List<CheckPoint> checkpoints;

    }
}
