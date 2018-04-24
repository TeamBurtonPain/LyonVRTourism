using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class StateQuest
    {
        private Quest quest;
        private bool done;
        private List<StateCheckPoint> checkpoints;





        /// <summary>
        /// Initializes a new instance of the <see cref="StateQuest"/> class.
        /// The quest is 0% initialized
        /// </summary>
        /// <param name="q">The q.</param>
        public StateQuest(Quest q)
        {
            quest = q;
            done = false;

            //init checkpoints state to 0%
            checkpoints = new List<StateCheckPoint>(quest.Checkpoints.Count);
            for (int i = 0; i < quest.Checkpoints.Count; ++i)
            {
                checkpoints[i] = new StateCheckPoint(q.Checkpoints[i], StatusCheckPoint.UNINIT);
            }
        }

        public Quest Quest
        {
            get { return quest; }
        }

        public bool Done
        {
            get { return done; }
            set { done = value; }
        }

        public List<StateCheckPoint> Checkpoints
        {
            get { return checkpoints; }
        }

    }
}