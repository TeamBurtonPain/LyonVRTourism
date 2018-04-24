using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Controller
{
    /// <summary>
    /// Interface for the state pattern.
    /// Methods to override are common actions methods like the return action.
    /// </summary>
    public interface State
    {

        void ReturnAction();
        void OptionAction();
    }
}
