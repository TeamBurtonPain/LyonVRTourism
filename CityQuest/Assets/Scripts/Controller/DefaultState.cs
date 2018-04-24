using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Assets.Scripts.Controller.State" />
    public class DefaultState : State
    {
        private Controller controller;

        public DefaultState(Controller c)
        {
            controller = c;
        }

        /// <summary>
        /// Returns to the map page
        /// </summary>
        /// <seealso cref="Assets.Scripts.Controller.MapState" />
        public void ReturnAction()
        {
            controller.Transition(new MapState(controller));
        }

        public void OptionAction()
        {

        }
    }
}
