using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    enum StatusCheckPoint
    {
        FINISHED,
        BEGUN,
        UNINIT
    }

    class StateCheckPoint
    {
        private long idCheckPoint;
        private StatusCheckPoint status;

    }
}
