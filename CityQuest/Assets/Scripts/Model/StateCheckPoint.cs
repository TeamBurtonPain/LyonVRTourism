using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public enum StatusCheckPoint
    {
        FINISHED,
        BEGUN,
        UNINIT
    }

    public class StateCheckPoint
    {
        private CheckPoint checkpoint;
        private StatusCheckPoint status;

        public StateCheckPoint(CheckPoint cp, StatusCheckPoint status){
            this.checkpoint = cp;
            this.status = status;
        }

        public CheckPoint Checkpoint
        {
            get { return checkpoint; }
        }

        public StatusCheckPoint Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
