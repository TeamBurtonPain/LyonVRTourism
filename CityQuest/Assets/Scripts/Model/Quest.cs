using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Assets.Scripts.Model
{
    class Quest
    {
        private long id;
        private Tuple<Decimal, Decimal> geolocalisation;
        private string description;
        private StatsQuest statistics;
        private Boolean open;
        private string creator;
        private List<CheckPoint> checkpoints;

    }
}
